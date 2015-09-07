using CounterActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStateless.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var proxy = ActorProxy.Create<ICounterActor>(ActorId.NewId(), "fabric:/CounterStateless");

            Console.WriteLine("From Actor {0}: {1}", proxy.GetActorId(), proxy.DoWorkAsync().Result);
        }
    }
}
