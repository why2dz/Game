using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_TypeofWho
{
    Onewayconversations,
    Twowayconversations,
    Tip

}

[CreateAssetMenu ]
public class DialoguneSO : ScriptableObject
{
    public E_TypeofWho typeofWho;
    public string Lines;
}
