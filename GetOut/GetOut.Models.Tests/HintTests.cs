using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace GetOut.Models.Tests
{
    [TestFixture]
    class HintTests
    {
        [Test]
        public void Create()
        {
            var hint = new Hint(0, 0, new Size(100, 100), "hint", null);
            Assert.IsNotNull(hint);
        }

        [Test]
        public void Activate()
        {
            var hint = new Hint(0, 0, new Size(100, 100), "hint", null);
            hint.SetActive(true);
            Assert.IsTrue(hint.GetStatus());
        }

        [Test]
        public void Block()
        {
            var hint = new Hint(0, 0, new Size(100, 100), "hint", null);
            hint.SetActive(true);
            hint.SetActive(false);
            Assert.IsFalse(hint.GetStatus());
        }

    }
}
