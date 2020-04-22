namespace AkkaDnD.Actors.Events
{
    using Akka.Actor;
    using AkkaDnD.Messages.Events;

    abstract class Event : ReceiveActor
    {
        protected abstract EEventType EventType { get; }
        protected abstract EEventAction[] EventActions { get; }

        public Event()
        {
            Receive<ActivateCurrentEvent>(_ =>
            {
                OnActivate();
                Sender.Tell(new EventActions(EventActions));
            });

            Receive<EventActionSelected>(m => OnEventSelected(m.Action));
        }

        //protected override void OnReceive(object message)
        //{
        //    switch (message)
        //    {
        //        case FinishEvent _:
        //            Self.Tell(PoisonPill.Instance);
        //            break;
        //        case ActivateCurrentEvent _:
        //            OnActivate();
        //            Sender.Tell(new EventActions(EventActions));
        //            break;
        //        case EventActionSelected eventSelected:
        //            OnEventSelected(eventSelected.Action);
        //            break;
        //    }
        //}

        protected abstract void OnActivate();
        protected abstract void OnEventSelected(EEventAction action);
    }

    public enum EEventType
    {
        CrossRoad,
        Enemies,
        End
    }

    public enum EEventAction
    {
        Option1,
        Option2,
        DiceRoll
    }
}
