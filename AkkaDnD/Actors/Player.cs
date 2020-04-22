namespace AkkaDnD.Actors
{
    using Akka.Actor;

    class Player : UntypedActor
    {
        public int ID { get => m_ID; }

        private int   m_ID;
        private int   m_Health;
        private float m_Damage;

        public Player(int ID, int health, float damage)
        {
            m_ID = ID;
            m_Health = health;
            m_Damage = damage;
        }

        protected override void OnReceive(object message)
        {

        }

        public static Props Props(int Id, int health, float damage) => Akka.Actor.Props.Create(() => new Player(Id, health, damage));
    }
}
