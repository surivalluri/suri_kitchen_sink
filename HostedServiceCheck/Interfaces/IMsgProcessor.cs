using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public interface IMsgProcessor
    {
        string MessgeType { get;}
        string TargetSystem { get;}
        bool IsSupported(string msgType, string targetSystem);
        Task<bool> ProcessMessage(Document document, CancellationToken cancellationToken);
    }
}
