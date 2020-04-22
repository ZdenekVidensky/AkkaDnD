namespace AkkaDnD.Actors
{
    using System;
    using System.Collections.Generic;
    using Akka.Actor;
    using Akka.Event;
    using AkkaDnD.Actors.Events;
    using AkkaDnD.Messages;
    using AkkaDnD.Messages.Events;

    public class DungeonMaster : ReceiveActor
    {
        private IActorRef m_EventsManager;
        private IActorRef m_Player;

        private int       m_DistanceFromGoal;
        private Random    m_Random;
        private int       m_Seed;

        private ILoggingAdapter Log { get; } = Context.GetLogger();

        public DungeonMaster()
        {
            Receive<BeginGame>(message => OnBeginGame(message.Seed));
            Receive<EventActions>(eventActions => OnEventActions(eventActions));
            Receive<EventActionDistance>(distance => OnChangeDistance(distance.DistanceIncrease));
            Receive<FinishEvent>(_ => OnEventFinished());
        }

        private void OnBeginGame(int seed)
        {
            PrintIntro();

            m_Seed = seed;
            m_Random = new Random(m_Seed);
            m_DistanceFromGoal = m_Random.Next(5, 21);

            m_EventsManager = Context.ActorOf<EventsManager>("events-manager");
            m_Player        = Context.ActorOf(Player.Props(1, 200, 42.5f), "player");

            m_EventsManager.Tell(new GenerateEvent(m_Seed));
        }

        private void OnEventActions(EventActions eventActions)
        {
            var resultAction = GetPlayerAction(eventActions.Actions);
            m_EventsManager.Tell(new EventActionSelected(resultAction));
        }

        private void OnChangeDistance(int distanceIncrease)
        {
            m_DistanceFromGoal += distanceIncrease;
            Log.Info($"Posunul jsi se k cili, nyni je to {m_DistanceFromGoal}");
        }

        private void OnEventFinished()
        {
            if (m_DistanceFromGoal <= 0)
            {
                PrintOutro();
                Console.ReadLine();
                Context.System.Terminate();
            }
            else
            {
                m_EventsManager.Tell(new GenerateEvent(m_Seed));
                m_EventsManager.Tell(new ActivateCurrentEvent());
            }
        }

        // PLAYER INPUT

        private EEventAction GetPlayerAction(EEventAction[] eventActions)
        {
            var correct = false;
            var numberInput = 0;

            while (correct == false)
            {
                var input = Console.ReadLine();

                if (input == string.Empty)
                    continue;

                if (int.TryParse(input, out numberInput) == false || (numberInput < 1 || numberInput > eventActions.Length))
                {
                    Console.WriteLine($"Vyberte prosím jednu z možností [1] - [{eventActions.Length}]");
                    continue;
                }

                break;
            }

            return eventActions[numberInput - 1];
        }

        private void PrintIntro()
        {
            Console.WriteLine(" /////////////////////////////////");
            Console.WriteLine(" //////AKKA DUNGEONS & DRAGONS///");
            Console.WriteLine(" ////////////////////////////////");
            Console.WriteLine();

            var story = "Vítej dobrodruhu. Jsi rytíř, který se vydal do temné jeskyně, aby tu našel bájný poklad. Nicméně o jeskyni se traduje, že je velmi " +
                "nebezpečná, protože v ní sídlí nejrůznější příšery z pohádek starých bab. Nicméně protože jsi rytíř a nic tě jen tak nevyděsí, směle jsi vstoupil "
                + "do kobky.";

            Console.WriteLine(story);
            Console.WriteLine();
        }

        private void PrintOutro()
        {
            Console.WriteLine("Výborně! Došel jsi až do cíle a našel poklad. Zde tvá cesta končí.");
        }
    }
}
