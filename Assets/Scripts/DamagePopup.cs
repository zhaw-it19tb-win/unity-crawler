using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour {

  // Create a Damage Popup
  public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit) {
    var gameUtil = Object.FindObjectOfType<GameUtil>();
    Transform damagePopupTransform = Instantiate(gameUtil.DamagePopup.gameObject.transform, position, Quaternion.identity);
    DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
    damagePopup.Setup(damageAmount, isCriticalHit);

    return damagePopup;
  }

  private static int sortingOrder = 999;

  private const float DISAPPEAR_TIMER_MAX = 1f;

  private TextMeshPro textMesh;
  private float disappearTimer;
  private Color textColor;
  private Vector3 moveVector;

  private void Awake() {
    textMesh = transform.GetComponent<TextMeshPro>();
  }

  public void Setup(int damageAmount, bool isCriticalHit) {
    if (damageAmount < 0) {
      textMesh.SetText((-damageAmount).ToString());
      textMesh.fontSize = 22;
      textColor = Color.green;
    }
    else if (!isCriticalHit) {
      // Normal hit
      textMesh.SetText(damageAmount.ToString());
      textMesh.fontSize = 20;
      textColor = Color.red;
    }
    else {
      // Critical hit
      textMesh.SetText(damageAmount.ToString());
      textMesh.fontSize = 26;
      textColor = Color.yellow;
    }
    textMesh.color = textColor;
    disappearTimer = DISAPPEAR_TIMER_MAX;

    sortingOrder++;
    textMesh.sortingOrder = sortingOrder;

    moveVector = new Vector3(.7f, 1) * 60f / 120f;
  }

  private void Update() {
    transform.position += moveVector * Time.deltaTime;
    moveVector -= moveVector * 8f * Time.deltaTime / 120f;

    if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
      // First half of the popup lifetime
      float increaseScaleAmount = 0.18f;
      transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
    }
    else {
      // Second half of the popup lifetime
      float decreaseScaleAmount = 0.18f;
      transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
    }

    disappearTimer -= Time.deltaTime;
    if (disappearTimer < 0) {
      // Start disappearing
      float disappearSpeed = 3f;
      textColor.a -= disappearSpeed * Time.deltaTime;
      textMesh.color = textColor;
      if (textColor.a < 0) {
        Destroy(gameObject);
      }
    }
  }

}
