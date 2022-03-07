using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Idle")]
public class test : StateData
{
    public float a ;
    public AnimationCurve b;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) { }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) { }
    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) { }
}

