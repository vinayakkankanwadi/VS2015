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
        public async Task<string> DoWorkAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Doing Work");

            return await Task.FromResult("Work result");
        }
    }
}
