using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Service
{
    public interface IValueClientService
    {
        Task<ActionResult<IEnumerable<string>>> GetValues(CancellationToken cancellationToken = default(CancellationToken));

        Task<ActionResult<string>> GetValue(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task PostValue(string value, CancellationToken cancellationToken = default(CancellationToken));
    }
}
