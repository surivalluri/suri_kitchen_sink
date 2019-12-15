using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public abstract class MsgProcessor : IMsgProcessor
    {
        public MsgProcessor(string msgType, string targetSystem)
        {
            MessgeType = msgType; TargetSystem = targetSystem;
        }

        public string MessgeType { get; }

        public string TargetSystem { get; }

        public bool IsSupported(string msgType, string targetSystem)
        {
            if (msgType == MessgeType && targetSystem == TargetSystem) return true; else return false;
        }

        public abstract Task<bool> ProcessMessage(Document document, CancellationToken cancellationToken);
    }
}
