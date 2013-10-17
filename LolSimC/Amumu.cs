using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LolSimC
{
    public class Amumu : Player
    {
        public Amumu()
        {
            Attackdamage = 47;
            Attackspeed = 0.638;
            Attackrange = 125;
            Maxresource = 220;
            Resource = 220;
            MaxHealth = 472;
            Health = 472;
            Healthregen = 7.45;
            Resourceregen = 50;
            Armor = 18;
            Magicresist = 30;
            Movementspeed = 325;
            BonusAttackdamage = 0;
            var w = new Skill
                        {
                            Toggle = true,
                            Basedamage = new Damage(0, 8, 0),
                            Name = "Despair",
                            Maxcooldown = 1,
                            Cooldown = 0,
                            PercentageDamage = 1.5,
                            Aoe = true
                        };

            //Skill e = new Skill();
            //e.Name = "Tantrum";
            //e.Cooldown = 0;
            //e.Maxcooldown = 10;
            //e.Basedamage = new Damage(0,75,0);
            //e.Abilitypowerscale = 0.5;
            //this.RegisterDamageTakenCallBack(TriggerEPassive);
            Skills.Add(w);

            var smite = new Skill {Basedamage = new Damage(420, 0, 0), Maxcooldown = 75};
            Skills.Add(smite);

        }
        public void TriggerEPassive(Base source)
        {
            var e = Skills.Find(item => item.Name == "Tantrum");
            e.Cooldown -= 0.5;
            Log.ToLog(this, "skillchange", "0.5 from Tantrum, " + String.Format("{0:0.0}/{1:0}", e.Cooldown, e.Maxcooldown));
            //Log.ToLog("E Cd reduced 0.5 sec! " + e.Cooldown);

        }
    }
}
