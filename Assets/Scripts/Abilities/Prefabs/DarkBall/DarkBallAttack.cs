using Assets.Scripts.Abilities.Prefabs.DarkBall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBallAttack : Ability {
  public Transform Effect;

  public bool blocksOtherAbilities = false;

  DarkBallSegment _middleSkill;
  DarkBallSegment _leftSkill;
  DarkBallSegment _rightSkill;

  public override void Cast() {
    _middleSkill = new DarkBallSegment()
    {
      Effect = Effect,
      origin = origin,
      target = target,
      effectSpeed = effectSpeed,
    };
    _leftSkill = new DarkBallSegment()
    {
      targetOffset = 15f,
      Effect = Effect,
      origin = origin,
      target = target,
      effectSpeed = effectSpeed,
    };
    _rightSkill = new DarkBallSegment()
    {
      targetOffset = -15f,
      Effect = Effect,
      origin = origin,
      target = target,
      effectSpeed = effectSpeed,
    };
    IsCasting = true;
    _middleSkill.Cast();
    _leftSkill.Cast();
    _rightSkill.Cast();
  }

  // Update is called once per frame
  public override void UpdateCast() {
    if (IsCasting) {
      bool isCasting = false;
      if (_middleSkill.IsCasting) {
        isCasting = true;
        _middleSkill.UpdateCast();
      }
      if (_leftSkill.IsCasting) {
        isCasting = true;
        _leftSkill.UpdateCast();
      }
      if (_rightSkill.IsCasting) {
        isCasting = true;
        _rightSkill.UpdateCast();
      }
      if (!isCasting) {
        IsCasting = false;
      }
    }
  }
}
