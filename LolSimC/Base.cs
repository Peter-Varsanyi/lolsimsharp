using System.Collections.Generic;
using System.Linq;

namespace LolSimC
{
    public abstract class Base
    {
        #region Delegates

        public delegate void AutoattackHandler(Base source);

        public delegate void DamageTakenHandler(Base source);

        #endregion

        private double _armor, _attackspeed;
        private double _bonusattackdamage;

        private AutoattackHandler _autoattackcallback;
        private DamageTakenHandler _damagetakencallback;
        private double _bonusabilitypower;
        private double _cooldownreduction;
        private double _bonusdamagetominions;

        protected Base()
        {
            Inventory = new List<Item>();
            Skills = new List<Skill>();
            Buffs = new List<Buff>();
            Level = 1;
        }

        public double Armor
        {
            get
            {
                var armor = _armor;
                foreach (var buff in Buffs)
                {
                    armor += buff.ArmorPerLevel * Level;
                    armor += buff.Armor;
                }
                if (Inventory != null)
                {
                    var sum = Inventory.Sum(item => item.Armor);
                    armor += sum;
                }
                return armor;
            }
            set { _armor = value; }
        }

        public double Percentarmorpenetration
        {
            get
            {
                return Buffs.Aggregate<Buff, double>(1, (current, x) => current*(1 - x.PercentageArmorPenetration));
            }
        }
        public double Armorpenetration { get; set; }
        public double Percentmagicpenetration
        {
            get
            {
                return Buffs.Aggregate<Buff, double>(1, (current, x) => current*(1 - x.PercentageMagicPenetration));
            }
        }
        public double Magicpenetration { get; set; }
        public double BonusDamageToMinions
        {
            get
            {
                return _bonusdamagetominions + Buffs.Where(buff => buff != null).Sum(buff => buff.BonusDamageToMinions) + Inventory.Sum(item => item.BonusDamageToMinions);
            }
            set { _bonusdamagetominions = value; }
        }

        public double CooldownReduction
        {
            get
            {
                return _cooldownreduction + Buffs.Where(buff => buff != null).Sum(buff => buff.CooldownReduction) + Inventory.Sum(item => item.CooldownReduction);
            }
            set { _cooldownreduction = value; }
        }

        public double Bonusabilitypower
        {
            get
            {
                return _bonusabilitypower + Buffs.Where(buff => buff != null).Sum(buff => buff.BonusAbilityPower) + Inventory.Sum(item => item.BonusAbilityPower);
            }
            set { _bonusabilitypower = value; }
        }
        public double Attackrange { get; set; }
        public double Attackspeed
        {
            get
            {
                var attackspeed = Buffs.Where(buff => buff != null).Aggregate(_attackspeed, (current, buff) => current * buff.Attackspeed);
                return attackspeed = Inventory.Where(item => item != null).Aggregate(attackspeed, (current, item) => current * item.Attackspeed);
            }
            set { _attackspeed = value; }
        }
        public double BonusAttackdamage
        {
            get
            {
                return _bonusattackdamage + Buffs.Where(buff => buff != null).Sum(buff => buff.BonusAttackDamage) + Inventory.Sum(item => item.BonusAttackDamage) + Buffs.Where(buff => buff != null).Sum(buff => buff.BonusAttackDamagePerLevel * Level);
            }
            set { _bonusattackdamage = value; }
        }
        public double Attackdamage { get; set; }
        public double Resourceregen { get; set; }
        public double Healthregen { get; set; }
        public double Maxresource { get; set; }
        public double Resource { get; set; }
        public double MaxHealth { get; set; }
        public double Health { get; set; }

        public string Name { get; set; }
        public List<Item> Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public Base Target { get; set; }
        public List<Buff> Buffs { get; set; }
        public double Nextswing { get; set; }
        public double Movementspeed { get; set; }
        public double Magicresist { get; set; }
        public int Level { get; set; }
        public abstract bool IsCreep();

        public bool Alive()
        {
            return Health > 0;
        }

        public void UnitDie()
        {
        }

        public virtual void CdReduce(double time)
        {
            Nextswing -= time;
            foreach (var x in Buffs)
            {
                x.Duration -= time;
            }
        }

        public virtual void RemoveBuff(Buff buffname)
        {
            Buffs.Remove(buffname);
        }

        public virtual void ApplyBuff(Buff buffname)
        {
            Buffs.Add(buffname);
        }

