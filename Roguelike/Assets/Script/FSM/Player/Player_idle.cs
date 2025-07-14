using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_idle : IState
{
    private PlayerFSM manager;

    public Player_idle(PlayerFSM manager)
    {
        this.manager = manager;
    }
   
    public void OnEnter()
    {
        //manager.animator.Play("The_default_IdleUp");
    }

    public void OnExit()
    {
        manager.animator.Play(TransitionAnimator.PlayIdleUp);
  
    }

    public void OnUpdate()
    {
        
     
    }


}
