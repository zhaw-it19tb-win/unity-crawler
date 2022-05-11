using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour {

    public PotionType potionType;

    private PlayerInput input;
    private bool itemPicked, interaction = false;

    void OnEnable() {
        input.Enable();
    }

    void OnDisable() {
        input.Disable();
    }

    void Awake() {
        input = new PlayerInput();
        input.Player.PickItems.performed += OnItemPickedPerformed;
        input.Player.PickItems.canceled += OnItemPickedCancelled;
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player" && interaction) {
            // remove the object which will be interacted with
            PotionType type = potionType;

            switch (type) {
                case PotionType.Health:
                    Health health = FindObjectOfType<Health>();
                    if (health.enabled) {
                        health.IncreaseHealth(15);
                        itemPicked = true;
                    }
                    break;
                case PotionType.Strength:
                    // todo change that strength grows by approx. 5%
                        itemPicked = true;
                    break;
                case PotionType.Tempo:
                    Movement movement = FindObjectOfType<Movement>();
                    if (movement.enabled) {
                        movement.IncreaseSpeed(0.1f);
                        itemPicked = true;
                    }
                    break;
            }
        }
    }
    private void OnItemPickedPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        interaction = true;
    }

    private void OnItemPickedCancelled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        interaction = false;
    }

    // Update is called once per frame
    void Update() {
        if (itemPicked) {
            Destroy(this.gameObject);
        }
    }

    #region Enumeration

    // defines an enumeration for the given potion type.
    public enum PotionType {
        Health = 0,
        Strength = 1,
        Tempo = 2,
    }

    #endregion
}
