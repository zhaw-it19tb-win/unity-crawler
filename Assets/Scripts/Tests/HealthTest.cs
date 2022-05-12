using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class Slider : UnityEngine.UI.Slider
    {
    }

    public class HealthTest
    {
        [Test]
        public void TestTakesDamage()
        {
            var gameObject = new GameObject("Test");
            gameObject.AddComponent<Health>();
            var healthComponent = gameObject.GetComponent<Health>() as Health;

            var sliderObject = new GameObject("Slider");
            sliderObject.AddComponent<Slider>();
            healthComponent.HealthBar = sliderObject.GetComponent<Slider>();

            healthComponent.TakeDamage(1);
            Assert.AreEqual(expected: 99, healthComponent._health);

            healthComponent.TakeDamage(2);
            Assert.AreEqual(97, healthComponent._health);
        }

        [Test]
        public void TestDie()
        {
            bool hasDied = false;

            var gameObject = new GameObject("Test");
            gameObject.AddComponent<Health>();
            var healthComponent = gameObject.GetComponent<Health>() as Health;
            healthComponent.OnDied += () => hasDied = true;

            var sliderObject = new GameObject("Slider");
            sliderObject.AddComponent<Slider>();
            healthComponent.HealthBar = sliderObject.GetComponent<Slider>();

            healthComponent.TakeDamage(5000);
            Assert.AreEqual(expected: true, actual: hasDied);
        }
    }
}
