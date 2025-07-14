using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : IState
{
    private PlayerFSM manager;
    public Player_move(PlayerFSM manager)
    {
        this.manager = manager;

    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
            manager.animator.Play(TransitionAnimator.PlayMoveDown);
        if (Input.GetKeyDown(KeyCode.S))
            manager.animator.Play(TransitionAnimator.PlayMoveUp);
        if (Input.GetKeyDown(KeyCode.A))
            manager.animator.Play(TransitionAnimator.PlayMoveDownRight);
        if (Input.GetKeyDown(KeyCode.D))
            manager.animator.Play(TransitionAnimator.PlayMoveDownLift);
    }

}
