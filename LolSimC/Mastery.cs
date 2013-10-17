namespace LolSimC
{
    public class Mastery : Buff
    {
        private int _alacrity;
        private int _bruteforce;
        private int _butcher;
        private int _demolitionist;
        private int _mentalforce;
        private int _sorcery;
        private int _summonerswrath;
        private int _deadliness;
        private int _weaponexpertise;
        private int _arcaneknowledge;
        public int ArcaneKnowledge
        {
            get
            {
                return _arcaneknowledge;
            }
            set
            {
                if (value < 0 || value > 1) return;
                PercentageMagicPenetration = value * 0.1;
                _arcaneknowledge = value;
            }
        }

        public int WeaponExpertise
        {
            get { return _weaponexpertise; }
            set
            {
                if (value < 0 && value > 1) return;
                PercentageArmorPenetration = value * 0.1;
                _weaponexpertise = value;
            }
        }

        public int Deadliness
        {
            get
            {
                return _deadliness;

            }
            set
            {
                if (value < 0 && value > 4) return;
                BonusAttackDamagePerLevel = 0.125 * value;
                _deadliness = value;
            }
        }
        public int Butcher
        {
            get { return _butcher; }
            set
            {
                if (value < 0 && value > 2) return;
                BonusDamageToMinions = 2 * value;
                _butcher = value;
            }
        }

        public int Sorcery
        {
            get { return _sorcery; }
            set
            {
                if (value < 0 && value > 4) return;
                CooldownReduction = 0.01 * value;
                _sorcery = value;
            }
        }
        public int MentalForce
        {
            get { return _mentalforce; }
            set
            {
                if (value < 0 && value > 4) return;
                BonusAbilityPower = _mentalforce = value;
            }
        }
        public int Alacrity
        {
            get { return _alacrity; }
            set
            {
                if (value < 0 && value > 4) return;
                Attackspeed = 1 + 0.15 * value;
                _alacrity = value;
            }
        }
        public int Bruteforce
        {
            get { return _bruteforce; }
            set
            {
                if (value < 0 && value > 3) return;
                BonusAttackDamage = value;
                _bruteforce = value;
            }
        }

        public int Summonerswrath
        {
            get { return _summonerswrath; }
            set { if (value == 0 || value == 1) _summonerswrath = value; }
        }
    }
}