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
  public Image AutoAttackAbilityHotbarImage;
  public Image PrimaryAbilityAbilityHotbarImage;

  private class AbilityCast {
    public Ability ability;
    public Image cooldownSlot; 
  }

  private List<AbilityCast> _abilityList = null;

  private List<AbilityCast> GetAbilityList() {
    if (_abilityList == null) {
      _abilityList = new List<AbilityCast>();
      InitializeAbility(AutoAttackAbility, AutoAttackAbilityHotbarImage);
      InitializeAbility(PrimaryAbility, PrimaryAbilityAbilityHotbarImage);
      InitializeAbility(SecondaryAbility);
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
    // Input Handling
    /*if (GetAbilityList().Any(x => x.AbilityCurrentlyLockingOtherAbilities)) {
      Debug.Log("Ability still ongoing, can't cast ne skill.")
      return;
    }*/
    if (Input.GetMouseButtonDown(0)) {
      AutoAttackAbility.TryCast(GetOriginPosition(), GetTargetPosition());
    }
    else if (Input.GetKeyDown(KeyCode.Alpha1)) {
      PrimaryAbility.TryCast(GetOriginPosition(), GetTargetPosition());
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2)) {
      SecondaryAbility.TryCast(GetOriginPosition(), GetTargetPosition());
    }
  }

  

  private Vector2 GetTargetPosition() {
    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return new Vector2(mousePosition.x, mousePosition.y);
  }
  private Vector2 GetOriginPosition() {
    return new Vector2(player.transform.position.x, player.transform.position.y);
  }


}