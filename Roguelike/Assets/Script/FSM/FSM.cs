using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 所有需要的信息
/// </summary>
//[Serializable]
//public class Parameter
//{

//    public float moveSpeed;

//    public float chaseSpeed;

//    public float idleTime;

//    public Transform[] patrolPoints;

//    public Transform[] chasePoints;

//    public Animator animator;
//}

public abstract class FSM : MonoBehaviour
{

    //public Parameter parameter;

    private IState currentState;

    private Dictionary<E_CharacterState, IState> states = new Dictionary<E_CharacterState, IState>();

    

    // Start is called before the first frame update
    void Start()
    {
        ////添加所有状态
        //states.Add(E_CharacterState.Idle, new Player_idle(this));
        //states.Add(E_CharacterState.Idle, new Player_move(this));

        TransitionState(E_CharacterState.Idle);

       // Parameter.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(E_CharacterState state)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[state];
        currentState.OnEnter();
    }

    public void Flip(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
