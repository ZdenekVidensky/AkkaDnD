namespace AkkaDnD.Actors.Events
{
    using System;
    using Akka.Actor;
    using AkkaDnD.Messages.Events;

    class CrossRoadEvent : Event
    {
        protected override EEventType EventType => EEventType.CrossRoad;
        protected override EEventAction[] EventActions => new EEventAction[] { EEventAction.Option1, EEventAction.Option2 };

        private int m_Option1Distance;
        private int m_Option2Distance;

        public CrossRoadEvent(int option1Distance, int option2Distance)
        {
            m_Option1Distance = option1Distance;
            m_Option2Distance = option2Distance;
        }

        protected override void OnActivate()
        {
            Console.WriteLine("Došel jsi až na rozcestí. Kam se vydáš?");
            Console.WriteLine("[1] - Vlevo");
            Console.WriteLine("[2] - Vpravo");
        }

        protected override void OnEventSelected(EEventAction action)
        {
            var distanceIncrease = 0;

            switch (action)
            {
                case EEventAction.Option1:
                    Console.WriteLine("Šel jsi vlevo.");
                    distanceIncrease = m_Option1Distance;
                    break;
                case EEventAction.Option2:
                    Console.WriteLine("Šel jsi vpravo");
                    distanceIncrease = m_Option2Distance;
                    break;
            }

            Context.Parent.Tell(new EventActionDistance(distanceIncrease));
            Context.Parent.Tell(new FinishEvent());
        }

        public static Props Props(int option1Distance, int option2Distance) => Akka.Actor.Props.Create(() => new CrossRoadEvent(option1Distance, option2Distance));
    }
}
