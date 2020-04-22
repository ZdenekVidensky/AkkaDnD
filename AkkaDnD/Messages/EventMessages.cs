using Akka.Actor;
using AkkaDnD.Actors.Events;

namespace AkkaDnD.Messages.Events
{
    public abstract class EventMessage
    {
    }

    public class FinishEvent
    {
    }

    public class GenerateEvent
    {
        public int Seed;

        public GenerateEvent(int seed)
        {
            Seed = seed;
        }
    }

    public class GetActualEventRequest { }
    public class GetActualEventResult
    {
        public IActorRef EventActor;

        public GetActualEventResult(IActorRef eventActor)
        {
            EventActor = eventActor;
        }
    }

    public class EventActions
    {
        public EEventAction[] Actions;

        public EventActions(EEventAction[] actions)
        {
            Actions = actions;
        }
    }

    public class ActivateCurrentEvent {}

    public class EventActionSelected
    {
        public EEventAction Action;

        public EventActionSelected(EEventAction action)
        {
            Action = action;
        }
    }

    public class EventActionDistance
    {
        public int DistanceIncrease;

        public EventActionDistance(int distanceIncrease)
        {
            DistanceIncrease = distanceIncrease;
        }
    }
}
