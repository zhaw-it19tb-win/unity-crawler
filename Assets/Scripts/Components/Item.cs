using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour {

    public PotionType potionType;

    private PlayerInput input;
    private bool itemPicked;
    private bool interaction = false;

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
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            PotionType type = potionType;

            switch (type) {
                case PotionType.Health:
                    Health health = gameObject.GetComponent<Health>();
                    if (health.enabled) {
                        health.IncreaseHealth(15);
                        itemPicked = true;
                    }
                    break;
                case PotionType.Strength:
                    AbilityController abilityController = gameObject.GetComponent<AbilityController>();
                    if (abilityController.enabled) {
                        abilityController.IncreaseCriticalDamage(0.05f);
                        itemPicked = true;
                    }
                    break;
                case PotionType.Tempo:
                    PlayerInputController playerController = gameObject.GetComponent<PlayerInputController>();
                    if (playerController.enabled) {
                        playerController.IncreaseSpeed(1f/3f);
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
