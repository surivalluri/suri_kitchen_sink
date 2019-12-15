using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public interface INPService
    {
        Task<bool> Process(Document document, CancellationToken cancellationToken);
    }
}
