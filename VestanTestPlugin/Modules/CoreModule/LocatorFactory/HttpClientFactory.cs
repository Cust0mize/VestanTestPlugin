using System.Net.Http;
using System;

namespace VestanTestPlugin.Modules.CoreModule.LocatorFactory {
    public class HttpClientFactory : BaseLocatorFactory {
        public override void RegisterServices() {
            RegisterService(() => new HttpClient()
            {
                BaseAddress = new Uri(GlobalConstant.BASE_URL)
            });
        }
    }
}