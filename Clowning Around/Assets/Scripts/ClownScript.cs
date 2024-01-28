using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClownScript : MonoBehaviour
{
    [Header("Dialogue")]
    public string Name;
    public DialogueStruct[] dialogues;
    public ResponseStruct[] responses;
    public FinalSayings goodbye;
    public int[] correctResponse;
    public int numChoices;
    public int index = 0;
    public int outdex = 0;
    public bool good = true;
    public int CurChoice = 0;

    [Header("Sprites")]
    public Sprite angry;
    public Sprite happy;
    public Sprite neutral;
    public Image img;

    [Header("Stats")]
    public int likeablility = 0;
    public int red;
    public int green;

    public bool onChoice = false;
    private int talked = 0;
    public bool whotalked = false;

    public void OnEnter() 
    {
        //GameManager.instance.SetText(Name, dialogues[outdex].dialogueOne[index]);
        Invoke("ClownTalk", 2f);
    }
    public void OnBadResponse()
    {
        CurChoice++;
        good = false;
        onChoice = false;
        PlayerTalk();
        //Set Sprite to bad
        img.sprite = angry;
    }
    public void OnGoodResponse()
    {
        CurChoice++;
        likeablility++;
        onChoice = false;
        good = true;
        PlayerTalk();

        //set sprite to happy
        img.sprite = happy;
    }
    public void OnExit() { }
    public void Next()
    {
        Debug.Log("Hi");
        outdex++;
        index = 0;
        talked = 0;
        if (outdex <= numChoices)
        {
            img.sprite = neutral;
            GameManager.instance.SetText(Name, dialogues[outdex].question, responses[outdex].choice1, responses[outdex].choice2);
            whotalked = false;
        }
        else
        {
            GameManager.instance.Swipe();
            if (likeablility <= 0)
            {
                GameManager.instance.Reject();
            }

        }

    }
    void Update()
    {
        if(!onChoice && Input.GetKeyUp(KeyCode.Space))
        {
            if (whotalked)
            {
                ClownTalk();
            }
            else
            {
                PlayerTalk(); 
            }
        }
    }

    public void ClownTalk()
    {

               Debug.Log(talked);
        if (good)
        {
            if (index < dialogues[outdex].dialogueOne.Length)
            {
                GameManager.instance.SetText(Name, dialogues[outdex].dialogueOne[index]);
                talked++;
                if (talked == 2)
                {
                    index++;
                    talked = 0;
                }

                whotalked = false;

            }
            else
            {
                Debug.Log("ClownTalk good");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueOne.Length);
                Debug.Log(responses[outdex].responsesOne.Length);
                Next();
            }

        }
        else
        {
            if (index < dialogues[outdex].dialogueTwo.Length)
            {
                GameManager.instance.SetText(Name, dialogues[outdex].dialogueTwo[index]);
                talked++;
                if (talked == 2)
                {
                    index++;
                    talked = 0;
                }

                whotalked = false;
            }
            else
            {

                Debug.Log("ClownTalk bad");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueTwo.Length);
                Debug.Log(responses[outdex].responsesTwo.Length);
                Next();
            }

        }

    }

    public void PlayerTalk()
    {
        Debug.Log(talked);
        if(good)
        {
            if (index < responses[outdex].responsesOne.Length)
            {
                GameManager.instance.SetText("Player", responses[outdex].responsesOne[index]);
                talked++;
                if (talked == 2)
                {
                    index++;
                    talked = 0;
                }

                whotalked = true;
            }
            else
            {

                Debug.Log("PlayerTalk good");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueOne.Length);
                Debug.Log(responses[outdex].responsesOne.Length);
                Next();
            }
        }
        else
        {

            if (index < responses[outdex].responsesTwo.Length)
            {
                GameManager.instance.SetText("Player", responses[outdex].responsesTwo[index]);
                talked++;
                if (talked == 2)
                {
                    index++;
                    talked = 0;
                }

                whotalked = true;
            }
            else
            {
                Debug.Log("PlayerTalk bad");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueTwo.Length);
                Debug.Log(responses[outdex].responsesTwo.Length);
                Next();
            }
        }

    }
}

[System.Serializable]
public struct DialogueStruct
{
    public string question;
    public string[] dialogueOne;
    public string[] dialogueTwo;
}
[System.Serializable]
public struct ResponseStruct
{
    public string choice1;
    public string choice2;
    public string[] responsesOne;
    public string[] responsesTwo;

}
[System.Serializable]
public struct FinalSayings
{
    public string goodEnding;
    public string badEnding;
}
