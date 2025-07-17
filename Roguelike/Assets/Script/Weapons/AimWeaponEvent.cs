using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
//������׼������
public class AimWeaponEvent : MonoBehaviour
{
    //��Ϊ��������Ĳ�ͬ��ʹ�ò�ͬ���¼���
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