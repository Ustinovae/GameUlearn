using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GetOut.Models.Tests
{
    [TestFixture]
    class LevelsManagerTests
    {
        [Test]
        public void CheckStartGame()
        {
            var levelsManager = new LevelsManager();
            levelsManager.GetNextLevel();
            Assert.AreEqual(0, levelsManager.CurrentLevel.NumberLevel);
        }

        [Test]
        public void CheckRestart()
        {
            var levelsManager = new LevelsManager();
            levelsManager.GetNextLevel();
            levelsManager.Restart();
            Assert.AreEqual(0, levelsManager.CurrentLevel.NumberLevel);
        }
    }
}
