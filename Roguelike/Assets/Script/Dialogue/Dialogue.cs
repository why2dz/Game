using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Singleton<Dialogue>
{


    [SerializeField]
    public GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText, nameText;

    [SerializeField]
    [TextArea(1,3)]
    public string[] dialogueLines;
    [SerializeField]
    private int currentLine;

    private bool isSrolling = true;
    private float textSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        //≥ı ºªØ
        dialogueText.text = dialogueLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (isSrolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        CheackName();
                        //dialogueText.text = dialogueLines[currentLine];
                        StartCoroutine(ScrollingText());
                    }

                    else
                    {
                        dialogueBox.SetActive(false);
                        FindObjectOfType<Player>().canMove = true;
                    }
                }
              
                    
            }
        }
    }

    public void ShowDialogu(string[] _newLines,bool _hasName)
    {
        dialogueLines = _newLines;
        currentLine = 0;
        CheackName();
        StartCoroutine(ScrollingText());
        //dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        nameText.gameObject.SetActive(_hasName);


        FindObjectOfType<Player>().canMove = false;
    }

    public void CheackName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }

    private IEnumerator ScrollingText()
    {
        isSrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isSrolling = false;
    }
}
