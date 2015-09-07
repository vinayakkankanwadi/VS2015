﻿using System;
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
        public override Task OnActivateAsync()
        {
            if (this.State == null)
            {
                this.State = new CounterActorState() { Count = 0 };
            }

            ActorEventSource.Current.ActorMessage(this, "State initialized to {0}", this.State);
            return Task.FromResult(true);
        }

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
