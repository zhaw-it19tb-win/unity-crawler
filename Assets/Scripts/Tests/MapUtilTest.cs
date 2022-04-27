using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class MapUtilTest
    {
        [Test]
        public void MapUtilTestGetCenterOfTile()
        {
            var result = MapUtil.getCentreOfTile(12, 15);

            Assert.AreEqual(-1.5, result.x, .01);
            Assert.AreEqual(7, result.y, .01);
            Assert.AreEqual(0, result.z, .01);
        }

        [Test]
        public void MapUtilTestGetCenterOfTileNegative()
        {
            var result = MapUtil.getCentreOfTile(-17, 0);
            
            Assert.AreEqual(-8.5, result.x, .01);
            Assert.AreEqual(-4, result.y, .01);
            Assert.AreEqual(0, result.z, .01);
        }
    }
}