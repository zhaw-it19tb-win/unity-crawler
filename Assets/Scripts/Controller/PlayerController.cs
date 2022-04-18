using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour {
  private Health health;

  void Start() {
    health = GetComponent<Health>();
    health.OnDied += OnDied;
  }

  private void OnDied() {
    //SoundEffect
    FindObjectOfType<AudioManager>().Play("dead");
    Debug.Log("You died.");

  }
}
