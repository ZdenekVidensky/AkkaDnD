namespace AkkaDnD.Actors
{
    using Akka.Actor;
    using AkkaDnD.Messages;

    public class Dice : UntypedActor
    {
        private System.Random m_Random;

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case InitDice init:
                    m_Random = new System.Random(init.Seed);
                    break;
                case RollRequest request:
                    var result = GetRollResult(request.Edges);
                    Sender.Tell(result);
                    break;
            }
        }

        private int GetRollResult(int edges)
        {
            return m_Random.Next(1, edges + 1);
        }
    }
}
