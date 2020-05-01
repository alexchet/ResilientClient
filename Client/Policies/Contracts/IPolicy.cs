using Polly;
using System.Net.Http;

namespace SampleClient.Policies.Contracts
{
    public interface IPolicy
    {
        IAsyncPolicy<HttpResponseMessage> GetPolicyWrap(HttpRequestMessage request);
    }
}
