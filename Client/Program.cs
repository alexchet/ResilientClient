using Autofac;
using SampleClient.Factory;
using SampleClient.Handlers;
using SampleClient.Policies;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.ExternalClient;
using WebApi.Modules;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Method 1 - HttpClient

            var fooHttpClient = ClientFactory.FooHttpClient;
            var httpResponseMessage = await fooHttpClient.GetAsync("api/values/5");
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine("HttpClient: " + response);

            httpResponseMessage = await fooHttpClient.GetAsync("api/values");
            response = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(response);

            await fooHttpClient.PostAsync("api/values", new StringContent("Hello", Encoding.UTF8, "application/json"));

            var barHttpClient = ClientFactory.BarHttpClient;
            var httpResponseMessage2 = await barHttpClient.GetAsync("api/values/5");
            var response2 = await httpResponseMessage2.Content.ReadAsStringAsync();
            Console.WriteLine("HttpClient: " + response2);

            //Method 2 - Using a container
            const string baseUrl = "http://localhost:54197/";

            var resilientFooPolicy = new ResilientFooPolicy();
            var resilientDelegatingHandler = new ResilientDelegatingHandler(resilientFooPolicy);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ValueClientModule(baseUrl, resilientDelegatingHandler));
            
            var container = builder.Build();

            var restClient = container.Resolve<IWebApiRestClient>();

            var restResponse = await restClient.GetValue(5, CancellationToken.None);
            Console.WriteLine("Container: " + restResponse.StringContent);

            Console.ReadLine();
        }
    }
}
