using SampleClient.Handlers;
using SampleClient.Policies;
using System;
using System.Net.Http;

namespace SampleClient.Factory
{
    public class ClientFactory
    {
        public static HttpClient FooHttpClient => GetFooHttpClient();
        public static HttpClient BarHttpClient => GetBarHttpClient();

        private static HttpClient GetFooHttpClient()
        {
            var resilientFooPolicy = new ResilientFooPolicy();
            var resilientDelegatingHandler = new ResilientDelegatingHandler(resilientFooPolicy);
            var httpClient = HttpClientFactory.Create(resilientDelegatingHandler);
            httpClient.BaseAddress = new Uri("http://localhost:54197");

            return httpClient;
        }
        private static HttpClient GetBarHttpClient()
        {
            var resilientBarPolicy = new ResilientBarPolicy();
            var resilientDelegatingHandler = new ResilientDelegatingHandler(resilientBarPolicy);
            var httpClient = HttpClientFactory.Create(resilientDelegatingHandler);
            httpClient.BaseAddress = new Uri("http://localhost:54197");

            return httpClient;
        }
    }
}
