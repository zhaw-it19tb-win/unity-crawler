using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour {
  private Health health;


  void Start() {
        Debug.Log("Start is called");
    health.OnDied += OnDied;
  }

  void Awake() {
    Debug.Log("Awake is called");
    health = GetComponent<Health>();
  }

  private void OnDied() {
    FindObjectOfType<AudioManager>().Play("Dead");
    Debug.Log("You died.");
  }
}
