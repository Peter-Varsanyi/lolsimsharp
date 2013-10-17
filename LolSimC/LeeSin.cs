namespace LolSimC
{
    internal class LeeSin : Player
    {
        public LeeSin()
        {
            Attackdamage = 55.8;
            Attackspeed = 0.651;
            Attackrange = 125;
            Maxresource = 200;
            Resource = 200;
            MaxHealth = 428;
            Health = 428;
            Healthregen = 6.25;
            Resourceregen = 50;
            Armor = 30;
            Magicresist = 30;
            Movementspeed = 325;
            BonusAttackdamage = 60;

            var q = new Skill
                        {
                            Name = "Sonic Wave",
                            Level = 1,
                            Phase = 1,
                            Maxcooldown = 11,
                            Cooldown = 0,
                            Aoe = false,
                            Bonusattackdamagescale = 0.9,
                            Basedamage = new Damage(0, 0, 50)
                        };
            Skills.Add(q);
            RegisterDamageTakenCallBack(TriggerAutoattack);
        }

        public void TriggerAutoattack(Base source)
        {
            Skill q = Skills.Find(item => item.Name == "Sonic Wave");
            //Log.ToLog(""+q.Maxcooldown);

            Log.ToLog("Cd reduced 1 sec! " + q.Cooldown);
            q.Cooldown -= 1;
        }
    }
}