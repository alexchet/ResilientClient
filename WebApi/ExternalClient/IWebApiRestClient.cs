using Microsoft.AspNetCore.Mvc;
using RestEase;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.ExternalClient
{
    public interface IWebApiRestClient
    {
        [Get("/api/values")]
        Task<Response<ActionResult<IEnumerable<string>>>> GetValues(CancellationToken cancellationToken);

        [Get("/api/values/{id}")]
        Task<Response<ActionResult<string>>> GetValue([Path] int id, CancellationToken cancellationToken);

        [Post("/api/values")]
        Task PostValue([Body] string value, CancellationToken cancellationToken);
    }
}
