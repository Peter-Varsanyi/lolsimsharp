using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LolSimC;
using NUnit.Framework;

namespace LolSimNUnitTest
{
    class TestItem
    {
        [TestFixture]
        public class ItemTestClass
        {
            private readonly Player _x = new Player();
            readonly Item _y = new Item { Armor = 1, Attackspeed = 1.0, BonusAttackDamage = 1 ,ArmorPerLevel = 1};

            public ItemTestClass()
            {
                _x.Attackspeed = 1.0;
                _x.AddItem(_y);
            }
            [Test]
            public void TestAttackSpeed()
            {
                Assert.AreEqual(expected: 1.0, actual: _x.Attackspeed);
            }
            [Test]
            public void TestArmor()
            {
                Assert.AreEqual(expected: 1, actual:_x.Armor);
            }
            [Test]
            public void TestBonusAttackdamage()
            {
                Assert.AreEqual(expected: 1, actual: _x.BonusAttackdamage);
            }
        }
    }
}
