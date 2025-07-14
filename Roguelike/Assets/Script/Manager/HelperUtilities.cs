using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static Camera maincamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (maincamera == null)
            maincamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = maincamera.ScreenToWorldPoint(mouseScreenPosition);

        //Debug.Log(worldPosition);
        worldPosition.z = 0;

        return worldPosition;
    }


    public static float GetAngleFromVetor(Vector3 vector3)
    {
        float radians = Mathf.Atan2(vector3.y, vector3.x);

        float degree = radians * Mathf.Rad2Deg;

        return degree;
    }

    //将角度转为方向的枚举值
    public static AimDirection GetAimDirection(float angleDegree)
    {
        AimDirection aimDirection;
        if (angleDegree >= 22f && angleDegree <= 67f)
        {
            aimDirection = AimDirection.UpRight;
        }
        else if (angleDegree > 67f && angleDegree <= 112f)
        {
            aimDirection = AimDirection.Up;
        }
        else if (angleDegree > 112f && angleDegree <= 158f)
        {
            aimDirection = AimDirection.UpLeft;
        }
        else if ((angleDegree > 158f && angleDegree <= 180f) || (angleDegree > -180f && angleDegree <= 135f))
        {
            aimDirection = AimDirection.Left;
        }
        else if (angleDegree > -135f && angleDegree <= -45f)
        {
            aimDirection = AimDirection.Down;
        }
        else if ((angleDegree > -45f && angleDegree <= 0f) || (angleDegree > 0f && angleDegree < 22f))
        {
            aimDirection = AimDirection.Right;
        }
        else
        {
            aimDirection = AimDirection.Right;
        }
        return aimDirection;
    }
}
