using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GetOut.Models.Tests
{
    [TestFixture]
    class GameMapTests
    {
        [Test]
        public void CreateMap()
        {
            var map = GameMap.ParseFromText("e  \n   \n   ");
            Assert.IsNotNull(map);
        }

        [Test]
        public void CheckParseText()
        {
            var map = GameMap.ParseFromText("e##\n___\n___");
            Assert.AreEqual(3, map.EntitiesOnMap.Count);
        }

        [Test]
        public void CheckParseTextWitnPlayer()
        {
            var map = GameMap.ParseFromText("e##\n_P_\n___");
            Assert.AreEqual(3, map.EntitiesOnMap.Count);
        }

        [Test]
        public void CreateEntities()
        {
            var map = GameMap.ParseFromText("e##\n_P_\n___");
            Assert.IsNotNull(map.EntitiesOnMap);
        }

        [Test]
        public void CreateHints()
        {
            var map = GameMap.ParseFromText("e##\n_P_\n_h_");
            Assert.IsNotNull(map.HintOnLevels);
        }

    }
}
