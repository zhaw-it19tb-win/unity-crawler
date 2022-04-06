using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
  void OnTriggerStay2D(Collider2D collider)
  {
      if (collider.gameObject.tag == "Player")
      {
        Vector3 temp = new Vector3(0.1f,1.0f,0.5f);
        collider.gameObject.transform.position += temp;
      }
  }
}
