using System;

namespace VestanTestPlugin.Modules.CoreModule.LocatorFactory {
    public abstract class BaseLocatorFactory {
        protected VestanTestPluginLocator Locator => VestanTestPluginLocator.Instance;

        public abstract void RegisterServices();

        protected T GetService<T>() where T : class {
            return Locator.GetService<T>();
        }

        protected void RegisterService<T>(Func<T> factory) where T : class {
            Locator.RegisterService(factory);
        }
    }
}