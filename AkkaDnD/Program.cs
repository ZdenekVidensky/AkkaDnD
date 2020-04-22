namespace AkkaDnD
{
    using Akka.Actor;
    using AkkaDnD.Actors;
    using AkkaDnD.Messages;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var system = ActorSystem.Create("dnd-game");
            var dungeonMaster = system.ActorOf<DungeonMaster>("dungeon-master");
            dungeonMaster.Tell(new BeginGame(011001));

            await system.WhenTerminated;
        }
    }
}
