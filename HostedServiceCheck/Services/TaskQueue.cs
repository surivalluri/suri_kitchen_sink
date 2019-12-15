using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public class TaskQueue : ITaskQueue
    {
        public BlockingCollection<Func<CancellationToken, Task>> WorkItem;
        private readonly TaskQueueSettings _taskQueueSettings;

        public TaskQueue(IOptions<TaskQueueSettings> taskQueueSettings)
        {
            _taskQueueSettings = taskQueueSettings.Value ?? throw new ArgumentNullException(nameof(taskQueueSettings));
            WorkItem = new BlockingCollection<Func<CancellationToken, Task>>(_taskQueueSettings.QueueSize);
        }

        public void Queue(Func<CancellationToken, Task> workItem, CancellationToken cancellationToken)
        {
            WorkItem.Add(workItem, cancellationToken);
        }

        public Func<CancellationToken, Task> Dequeue()
        {
            return WorkItem.Take();
        }
    }

    public class TaskQueueSettings
    {
        public int QueueSize { get; set; } = 100;
    }

    public interface ITaskQueue
    {
        void Queue(Func<CancellationToken, Task> workItem, CancellationToken cancellationToken);

        Func<CancellationToken, Task> Dequeue();
    }
}
