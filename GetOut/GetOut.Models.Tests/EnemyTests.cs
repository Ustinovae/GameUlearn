using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace GetOut.Models.Tests
{
    [TestFixture]
    class EnemyTests
    {
        [Test]
        public void Go()
        {
            var map = Map.ParseFromText("e  \n   \n   ", null);
            map.enemy.MoveTo(new Point(2 * Map.CellSize, 2 * Map.CellSize), map);
            Assert.AreEqual(5, map.enemy.PosX);
        }

        [Test]
        public void Go1()
        {
            var map = Map.ParseFromText("e##\n## \n   ", null);
            map.enemy.MoveTo(new Point(2 * Map.CellSize, 2 * Map.CellSize), map);
            Assert.AreEqual(0, map.enemy.PosX);
        }

        //[Test]
        //public void Go2()
        //{
        //    var map = Map.FromText(
        //        "e     #____________\n" +
        //        "  P___#____________\n" +
        //        "______#_____ f__f__\n" +
        //        "______#____________\n" +
        //        "______#            \n" +
        //        "      #            \n" +
        //        "      #    #       \n" +
        //        "###  ##    ########\n" +
        //        "                   \n" +
        //        "                   \n" +
        //        "###f ##            \n" +
        //        "      #            \n" +
        //        "      #            \n" +
        //        "      #            \n" +
        //        "      #            \n" +
        //        "      #            \n" +
        //        "      #            \n" +
        //        "      #            ", null);
        //    //for(var i = 0; i < 2; i++) 
        //        map.enemy.Go(new Point(map.Player.PosX, map.Player.PosY), map);
        //    Assert.AreEqual(new Point(map.Player.PosX, map.Player.PosY), new Point(map.enemy.PosX, map.enemy.PosY));
        //}
    }
}
