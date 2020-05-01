using Autofac;
using RestEase;
using System;
using System.Net.Http;
using WebApi.ExternalClient;
using WebApi.Service;

namespace WebApi.Modules
{
    public class ValueClientModule : Module
    {
        private readonly Uri _baseUrl;
        private readonly DelegatingHandler _delegatingHandler;

        public ValueClientModule(string baseUrl, DelegatingHandler delegatingHandler)
        {
            _baseUrl = new Uri(baseUrl);
            _delegatingHandler = delegatingHandler;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var httpClient = HttpClientFactory.Create(_delegatingHandler);
            httpClient.BaseAddress = _baseUrl;

            var webApiRestClient = RestClient.For<IWebApiRestClient>(httpClient);

            builder
                .Register(x => webApiRestClient);

            builder
                .RegisterType<ValueClientService>()
                .As<IValueClientService>()
                .SingleInstance();
        }
    }
}
