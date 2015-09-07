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
    public class CounterActor : Actor, ICounterActor
    {
        private int value = 0;

        public Task<int> GetAsync()
        {
            return Task.FromResult(this.value);
        }

        public Task SetAsync(int count)
        {
            this.value = count;
            return Task.FromResult(true);
        }

        public Task<int> CompareExchangeAsync(int value, int comparand)
        {
            ActorEventSource.Current.ActorMessage(this, "CompareExchange({0},{1})", value, comparand);
            ActorEventSource.Current.ActorMessage(this, "Current counter value is {0}", this.value);

            var current = this.value;
            if (current == comparand)
            {
                this.value = value;
                ActorEventSource.Current.ActorMessage(this, "Current counter value is now {0}", this.value);
            }
            return Task.FromResult(current);
        }
    }
}
