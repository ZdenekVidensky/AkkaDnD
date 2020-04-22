namespace AkkaDnD.Messages
{
    public sealed class RegisterPlayer
    {
        public int   ID;
        public int   Health;
        public float Damage;

        public RegisterPlayer(int Id, int health, float damage)
        {
            ID     = Id;
            Health = health;
            Damage = damage;
        }
    }
}
