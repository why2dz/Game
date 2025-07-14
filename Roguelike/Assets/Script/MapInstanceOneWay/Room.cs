using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject DoolR, DoolL, DoolD, DoolU;
    public bool doolR, doolL, doolD, doolU = false;
    
    public Text roomNum;

    public int stratP;
    public int doorcount;
    void Start()
    {
        
        DoolR.SetActive(doolR);
        DoolL.SetActive(doolL);
        DoolU.SetActive(doolU);
        DoolD.SetActive(doolD);
       
    }

    public void UpRoom(int Xoffic,int Yoffice)
    {
        stratP = (int)(System.Math.Abs(transform.position.x / Xoffic) + System.Math.Abs(transform.position.y / Yoffice));

        roomNum.text = stratP.ToString();

        if (doolR) doorcount++;
        if (doolL) doorcount++;
        if (doolU) doorcount++;
        if (doolD) doorcount++;
    }
}
