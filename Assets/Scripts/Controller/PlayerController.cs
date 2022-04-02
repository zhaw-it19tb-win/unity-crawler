using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour {
  private Health health;

  void Start() {
    health = GetComponent<Health>();
    health.OnDied += OnDied;
  }

  private void OnDied() {
    Debug.Log("You died.");

  }
}