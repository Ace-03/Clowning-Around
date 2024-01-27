using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownScript : MonoBehaviour
{
    [Header("Dialogue")]
    public string Name;
    public string[] dialogue;
    public string[] responses;
    public int[] correctResponse;
    public int index = 0;

    [Header("Sprites")]
    public Sprite angry;
    public Sprite happy;
    public Sprite neutral;

    [Header("Stats")]
    public int red;
    public int green;

    public void OnEnter() 
    {
        GameManager.instance.SetText(Name, dialogue[index+2], responses[2 * index], responses[2 * index + 1]);
    }
    public void OnBadResponse()
    {
        GameManager.instance.SetText(Name, dialogue[1]);
        Invoke("Next", 2f);
        //Set Sprite to bad
    }
    public void OnGoodResponse()
    {
        GameManager.instance.SetText(Name, dialogue[0]);
        Invoke("Next", 2f);
        //set sprite to happy
    }
    public void OnExit() { }
    public void Next()
    {
        index++;
        if (index < 5)
        {
            GameManager.instance.SetText(Name, dialogue[index + 2], responses[2 * index], responses[2 * index + 1]);
        }
        else
            GameManager.instance.Swipe();
    }
}
