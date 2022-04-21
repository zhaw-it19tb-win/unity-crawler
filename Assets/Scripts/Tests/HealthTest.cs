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
        GameObject obj = new GameObject("Test");
        Slider healthBar = new Slider();
        obj.AddComponent<Health>();
        Health healthComponent = obj.GetComponent(typeof(Health)) as Health;
        healthComponent.HealthBar = healthBar;
        healthComponent.TakeDamage(1);
        Assert.AreEqual(99, healthComponent.health);
    }
}
