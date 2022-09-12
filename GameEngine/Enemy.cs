using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public abstract class Enemy
    {
        public string Name { get; set; }
        public abstract double TotalSpecialPower { get; }
        public abstract double SpecialPowerUses { get; }
        public double SpecialAttackPower => TotalSpecialPower / SpecialPowerUses;
    }
}
