using VestanTestPlugin.Modules.CoreModule.LocatorFactory;
using System.Collections.Generic;
using System;

namespace VestanTestPlugin.Modules.CoreModule;

public class VestanPlugin {
    private static readonly Lazy<VestanPlugin> _instance = new Lazy<VestanPlugin>(() => new VestanPlugin());

    public static VestanPlugin Instance => _instance.Value;

    public static VestanTestPluginLocator Locator => VestanTestPluginLocator.Instance;

    private VestanPlugin() {
    }

    public void Initialize(BaseLocatorFactory[] baseLocatorFactories = null) {
        List<BaseLocatorFactory> factories = new List<BaseLocatorFactory>
        {
            new HttpClientFactory(),
            new PostServiceFactory()
        };

        if (baseLocatorFactories != null && baseLocatorFactories.Length > 0) {
            factories.AddRange(baseLocatorFactories);
        }

        Locator.InitializeFactories(factories.ToArray());
    }
}