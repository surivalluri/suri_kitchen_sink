using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWorkerService.HostedService
{
    public class MsgProcessorResolver : IMsgProcessorResolver
    {
        private readonly IEnumerable<IMsgProcessor> _msgProcessors;

        public MsgProcessorResolver(IEnumerable<IMsgProcessor> msgProcessors)
        {
            _msgProcessors = msgProcessors;
        }

        public IMsgProcessor Resolve(string msgType, string targetSystem)
        {
            var processor = _msgProcessors.FirstOrDefault(c => c.MessgeType == msgType && c.TargetSystem == targetSystem);

            if (processor == null) throw new InvalidOperationException($"No Processor Exists for the requested Message Type : {msgType} and TargetSystem : {targetSystem}");

            return processor;
        }
    }
}
