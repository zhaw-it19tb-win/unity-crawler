using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ShootingTest
    {
        private static void addTag(string tag)
        {
            var tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var tagsProp = tagManager.FindProperty("tags");
            var layersProp = tagManager.FindProperty("layers");

            var found = false;
            for (var i = 0; i < tagsProp.arraySize; i++)
            {
                var t = tagsProp.GetArrayElementAtIndex(i);
                if (!t.stringValue.Equals(tag)) continue;
                found = true;
                break;
            }

            if (!found)
            {
                tagsProp.InsertArrayElementAtIndex(0);
                var n = tagsProp.GetArrayElementAtIndex(0);
                n.stringValue = tag;
            }

            var layerName = "my_layer";

            var sp = layersProp.GetArrayElementAtIndex(10);
            if (sp != null) sp.stringValue = layerName;
            tagManager.ApplyModifiedProperties();
        }

        [UnityTest]
        public IEnumerator ShootingTestIfBulletMoves()
        {
            var player = Object.Instantiate(
                new GameObject("Player")
                {
                    hideFlags = HideFlags.None,
                    layer = 0,
                    isStatic = false,
                    tag = "Player"
                }
            );
            player.transform.position.Set(12, 14, 0);
            
            var enemy = Object.Instantiate(
                new GameObject("enemy")
            );

            enemy.transform.position.Set(-12, 14, 0);
            enemy.AddComponent<Shooting>();
            enemy.GetComponent<Shooting>().firePoint = enemy.transform;
            addTag("Bullet");
            
            var bulletPrefab = Object.Instantiate(
                new GameObject("bullet")
                {
                    hideFlags = HideFlags.None,
                    layer = 0,
                    isStatic = false,
                    tag = "Bullet"
                }
            );
            
            bulletPrefab.AddComponent<Rigidbody2D>();
            enemy.GetComponent<Shooting>().bulletPrefab = bulletPrefab;
    
            yield return new WaitForEndOfFrame();
            
            var transform1 = bulletPrefab.transform.position;
            yield return new WaitForSeconds(.25f);
            var transform2 = bulletPrefab.transform.position;
            
            // Test if bullet moved
            Assert.AreEqual(0.5, (transform1 - transform2).magnitude, .2f);
        }
    }
}