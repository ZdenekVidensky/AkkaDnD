namespace AkkaDnD.Messages
{
    public sealed class InitDice
    {
        public int Seed;

        public InitDice(int seed)
        {
            Seed = seed;
        }
    }

    public sealed class RollRequest
    {
        public int Edges;

        public RollRequest(int edges)
        {
            Edges = edges;
        }
    }

    public sealed class RollResult
    {
        public int Result;

        public RollResult(int result)
        {
            Result = result;
        }
    }
}
