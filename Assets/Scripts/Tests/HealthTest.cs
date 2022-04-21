using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Runtime.Serialization;
using System.Reflection;

public class Slider : UnityEngine.UI.Slider
{
}

public class HealthTest
{
  [Test]
  public void TestTakesDamage()
  {
    GameObject gameObject = new GameObject("Test");
    gameObject.AddComponent<Health>();
    Health healthComponent = gameObject.GetComponent<Health>() as Health;

    GameObject sliderObject = new GameObject("Slider");
    sliderObject.AddComponent<Slider>();
    healthComponent.HealthBar = sliderObject.GetComponent<Slider>();

    healthComponent.TakeDamage(1);
    Assert.AreEqual(99, healthComponent.health);

    healthComponent.TakeDamage(2);
    Assert.AreEqual(97, healthComponent.health);
  }

  [Test]
  public void TestDie()
  {
		bool hasDied = false;

		GameObject gameObject = new GameObject("Test");
    gameObject.AddComponent<Health>();
    Health healthComponent = gameObject.GetComponent<Health>() as Health;
		healthComponent.OnDied += () => hasDied = true;

    GameObject sliderObject = new GameObject("Slider");
    sliderObject.AddComponent<Slider>();
    healthComponent.HealthBar = sliderObject.GetComponent<Slider>();

    healthComponent.TakeDamage(5000);
		Assert.AreEqual(true, hasDied);
  }
}
