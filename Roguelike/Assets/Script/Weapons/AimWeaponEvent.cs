using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
//武器瞄准发布者
public class AimWeaponEvent : MonoBehaviour
{
    //因为武器种类的不同，使用不同的事件。
    public event Action<AimWeaponEvent, AimWeaponEventArg> OnWeaponAim;

    public void CallAimWeaponEvent(AimDirection aimDirection, float aimAngle, float weaponAIMAngle,Vector3 weaponAimDirectionVector)
    {
        OnWeaponAim?.Invoke(this, new AimWeaponEventArg() { aimDirection = aimDirection, aimAngle = aimAngle, weaponAIMAngle = weaponAIMAngle , weaponAimDirectionVector  = weaponAimDirectionVector});
        
    }
}

public class AimWeaponEventArg:EventArgs
{
    public AimDirection aimDirection;
    public float aimAngle;
    public float weaponAIMAngle;
    public Vector3 weaponAimDirectionVector;
}