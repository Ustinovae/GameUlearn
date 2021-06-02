using System;
using NUnit.Framework;

namespace GetOut.Models.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void MovePlayer()
        {
            var player = new Player(0, 0, 30, 60, "Player");
            var map = GameMap.ParseFromText("P__\n___\n___", null);
            player.StartMove(1, 0);
            player.Act(map);
            player.StopMove();
            Assert.AreEqual(10, player.PosX);
        }

        [Test]
        public void DontMove()
        {
            var map = GameMap.ParseFromText("Pf_\n___\n___", null);
            var player = map.Player;
            player.TakeAnFurniture(map);
            player.StartMove(1, 0);
            player.Act(map);
            player.StopMove();
            Assert.AreEqual(0, player.PosX);
        }

        [Test]
        public void TakeFurniturePlayerWhenDontMove()
        {
            var map = GameMap.ParseFromText("Pf_\n___\n___", null);
            var player = map.Player;
            player.TakeAnFurniture(map);
            player.StartMove(1, 0);
            player.Act(map);
            player.StopMove();
            Assert.AreEqual(0, player.PosX);
        }

        [Test]
        public void TakeFurniturePlayer()
        {
            var map = GameMap.ParseFromText("P__\n_f_\n___", null);
            var player = map.Player;
            player.TakeAnFurniture(map);
            player.StartMove(0, 1);
            player.Act(map);
            player.StopMove();
            Assert.AreEqual(40, map.EntitiesOnMap[0].PosY);
        }

        [Test]
        public void ReleaseFurniturePlayer()
        {
            var map = GameMap.ParseFromText("P__\n_f_\n___", null);
            var player = map.Player;
            player.TakeAnFurniture(map);
            player.ReleaseObject();
            player.StartMove(0, 1);
            player.Act(map);
            player.StopMove();
            Assert.AreEqual(30, map.EntitiesOnMap[0].PosY);
        }
    }
}
