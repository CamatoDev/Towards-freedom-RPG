using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public GameObject UIpannel;
    public GameObject onPausePanel;
    //public string openKey;

    // 
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        UIpannel.SetActive(isOpen);
        onPausePanel.SetActive(isOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            UIpannel.SetActive(true);
            onPausePanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            UIpannel.SetActive(false);
            onPausePanel.SetActive(false);
            Time.timeScale = 1f; // Resume the game
        }
    }
}
