using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Tests
{
    [TestClass]
    public class BossEnemyShould
    {
        [TestMethod]
        public void HaveCorrectSpecialAtackPower()
        {
            var sut = new BossEnemy();

            Assert.AreEqual(166.6, sut.SpecialAttackPower, 0.07);
        }

        
    }
}
