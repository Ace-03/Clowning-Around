using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("ClownStuffs")]
    public GameObject[] clowns;
    public GameObject[] bachelors = new GameObject[5];
    private int[] bachelorids = new int[5];

    [Header("Player Stats")]
    public int redFlags = 0;
    public int greenFlags = 0;

    [Header("Interactable")]
    public TextMeshProUGUI DialogueBox;
    public TextMeshProUGUI response1;
    public TextMeshProUGUI response2;
    public TextMeshProUGUI Name;
    public Button responseOne;
    public Button responseTwo;
    public Button reject;
    public Button like;
    public GameObject TransitionScreen;

    public int cur = 0;
    public ClownScript clown;

    public static GameManager instance;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // random generator to pick a clown
        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(1, clowns.Length);
            if (checkNumInArray(index))
            {
                bachelors[i] = Instantiate(clowns[index], GameObject.Find("In-Game").transform);
                bachelorids[i] = index;
            }
            else
            {
                i--;
            }
        }

        //Set all the bachelors to false
        foreach(var bachelor in bachelors)
        {
            bachelor.SetActive(false);
        }

        bachelors[0].SetActive(true);
        clown = bachelors[0].transform.GetComponent<ClownScript>();
        clown.OnEnter();
    }
    private bool checkNumInArray(int index)
    {
        foreach(int i in bachelorids)
        {
            if (i == index)
                return false;
        }
        return true;
    }
    public void Next()
    {
        bachelors[cur].transform.GetComponent<ClownScript>().OnExit();
        bachelors[cur].SetActive(false); cur++;
        if(cur < 5)
        {
            bachelors[cur].SetActive(true);
            clown = bachelors[cur].transform.GetComponent<ClownScript>();
            clown.onChoice = true;
            clown.OnEnter();
        }
        else if(cur == 5)
        {
            EndGame();
        }
    }
    public void SetText(string nameText, string speech)
    {
        response1.text = " ";
        response2.text = " ";

        Name.text = nameText;
        DialogueBox.text = speech;
        clown.onChoice = false;
        responseOne.interactable = false;
        responseTwo.interactable = false;

    }
    public void SetText(string nameText, string speech, string R1, string R2)
    {
        response1.text = R1;
        response2.text = R2;

        Name.text = nameText;
        DialogueBox.text = speech;
        clown.onChoice = true;
        responseOne.interactable = true;
        responseTwo.interactable = true;

    }
    private void EndGame() 
    {
        _Menu.instance.OnButtonClick(2);
        _Menu.instance.UpdateEndGameStatistics();
    }

    public void Response(int id)
    {
        if (clown.correctResponse[clown.CurChoice] == id)
        {
            clown.OnGoodResponse();
        }
        else { clown.OnBadResponse(); }

    }
    public void Swipe()
    {
        reject.interactable = !reject.interactable;
        like.interactable = !like.interactable;
        clown.onChoice = !clown.onChoice;
    }

    public void Like()
    {
        redFlags += clown.red;
        greenFlags += clown.green;
        Swipe();
        Transition();
    }

    public void Reject()
    {
        Swipe();
        Transition();
    }

    public void Transition()
    {
        clown.onChoice = true;
        TransitionScreen.SetActive(true);
        Invoke("Next", 1f);
    }

    public void TransitionButton()
    {
        Invoke("StartNext", 1.5f);
    }
    public void StartNext()
    {
        TransitionScreen.SetActive(false);
        clown.onChoice = false;
    }
}
