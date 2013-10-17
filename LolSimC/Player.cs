namespace LolSimC
{
    public class Player : Base
    {

        public override bool IsCreep()
        {
            return false;
        }

        public override void TakeDamage(Base source, Damage dmg)
        {
            //Log.ToLog("Player take damege");
            base.TakeDamage(source, dmg);
        }

        public override void ApplyBuff(Buff buffname)
        {
            base.ApplyBuff(buffname);
        }

        public override void CdReduce(double time)
        {
            foreach (var x in Skills)
            {
                x.ReduceCooldown(time);
            }
            base.CdReduce(time);
        }

    }
}