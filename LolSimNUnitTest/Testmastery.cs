using LolSimC;
using NUnit.Framework;

namespace LolSimNUnitTest
{
    [TestFixture]
    internal class Testmastery
    {
        [Test]
        public void TestAlacrity()
        {
            var x = new Player {Attackspeed = 1.0};
            var m = new Mastery {Alacrity = 4};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 1.6, actual: x.Attackspeed);
        }

        [Test]
        public void TestArcaneKnowledge()
        {
            var x = new Player();
            var m = new Mastery {ArcaneKnowledge = 1};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 0.9, actual: x.Percentmagicpenetration);
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 0.81, actual: x.Percentmagicpenetration);
        }

        [Test]
        public void TestBruteforce()
        {
            var x = new Player();
            var m = new Mastery {Bruteforce = 3};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 3, actual: x.BonusAttackdamage);
        }

        [Test]
        public void TestButcher()
        {
            var x = new Player();
            var m = new Mastery {Butcher = 2};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 4, actual: x.BonusDamageToMinions);
        }

        [Test]
        public void TestDeadliness()
        {
            var x = new Player();
            var m = new Mastery {Deadliness = 4};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: .5, actual: x.BonusAttackdamage);
        }

        [Test]
        public void TestMentalForce()
        {
            var x = new Player();
            var m = new Mastery {MentalForce = 4};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 4, actual: x.Bonusabilitypower);
        }

        [Test]
        public void TestSorcery()
        {
            var x = new Player();
            var m = new Mastery {Sorcery = 4};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 0.04, actual: x.CooldownReduction);
        }

        [Test]
        public void TestSummonersWrath()
        {
            var x = new Player();
            var m = new Mastery {Summonerswrath = 1};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 1, actual: m.Summonerswrath);
        }

        [Test]
        public void TestWeaponExpertise()
        {
            var x = new Player();
            var m = new Mastery {WeaponExpertise = 1};
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 0.9, actual: x.Percentarmorpenetration);
            x.ApplyBuff(m);
            Assert.AreEqual(expected: 0.81, actual: x.Percentarmorpenetration);
        }
    }
}