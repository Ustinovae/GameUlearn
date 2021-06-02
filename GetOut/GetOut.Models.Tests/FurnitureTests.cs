using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GetOut.Models.Tests
{
    [TestFixture]
    class FurnitureTests
    {
        [Test]
        public void CheckMove()
        {
            var map = GameMap.ParseFromText("_f_\n___\n___");
            var f = (Furniture)map.EntitiesOnMap[0];
            f.Move(10, 0, map);
            Assert.AreEqual(40, f.PosX);
        }
    }
}
