using System;
using System.Collections.Generic;
using System.Text;

namespace MyWorkerService.HostedService
{
    public interface IMsgProcessorResolver
    {
        IMsgProcessor Resolve(string msgType, string targetSystem);
    }
}
