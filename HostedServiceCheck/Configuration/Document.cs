using System;

namespace MyWorkerService.HostedService
{
    public class Document
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string FullAddress { get; set; }

        public string MessageType { get; set; }

        public string TargetSystem { get; set; }

        public int Count { get; set; 
        }

        public DateTime ReceivedOn { get; set; } 
    }
}