using System;

namespace LolSimC
{
    public struct Damage
    {
        private double _magicdamage, _physicaldamage;
        private double _truedamage;

        public Damage(double trueDamage, double magicDamage, double physicalDamage)
        {
            _truedamage = trueDamage;
            _magicdamage = magicDamage;
            _physicaldamage = physicalDamage;
        }

        public double Physicaldamage
        {
            get { return _physicaldamage; }
            set { _physicaldamage = value; }
        }

        public double Magicdamage
        {
            get { return _magicdamage; }
            set { _magicdamage = value; }
        }

        public double Truedamage
        {
            get { return _truedamage; }
            set { _truedamage = value; }
        }

        public override string ToString()
        {
            return "T: " + _truedamage + " M: " + _magicdamage + " P:" + _physicaldamage;
        }

        public static Damage operator +(Damage dmg1, Damage dmg2)
        {
            return new Damage(dmg1.Truedamage + dmg2.Truedamage, dmg1.Magicdamage + dmg2._magicdamage,
                              dmg1.Physicaldamage + dmg2._physicaldamage);
        }

        public static Boolean operator ==(Damage dmg1, Damage dmg2)
        {
            return dmg1.Equals(dmg2);
        }

        public static Boolean operator !=(Damage dmg1, Damage dmg2)
        {
            return !dmg1.Equals(dmg2);
        }

        public bool Equals(Damage dmg)
        {
            return dmg._truedamage.Equals(_truedamage) && dmg._magicdamage.Equals(_magicdamage) &&
                   dmg._physicaldamage.Equals(_physicaldamage);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Damage && Equals((Damage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _truedamage.GetHashCode();
                result = (result*397) ^ _magicdamage.GetHashCode();
                result = (result*397) ^ _physicaldamage.GetHashCode();
                return result;
            }
        }
    }
}