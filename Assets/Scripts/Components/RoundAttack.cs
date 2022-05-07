using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundAttack : MonoBehaviour {
  private GameObject player;
  public Transform Effect;
  // Start is called before the first frame update
  void Start() {
    GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
    player = playerObjs[0];
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown("space")) {
      var vfx = Instantiate(Effect, new Vector3(player.transform.position.x, player.transform.position.y, 0), Quaternion.identity);
      
      /* vfx.localScale = new Vector3(0.2f,0.2f,0.2f);
       foreach (var renderer in vfx.GetComponentsInChildren<Renderer>()) {
         renderer.sortingOrder = 100;
         //renderer.sort = 100;
       }*/
      Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, 1.0f);
      foreach (Collider2D collider in colliders) {
        collider.gameObject.GetComponent<Health>()?.TakeDamage(10);
      }
    }
  }

}
