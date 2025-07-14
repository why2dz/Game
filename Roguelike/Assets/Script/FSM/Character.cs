using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Character : MonoBehaviour
{
   // public CharacterParameters parameters;
    //public Transform target; // 敌人或玩家的目标

    protected IState currentState;
    protected Dictionary<E_CharacterState, IState> states = new Dictionary<E_CharacterState, IState>();

    protected virtual void Start()
    {
        InitializeStates();
    }

    protected virtual void Update()
    {
        currentState?.OnUpdate();
    }

    protected abstract void InitializeStates();

    public void TransitionState(E_CharacterState state)
    {
        currentState?.OnExit();
        currentState = states[state];
        currentState.OnEnter();
    }


}
