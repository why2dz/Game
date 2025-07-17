using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    

}
public class PlayerFSM : Character
{
    public Animator animator;

    protected override void InitializeStates()
    {
        animator = GetComponent<Animator>();
        states.Add(E_CharacterState.Idle, new Player_idle(this));
        states.Add(E_CharacterState.Move, new Player_move(this));

        TransitionState(E_CharacterState.Move);
    }

}
