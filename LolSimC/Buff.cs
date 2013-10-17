namespace LolSimC
{
    public class Buff
    {
        public Buff()
        {
            FlatPhysicalDamageReduced = 0;
        }

        public double PercentageMagicPenetration { get; set; }
        public double PercentageArmorPenetration { get; set; }
        public double BonusAttackDamagePerLevel { get; set; }
        public double BonusDamageToMinions { get; set; }
        public double CooldownReduction { get; set; }
        public double BonusAttackDamage { get; set; }
        public double BonusAbilityPower { get; set; }

        public double ArmorPerLevel { get; set; }


        public double Armor { get; set; }

        public double FlatPhysicalDamageReduced { get; set; }

        public double FlatMagicalDamageReduced { get; set; }

        public double PercentagePhysicalDamageReduced { get; set; }

        public double PercentageMagicalDamageReduced { get; set; }

        public int Stack { get; set; }

        public int Hitcount { get; set; }

        public string Name { get; set; }

        public double Shield { get; set; }

        public double Attackspeed { get; set; }

        public double Duration { get; set; }

        public bool Unique { get; set; }

        public bool Permanent { get; set; }
    }
}