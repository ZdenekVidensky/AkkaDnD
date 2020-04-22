namespace AkkaDnD.Actors
{
    using Akka.Actor;
    using Akka.Event;
    using AkkaDnD.Actors.Events;
    using AkkaDnD.Messages.Events;

    class EventsManager : ReceiveActor
    {
        private IActorRef       m_CurrentEvent;
        private System.Random   m_Random;

        public EventsManager()
        {
            m_Random = new System.Random();

            Receive<GenerateEvent>(m => OnGenerateEvent(m.Seed));
            Receive<GetActualEventRequest>(m => Sender.Tell(new GetActualEventResult(m_CurrentEvent)));
            Receive<EventActionSelected>(m => OnEventActionSelected(m));
            Receive<FinishEvent>(finished => OnEventFinished(finished));
            Receive<EventActionDistance>(distance => Context.Parent.Forward(distance));
        }

        private void OnGenerateEvent(int seed)
        {
            var event1     = Context.ActorOf(CrossRoadEvent.Props(-2, -1), $"event-{m_Random.Next(int.MaxValue)}");
            m_CurrentEvent = event1;
            m_CurrentEvent.Forward(new ActivateCurrentEvent());
        }

        private void OnEventActionSelected(EventActionSelected selected)
        {
            m_CurrentEvent.Forward(selected);
        }

        private void OnEventFinished(FinishEvent finished)
        {
            m_CurrentEvent.Tell(PoisonPill.Instance);
            m_CurrentEvent = null;
            Context.Parent.Forward(finished);
        }
    }
}
