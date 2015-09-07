using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CounterActor.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;

namespace CounterActor
{
    public class CounterActor : Actor<CounterActorState>, ICounterActor
    {
        public Task<int> CompareExchangeAsync(int count, int comparand)
        {
            ActorEventSource.Current.ActorMessage(this, "CompareExchange({0},{1})", count, comparand);
            ActorEventSource.Current.ActorMessage(this, "Current counter value is {0}", this.State.Count);

            var current = this.State.Count;
            if (current == comparand)
            {
                this.State.Count = count;
                ActorEventSource.Current.ActorMessage(this, "Current counter value is now {0}", this.State.Count);
            }
            return Task.FromResult(current);
        }
        
        [Readonly]
        public Task<int> GetCountAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Getting current count value as {0}", this.State.Count);
            return Task.FromResult(this.State.Count);
        }

        public Task SetCountAsync(int count)
        {
            ActorEventSource.Current.ActorMessage(this, "Setting current count of value to {0}", count);
            this.State.Count = count;

            return Task.FromResult(true);
        }
    }
}
