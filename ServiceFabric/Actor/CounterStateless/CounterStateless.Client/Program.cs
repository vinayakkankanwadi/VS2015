using CounterActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CounterStateless.Client
{
    public class Program
    {
        private const string ApplicationName = "fabric:/CounterStateless";

        public static void Main(string[] args)
        {
            var counter = ActorProxy.Create<ICounterActor>(new ActorId("COUNTER-1"), //Actor Id
                ApplicationName);

            PrintConnectionInformation(counter);

            for (;;)
            {
                var value = counter.GetAsync().Result;
                Console.WriteLine(@"Counter {1} value: {0}", value, counter.GetActorId());
                Console.WriteLine();
                counter.CompareExchangeAsync(value + 1, value).Wait();
                Thread.Sleep(500);
            }
        }
        private static void PrintConnectionInformation(ICounterActor counterActor)
        {
            var actorProxy = counterActor as IActorProxy;

            if (actorProxy != null)
            {
                ResolvedServicePartition rsp;

                if (actorProxy.ActorServicePartitionClient.TryGetLastResolvedServicePartition(out rsp))
                {
                    var endpoint = rsp.GetEndpoint();
                    Console.WriteLine();
                    Console.WriteLine(
                        @"Connected to a Counter of an actor {0} hosted by the replica of a {3} Service {1} listening at Address {2}",
                        actorProxy.ActorId,
                        rsp.ServiceName,
                        endpoint.Address,
                        endpoint.Role);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        @"Connecting to Counter box of an actor {0} hosted by the replica of Service {1}",
                        actorProxy.ActorId,
                        actorProxy.ActorServicePartitionClient.ServiceUri);
                }
            }
        }
    }
}
