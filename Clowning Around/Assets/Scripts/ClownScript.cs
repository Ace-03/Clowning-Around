using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownScript : MonoBehaviour
{
    [Header("Dialogue")]
    public string Name;
    public DialogueStruct[] dialogues;
    public ResponseStruct[] responses;
    public int[] correctResponse;
    public int numChoices;
    public int index = 0;
    public int outdex = 0;
    public bool good = true;

    [Header("Sprites")]
    public Sprite angry;
    public Sprite happy;
    public Sprite neutral;

    [Header("Stats")]
    public int likeablility = 0;
    public int red;
    public int green;

    public void OnEnter() 
    {
        GameManager.instance.SetText(Name, dialogues[outdex].dialogueOne[index]);
        Invoke("PlayerTalk", 2f);
    }
    public void OnBadResponse()
    {
        good = false;
        PlayerTalk();
        //Set Sprite to bad
    }
    public void OnGoodResponse()
    {
        good = true;
        PlayerTalk();

        //set sprite to happy
    }
    public void OnExit() { }
    public void Next()
    {
        Debug.Log("Hi");
        outdex++;
        index = 0;
        if (outdex <= numChoices)
        {
            GameManager.instance.SetText(Name, dialogues[outdex].question, responses[outdex].choice1, responses[outdex].choice2);
        }
        else
            GameManager.instance.Swipe();
    }

    public void ClownTalk()
    {
        if (good)
        {
            if (index >= dialogues[outdex].dialogueOne.Length || index >= responses[outdex].responsesOne.Length)
            {
                Debug.Log("ClownTalk good");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueOne.Length);
                Debug.Log(responses[outdex].responsesOne.Length);
                Next();
            }
            else
            {
                GameManager.instance.SetText(Name, dialogues[outdex].dialogueOne[index]);
                Invoke("PlayerTalk", 2f);
            }

        }
        else
        {
            if (index >= dialogues[outdex].dialogueTwo.Length || index >= responses[outdex].responsesTwo.Length)
            {
                Debug.Log("ClownTalk bad");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueTwo.Length);
                Debug.Log(responses[outdex].responsesTwo.Length);
                Next();
            }
            else
            {
                GameManager.instance.SetText(Name, dialogues[outdex].dialogueTwo[index]);
                Invoke("PlayerTalk", 2f);
            }

        }

    }

    public void PlayerTalk()
    {
        if(good)
        {
            if (index >= dialogues[outdex].dialogueOne.Length || index >= responses[outdex].responsesOne.Length)
            {
                Debug.Log("PlayerTalk good");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueOne.Length);
                Debug.Log(responses[outdex].responsesOne.Length);
                Next();
            }
            else
            {
                GameManager.instance.SetText("Player", responses[outdex].responsesOne[index]);
                index++;
                Invoke("ClownTalk", 2f);
            }
        }
        else
        {

            if (index >= dialogues[outdex].dialogueTwo.Length || index >= responses[outdex].responsesTwo.Length)
            {
                Debug.Log("PlayerTalk bad");
                Debug.Log(index);
                Debug.Log(dialogues[outdex].dialogueTwo.Length);
                Debug.Log(responses[outdex].responsesTwo.Length);
                Next();
            }
            else
            {
                GameManager.instance.SetText("Player", responses[outdex].responsesTwo[index]);
                index++;
                Invoke("ClownTalk", 2f);
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
