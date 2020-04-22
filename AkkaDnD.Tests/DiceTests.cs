using AkkaDnD.Actors;
using AkkaDnD.Messages;
using NUnit.Framework;

namespace AkkaDnD.Tests
{
    [TestFixture]
    public class DiceTests : Akka.TestKit.NUnit.TestKit
    {
        [Test]
        public void Roll6EdgesDice()
        {
            var diceActor = ActorOfAsTestActorRef<Dice>();
            diceActor.Tell(new InitDice(42));
            diceActor.Tell(new RollRequest(6));
            var result = ExpectMsgFrom<int>(diceActor);
            Assert.AreEqual(5, result, "Result is wrong!");
        }

        [Test]
        public void Roll3EdgesDice()
        {
            var diceActor = ActorOfAsTestActorRef<Dice>();
            diceActor.Tell(new InitDice(42));
            diceActor.Tell(new RollRequest(3));
            var result = ExpectMsgFrom<int>(diceActor);
            Assert.AreEqual(3, result, "Result is wrong!");
        }

        [Test]
        public void Roll20EdgesDice()
        {
            var diceActor = ActorOfAsTestActorRef<Dice>();
            diceActor.Tell(new InitDice(42));
            diceActor.Tell(new RollRequest(3));
            var result = ExpectMsgFrom<int>(diceActor);
            Assert.AreEqual(3, result, "Result is wrong!");
        }
    }
}