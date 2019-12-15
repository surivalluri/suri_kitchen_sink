using MyWorkerService.HostedService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class NEProcessor : MsgProcessor
    {
        public NEProcessor() : base("MsgType", "TargetSystem")
        {

        }

        public override Task<bool> ProcessMessage(Document document, CancellationToken cancellationToken)
        {
            ;
            ;
            ;
            ;
            ;
            return Task.FromResult<bool>(true);
        }
    }
}
