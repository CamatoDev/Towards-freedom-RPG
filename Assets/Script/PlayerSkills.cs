using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    public GameObject UIpannel;
    public Text pointText;
    public int availablePoints;
    public string openKey;

    // 
    private bool isOpen;
    private PlayerInventory playerInv;

    // Start is called before the first frame update
    void Start()
    {
        playerInv = GetComponent<PlayerInventory>();
        isOpen = false;
        UIpannel.SetActive(isOpen);
        pointText.text = "Skill Points: " + availablePoints;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(openKey))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            pointText.text = "Skill Points: " + availablePoints;
            UIpannel.SetActive(true);
        }
        else
        {
            UIpannel.SetActive(false);
            //Time.timeScale = 1f; // Resume the game
        }
    }

    public void addHealthMax(float amountHp)
    {
        if(availablePoints >= 1)
        {
            playerInv.maxHealth += amountHp;
            playerInv.currentHealth += amountHp;
            availablePoints -= 1;
        }
    }

    public void addManaMax(float amountMana)
    {
        if(availablePoints >= 1)
        {
            playerInv.maxMana += amountMana;
            playerInv.currentMana += amountMana;
            availablePoints -= 1;
        }
    }
}
