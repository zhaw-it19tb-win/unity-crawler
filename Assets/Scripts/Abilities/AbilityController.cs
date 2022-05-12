using Assets.Scripts.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

  public GameObject player;
  public Ability AutoAttackAbility;
  public Ability PrimaryAbility;
  public Ability SecondaryAbility;
  public Ability ThirdAbility;
  public Image AutoAttackAbilityHotbarImage;
  public Image PrimaryAbilityAbilityHotbarImage;
  public Image SecondaryAbilityAbilityHotbarImage;
  public Image ThirdAbilityAbilityHotbarImage;
  private Mana mana;

  private class AbilityCast {
    public Ability ability;
    public Image cooldownSlot; 
  }

  private void Start() {
    mana = player.GetComponent<Mana>();
  }

  private List<AbilityCast> _abilityList = null;

  private List<AbilityCast> GetAbilityList() {
    if (_abilityList == null) {
      _abilityList = new List<AbilityCast>();
      InitializeAbility(AutoAttackAbility, AutoAttackAbilityHotbarImage);
      InitializeAbility(PrimaryAbility, PrimaryAbilityAbilityHotbarImage);
      InitializeAbility(SecondaryAbility, SecondaryAbilityAbilityHotbarImage);
      InitializeAbility(ThirdAbility, ThirdAbilityAbilityHotbarImage);
    }
    return _abilityList;
  }

  private void InitializeAbility(Ability ability, Image hotbar = null) {
    if (ability != null) {
      if (hotbar != null && ability.Icon != null) {
        hotbar.sprite = ability.Icon; // https://www.youtube.com/watch?v=wtrkrsJfz_4
        hotbar.type = Image.Type.Filled;
        hotbar.fillMethod = Image.FillMethod.Radial360;
        hotbar.fillOrigin = 2;
        hotbar.fillAmount = 1;
        hotbar.fillClockwise = true;
      }
      _abilityList.Add(new AbilityCast() { ability = ability, cooldownSlot = hotbar });
    }
  }

  // Update is called once per frame
  protected void Update() {
    foreach (var ability in GetAbilityList()) {
      ability.ability.UpdateCast();
      ability.cooldownSlot.fillAmount = ability.ability.GetCooldownPercentage();
    }

    bool isCasting = GetAbilityList().Any(x => x.ability.AbilityCurrentlyLockingOtherAbilities);
    // Input Handling
    if (Input.GetMouseButtonDown(0)) {
      CastAbility(AutoAttackAbility);
    }
    if (isCasting) {
      return;
    }
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      CastAbility(PrimaryAbility);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2)) {
      CastAbility(SecondaryAbility);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha3)) {
      CastAbility(ThirdAbility);
    }
  }

  

  private Vector2 GetTargetPosition() {
    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return new Vector2(mousePosition.x, mousePosition.y);
  }
  private Vector2 GetOriginPosition() {
    return new Vector2(player.transform.position.x, player.transform.position.y);
  }

  private void CastAbility(Ability ability) {
    if (mana.CurrentMana > ability.manaCost) {
      if (ability.TryCast(GetOriginPosition(), GetTargetPosition())) {
        mana.DecreaseMana(ability.manaCost);
      }
    }
  }
}
