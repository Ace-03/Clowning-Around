using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class _Menu : MonoBehaviour
{
    public static _Menu instance;
    public GameObject[] screens;
    public TextMeshProUGUI redFlags;
    public TextMeshProUGUI greenFlags;

    void Awake()
    {
        instance = this;
    }
    public void OnButtonClick(int index)
    {
        if (index >= 3)
            SceneManager.LoadScene(Mathf.Abs(index-4));
        else
            SetScreen(index);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetScreen(0);
        }
    }

    public void UpdateEndGameStatistics()
    {
        redFlags.text = GameManager.instance.redFlags.ToString();
        greenFlags.text = GameManager.instance.greenFlags.ToString();
    }

    private void SetScreen(int index)
    {
        foreach(var screen in screens)
        {
            screen.gameObject.SetActive(false);
        }

        screens[index].gameObject.SetActive(true);
    }
}