        public void AddItem(Item item)
        {
            if (Inventory.Count < 6)
            {
                Inventory.Add(item);
            }
        }

        public Damage GetDamage()
        {
            return new Damage(0, 0, Attackdamage + BonusAttackdamage);
        }

        public double GetLowestSkillCooldown()
        {
            return Skills.Select(t => t.Cooldown).Concat(new double[] { 12 }).Min();
        }

        public void ActivateAutoAttack()
        {
            if (Nextswing < double.Epsilon)
            {
                AutoAttack();
            }
        }

        public void TestAutoAttack(Base source)
        {
            Log.ToLog(Name + " azt uzeni " + source.Name + "-nek, hogy bazdmeg!");
        }

        public void RegisterDamageTakenCallBack(DamageTakenHandler handler)
        {
            _damagetakencallback = handler;
        }

        public void RegisterAutoattackCallBack(AutoattackHandler handler)
        {
            _autoattackcallback = handler;
        }

        public void AutoAttack()
        {
            Target.TakeAutoattackDamage(this, GetDamage());
            Nextswing = 1 / Attackspeed;
            if (_autoattackcallback != null)
            {
                _autoattackcallback(this);
            }
        }

        public void ActivateSkills(List<Base> camp)
        {
            foreach (var t in Skills.Where(t => t.Cooldown < double.Epsilon))
            {
                if (t.Aoe)
                {
                    foreach (var target in camp)
                    {
                        if (!target.Equals(this))
                        {
                            target.TakeSkillDamage(source: this, damage: t.CalculateDamage(this, target));
                        }
                        t.Cooldown = t.Maxcooldown;
                    }
                }
                else
                {
                    Target.TakeSkillDamage(this, t.CalculateDamage(this, Target));
                    t.Cooldown = t.Maxcooldown;
                }
            }
        }

        public virtual void TakeAutoattackDamage(Base source, Damage damage)
        {
            TakeDamage(source, damage);
            Log.ToLog(source, target: this, damage: damage, type: "autoattackdamage");
            if (_damagetakencallback != null)
            {
                _damagetakencallback(source);
            }
        }

        public virtual void TakeSkillDamage(Base source, Damage damage)
        {
            TakeDamage(source, damage);
            Log.ToLog(source: source, target: this, damage: damage, type: "skilldamage");
        }

        public virtual void TakeDamage(Base source, Damage damage)
        {
            //Log.ToLog("Base take damage");

            if (Equals(damage, new Damage(0, 0, 0)))
            {
                return;
            }
            // 1. percentage armor reduction
            // 2. flat armor reduction
            // 3. flat amor penetration
            // 4. percentage armor penetration
            // 1
            // 2
            // 3

            var myarmor = Armor;
            if (myarmor >= source.Armorpenetration)
            {
                myarmor -= source.Armorpenetration;
            }
            else
            {
                myarmor = 0;
            }
            // 4
            // 1. percentage magic resistance reduction
            // 2. flat magic resistance reduction
            // 3. flat magic resistance penetration
            // 4. percentage magic resistance penetration
            var magicresist = Magicresist;

            // 1
            // 2
            // 3
            if (magicresist >= source.Magicresist)
            {
                magicresist -= source.Magicresist;
            }
            else
            {
                magicresist = 0;
            }
            // 4
            var physMult = 100 / (100 + myarmor);
            var magicMult = 100 / (100 + magicresist);
            var dmg = damage.Magicdamage * magicMult + damage.Physicaldamage * physMult + damage.Truedamage;
            foreach (var buff in Buffs.Where(buff => buff.Shield > 0))
            {
                if (dmg > buff.Shield)
                {
                    dmg -= buff.Shield;
                    Log.ToLog("Partitional absorbing: " + buff.Shield);
                    buff.Shield = 0;
                }
                else
                {
                    buff.Shield -= dmg;
                    Log.ToLog("Absorbing: " + dmg);
                    dmg = 0;
                }
            }
            foreach (var buff in Buffs)
            {
                dmg -= buff.FlatPhysicalDamageReduced;
                Log.ToLog("Incoming damage reduced by buff:" + buff.Name + " by " + buff.FlatPhysicalDamageReduced);
            }
            dmg = Inventory.Aggregate(dmg, (current, item) => current - item.FlatPhysicalDamageReduced);
            Health -= dmg;
        }
    }
}