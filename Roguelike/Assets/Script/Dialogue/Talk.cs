using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
    [SerializeField]
    private bool isEntered = false;
    
    public bool hasName = false;
    [TextArea(1, 3)]
    public string[] lines;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEntered = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEntered = false;
        }
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.Space)&&Dialogue.Instance.dialogueBox.activeInHierarchy == false)
            Dialogue.Instance.ShowDialogu(lines, hasName);
    }


}
