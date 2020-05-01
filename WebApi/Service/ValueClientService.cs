using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.ExternalClient;

namespace WebApi.Service
{
    public class ValueClientService : IValueClientService
    {
        private readonly IWebApiRestClient _client;
        public async Task<ActionResult<string>> GetValue(int id, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetValue(id, cancellationToken);
            return response.GetContent();
        }

        public async Task<ActionResult<IEnumerable<string>>> GetValues(CancellationToken cancellationToken = default)
        {
            var response = await _client.GetValues(cancellationToken);
            return response.GetContent();
        }

        public async Task PostValue(string value, CancellationToken cancellationToken = default)
        {
            await _client.PostValue(value, cancellationToken);
        }
    }
}
