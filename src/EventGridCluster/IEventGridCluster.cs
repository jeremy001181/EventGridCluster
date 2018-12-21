using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid.Models;

namespace EventGridCluster
{
    public interface IEventGridCluster : IDisposable
    {
        Task PublishAsync(IList<EventGridEvent> events, CancellationToken cancelationToken = default(CancellationToken));
    }
}