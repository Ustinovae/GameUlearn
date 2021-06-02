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
        public void WithoutObjOnMap()
        {
            var map = GameMap.ParseFromText("e  \n   \n   ", null);
            map.enemy.MoveTo(new Point(1 * GameMap.CellSize, 1 * GameMap.CellSize), map);
            Assert.AreEqual(10, map.enemy.PosX);
        }

        [Test]
        public void WhenNoPath()
        {
            var map = GameMap.ParseFromText("e##\n## \n   ", null);
            map.enemy.MoveTo(new Point(2 * GameMap.CellSize, 2 * GameMap.CellSize), map);
            Assert.AreEqual(0, map.enemy.PosY);
        }

        [Test]
        public void WhenNoPathWithoutObjOnMap()
        {
            var map = GameMap.ParseFromText("e__\n___\n___", null);
            map.enemy.MoveTo(new Point(1 * GameMap.CellSize, 1 * GameMap.CellSize), map);
            Assert.AreEqual(10, map.enemy.PosX);
        }

        [Test]
        public void WithObjOnMap()
        {
            var map = GameMap.ParseFromText(
                "e     #____________\n" +
                "  _________________\n" +
                "__________P_ f__f__\n" +
                "______#____________\n", null);
            for (var i = 0; i < 36; i++)
                map.enemy.MoveTo(new Point(map.Player.PosX, map.Player.PosY), map);
            Assert.AreEqual(new Point(map.Player.PosX, map.Player.PosY), new Point(map.enemy.PosX, map.enemy.PosY));
        }

        [Test]
        public void NoPathWithObjOnMap()
        {
            var map = GameMap.ParseFromText(
                "e     #____________\n" +
                "  ____#____________\n" +
                "______#_____ f__f__\n" +
                "______#_______P____\n", null);
            for (var i = 0; i < 15; i++)
                map.enemy.MoveTo(new Point(map.Player.PosX, map.Player.PosY), map);
            Assert.AreEqual(new Point(0, 0), new Point(map.enemy.PosX, map.enemy.PosY));
        }

        [Test]
        public void BigTest()
        {
            var map = GameMap.ParseFromText(
                "e     #____________\n" +
                "  P___#____________\n" +
                "______#_____ f__f__\n" +
                "______#____________\n" +
                "______#            \n" +
                "      #            \n" +
                "      #    #       \n" +
                "###  ##    ########\n" +
                "                   \n" +
                "                   \n" +
                "###f ##            \n" +
                "      #            \n" +
                "      #            \n" +
                "      #            \n" +
                "      #            \n" +
                "      #            \n" +
                "      #            \n" +
                "      #            ", null);
            for (var i = 0; i < 15; i++)
                map.enemy.MoveTo(new Point(map.Player.PosX, map.Player.PosY), map);
            Assert.AreEqual(new Point(map.Player.PosX, map.Player.PosY), new Point(map.enemy.PosX, map.enemy.PosY));
        }
    }
}
