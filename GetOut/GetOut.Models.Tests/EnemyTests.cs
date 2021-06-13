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
            var map = GameMap.ParseFromText("e  \n   \n   ", null, null);
            
            map.EnemisOnMap[0].MoveTo(new Point(1 * GameMap.CellSize, 1 * GameMap.CellSize), map);
            Assert.AreEqual(10, map.EnemisOnMap[0].PosX);
        }

        [Test]
        public void WhenNoPath()
        {
            var map = GameMap.ParseFromText("e##\n## \n   ", null, null);
            map.EnemisOnMap[0].MoveTo(new Point(2 * GameMap.CellSize, 2 * GameMap.CellSize), map);
            Assert.AreEqual(0, map.EnemisOnMap[0].PosY);
        }

        [Test]
        public void WhenNoPathWithoutObjOnMap()
        {
            var map = GameMap.ParseFromText("e__\n___\n___", null, null);
            map.EnemisOnMap[0].MoveTo(new Point(1 * GameMap.CellSize, 1 * GameMap.CellSize), map);
            Assert.AreEqual(10, map.EnemisOnMap[0].PosX);
        }

        [Test]
        public void WithObjOnMap()
        {
            var map = GameMap.ParseFromText(
                "e     #____________\n" +
                "  _________________\n" +
                "__________p_ f__f__\n" +
                "______#____________\n", null, null);
            for (var i = 0; i < 36; i++)
                map.EnemisOnMap[0].MoveTo(new Point(map.Player.PosX, map.Player.PosY), map);
            Assert.AreEqual(new Point(100, 0), new Point(map.EnemisOnMap[0].PosX, map.EnemisOnMap[0].PosY));
        }

        [Test]
        public void NoPathWithObjOnMap()
        {
            var map = GameMap.ParseFromText(
                "e     #____________\n" +
                "  ____#____________\n" +
                "______#_____ f__f__\n" +
                "______#_______p____\n", null, null);
            for (var i = 0; i < 15; i++)
                map.EnemisOnMap[0].MoveTo(new Point(map.Player.PosX, map.Player.PosY), map);
            Assert.AreEqual(new Point(25, 0), new Point(map.EnemisOnMap[0].PosX, map.EnemisOnMap[0].PosY));
        }

        [Test]
        public void BigTest()
        {
            var map = GameMap.ParseFromText(
                "e     #____________\n" +
                "  p___#____________\n" +
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
                "      #            ", null, null);
            for (var i = 0; i < 15; i++)
                map.EnemisOnMap[0].MoveTo(new Point(map.Player.PosX, map.Player.PosY), map);
            Assert.AreEqual(new Point(map.Player.PosX, map.Player.PosY), new Point(map.EnemisOnMap[0].PosX, map.EnemisOnMap[0].PosY));
        }
    }
}
