using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public class NPService : INPService
    {
        private readonly IMsgProcessorResolver _msgProcessorResolver;

        public NPService(IMsgProcessorResolver msgProcessorResolver)
        {
            _msgProcessorResolver = msgProcessorResolver;
        }

        public async Task<bool> Process(Document document, CancellationToken cancellationToken)
        {
            if (document.Name == null) throw new ArgumentNullException(nameof(document));

            //Resolve
            var msgProcessor = _msgProcessorResolver.Resolve(document?.MessageType, document?.TargetSystem);

            return await msgProcessor.ProcessMessage(document, cancellationToken);
        }
    }
}
