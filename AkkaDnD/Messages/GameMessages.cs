namespace AkkaDnD.Messages
{
    public sealed class BeginGame
    {
        public int Seed;

        public BeginGame(int seed)
        {
            Seed = seed;
        }
    }
}
