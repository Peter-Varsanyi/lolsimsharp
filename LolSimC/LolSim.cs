using System;
using System.Collections.Generic;

namespace LolSimC
{
    internal class LolSim
    {
        private readonly String[] _campNames;
        private readonly Player _currentPlayer;
        private String _champion;
        private String _route;

        public LolSim(string character, string map)
        {
            _champion = character;
            _route = map;
            _campNames = new[] {"Blue", "Wolves"}; // "Wolves", "Wraiths", "Red", "Doublegolem" };
            if (character.Equals("Amumu"))
            {
                _currentPlayer = new Amumu();
            }
            _currentPlayer.Name = "Gwelican";

            var mastery = new Mastery {Permanent = true, Armor = 6, FlatPhysicalDamageReduced = 4, Name = "Mastery"};
            var markRune = new Buff {Armor = 9*.91};
            var item1 = new Item {Armor = 18};
            _currentPlayer.AddItem(item1);
            _currentPlayer.ApplyBuff(mastery);
            _currentPlayer.ApplyBuff(markRune);
//            Log.ToLog("WTF: "+current_player.Skills.Select(t => t.Cooldown).Min());
            Log.ToLog("" + _currentPlayer.Armor);
            _currentPlayer.Level = 18;
            Log.ToLog("" + _currentPlayer.Armor);
            //          Console.ReadKey();
        }


        public List<Base> GenerateCamp(Base player, string name)
        {
            var camp = new List<Base>();
            var golem = new Creep
                            {
                                Name = "BlueGolem",
                                MaxHealth = 1275 + 175,
                                Health = 1275 + 175,
                                Attackdamage = 110,
                                Armor = 24,
                                Attackspeed = 0.613,
                                Movementspeed = 200,
                                Nextswing = 5
                            };

            var lizard1 = new Creep
                              {
                                  Name = "Lizard1",
                                  MaxHealth = 350,
                                  Health = 350,
                                  Attackdamage = 18,
                                  Armor = 8,
                                  Attackspeed = 0.679,
                                  Nextswing = 5
                              };

            var lizard2 = new Creep
                              {
                                  Name = "Lizard2",
                                  MaxHealth = 350,
                                  Health = 350,
                                  Attackdamage = 18,
                                  Armor = 8,
                                  Attackspeed = 0.679,
                                  Nextswing = 5
                              };
            camp.Add(lizard1);
            camp.Add(golem);
            camp.Add(lizard2);
            //player.RegisterAutoattackCallBack(Player.)
            //current_player.RegisterAutoattackCallBack((current_player as LeeSin).TriggerAutoattack);
            //player.RegisterAutoattackCallBack((LeeSin)player.TriggerAutoattack())
            //player.RegisterAutoattackCallBack(golem.TestAutoAttack);
            //golem.RegisterAutoattackCallBack(player.TestAutoAttack);
            camp.Add(player);
            return camp;
        }

        public Base LookForTarget(Base src, List<Base> camp)
        {
            Base target = null;
            foreach (var t in camp)
            {
                if (src.IsCreep() && !t.IsCreep()) return t;
                if (!src.IsCreep() && (target == null || target.MaxHealth < t.MaxHealth) && t.IsCreep())
                {
                    target = t;
                }
            }
            return target;
        }

        public void Run()
        {
            foreach (var campName in _campNames)
            {
                // Runs the current camp
                Log.ToLog("################### " + campName);
                var camp = GenerateCamp(_currentPlayer, campName);
                try
                {
                    // If there is a creep...
                    while (camp.Count > 1)
                    {
                        double nextAction = 15; // maximum time till the next action
                        for (var i = 0; i < camp.Count; i++)
                        {
                            // 
                            // Checking target && searching for target
                            //
                            if (camp[i].Target == null || camp[i].Target.Health < 0)
                            {
                                camp[i].Target = LookForTarget(camp[i], camp);
                                if (camp[i].Target == null) throw new RunException("Player dead"); // Exception because can't break from inner iteration
                                Log.ToLog(camp[i], camp[i].Target, "targetfind");
                            }


                            //
                            // Handling skills
                            //
                            camp[i].ActivateSkills(camp);

                            //
                            // Handling autoattacks
                            //
                            camp[i].ActivateAutoAttack();


                            //
                            // calculate nextaction from skillcooldown
                            //

                            if (camp[i].GetLowestSkillCooldown() < nextAction)
                            {
                                nextAction = camp[i].GetLowestSkillCooldown();
                            }
                            // calculate nextaction from autoattack
                            if (nextAction > camp[i].Nextswing) nextAction = camp[i].Nextswing;

                            //
                            // remove dead objects
                            //
                            if (camp[i].Health >= 0) continue; // if alive, continue
                            Log.ToLog(camp[i], "objectdelete");
                            camp.RemoveAt(i);
                            nextAction = 0; // if we finished dont add time at the end
                        }
                        foreach (var t in camp)
                        {
                            t.CdReduce(nextAction);
                        }

                        Log.AddTime(nextAction); // adding time for logger
                    }
                }
                catch (RunException e)
                {
                    if (e.Errormessage == "Player dead")
                    {
                        return;
                    }
                }
            }
        }
    }
}