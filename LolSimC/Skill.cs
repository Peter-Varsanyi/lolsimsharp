using System;

namespace LolSimC
{
    public class Skill
    {
        private double _cooldown;

        public Skill()
        {
            Toggle = false;
        }

        //        bool aoe;
        //      int phase, level;
        //    Damage basedamage;
        //  string name;

        public double PercentageDamage { get; set; }
        public Boolean Toggle { get; set; }
        public double Truedamage { get; set; }
        public double Bonusattackdamagescale { get; set; }
        public double Attackdamagescale { get; set; }
        public double Abilitypowerscale { get; set; }
        public double Maxcooldown { get; set; }

        public double Cooldown
        {
            get { return _cooldown; }
            set { _cooldown = value < 0 ? 0 : value; }
        }

        public string Name { get; set; }
        public int Phase { get; set; }
        public int Level { get; set; }
        public bool Aoe { get; set; }
        public Damage Basedamage { get; set; }

        public Damage CalculateDamage(Base src, Base target)
        {
            var x = Basedamage +
                       new Damage(Truedamage, src.Bonusabilitypower*Abilitypowerscale,
                                  src.BonusAttackdamage*Bonusattackdamagescale);
            //Log.ToLog("Skill" + x);

            return x;
        }

        public void ReduceCooldown(double time)
        {
            Cooldown -= time;
            if (Cooldown < 0) Cooldown = 0;
        }
    }
}