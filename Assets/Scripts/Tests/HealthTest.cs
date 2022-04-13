using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestTakesDamage()
    {
        Health healthBehaviour = new Health();
        healthBehaviour.TakeDamage(1);
        Assert.AreEqual(99, healthBehaviour.health);
    }
}
