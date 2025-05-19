using ECSEx.Components;
using ECSEx.Systems;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static ECSEx.Components.CompID;

namespace ECSEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SystemManager.Register(new MovementSystem());

            var before = DateTime.Now;

            for (int i = 0; i < 100000; i++)
                EntityManager.Create("test1", new Position(i / 5, i % 5), new Gravity());

            EntityManager.Create("player", new Position(3f, 5f), new Gravity());

            for (int i = 0; i < 50000; i++)
                EntityManager.Create("test2", new Position(i / 5, i % 5));

            Console.Title = $"ECS TEST - Est Time of creating {EntityManager.EntitiesCount} entities: {(DateTime.Now - before)}";


            var esc = false;
            while (!esc)
            {
                Thread.Sleep(500);
                Events.InvokeUpdate();

            }
        }
    }
}
