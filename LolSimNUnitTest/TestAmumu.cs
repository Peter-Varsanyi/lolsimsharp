using NUnit.Framework;
using LolSimC;

namespace LolSimNUnitTest
{
    [TestFixture]
    public class Heroes
    {
        [Test]
        public void TestAmumu()
        {
            var x = new Amumu();
            Assert.AreEqual(x.Armor, 18);
            Assert.AreEqual(x.Attackdamage, 47);
        }
    }
}