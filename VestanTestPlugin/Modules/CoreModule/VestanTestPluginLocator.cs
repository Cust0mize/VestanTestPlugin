using VestanTestPlugin.Modules.CoreModule.LocatorFactory;
using System.Collections.Generic;
using System;

namespace VestanTestPlugin.Modules.CoreModule {
    public sealed class VestanTestPluginLocator : IDisposable {
        private static readonly Lazy<VestanTestPluginLocator> _instance = new Lazy<VestanTestPluginLocator>(() => new VestanTestPluginLocator());

        private readonly List<BaseLocatorFactory> _registeredFactories;
        private readonly Dictionary<Type, object> _singletonInstances;
        private readonly Dictionary<Type, Func<object>> _factories;
        private readonly List<IDisposable> _disposables;

        internal static VestanTestPluginLocator Instance => _instance.Value;

        private VestanTestPluginLocator() {
            _factories = new Dictionary<Type, Func<object>>();
            _singletonInstances = new Dictionary<Type, object>();
            _disposables = new List<IDisposable>();
            _registeredFactories = new List<BaseLocatorFactory>();
        }
        
        public T GetService<T>() where T : class {
            if (TryGetService<T>(out T service)) {
                return service;
            }

            throw new InvalidOperationException($"Service {typeof(T).Name} is not registered");
        }
        
        public bool TryGetService<T>(out T service) where T : class {
            Type type = typeof(T);

            if (_singletonInstances.TryGetValue(type, out object singletonInstance)) {
                service = (T)singletonInstance;
                return true;
            }

            if (_factories.TryGetValue(type, out Func<object> factory)) {
                object createdService = factory();

                if (createdService is IDisposable disposable) {
                    _disposables.Add(disposable);
                }

                service = (T)createdService;
                return true;
            }

            service = null;
            return false;
        }

        internal void RegisterService<T>(Func<T> factory) where T : class {
            Type type = typeof(T);
            _factories[type] = factory;
        }
        
        internal void InitializeFactories(BaseLocatorFactory[] baseLocatorFactories) {
            for (int index = 0; index < baseLocatorFactories.Length; index++) {
                BaseLocatorFactory factory = baseLocatorFactories[index];
                _registeredFactories.Add(factory);
                factory.RegisterServices();
            }
        }

        internal void RegisterService<T>(T instance) where T : class {
            Type type = typeof(T);
            _singletonInstances[type] = instance;

            if (instance is IDisposable disposable) {
                _disposables.Add(disposable);
            }
        }

        public void Dispose() {
            for (int index = 0; index < _disposables.Count; index++) {
                IDisposable disposable = _disposables[index];
                disposable?.Dispose();
            }

            _disposables.Clear();
            _singletonInstances.Clear();
            _factories.Clear();
            _registeredFactories.Clear();
        }
    }
}