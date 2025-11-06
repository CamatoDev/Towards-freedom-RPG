using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    // ID de l'arme 
    private int itemID;

    // membres du personnage 
    public GameObject bodyPart;

    //liste des armes 
    public List<GameObject> itemList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
            itemID = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
        }
        else
        {
            itemID = 0;

            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetActive(false);
            }
        }

        // Si plusieurs Item sont détecté au même endroit on désactive tout sauf celle qu'on a vraiment équipé
        if(bodyPart.transform.childCount > 1)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetActive(false);
            }
        }

        //pour l'épée wideblade sword
        if(itemID == 1 && transform.childCount > 0)
        {
            for(int i=0; i < itemList.Count; i++)
            {
                if(i == 0)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //pour le casque helmet
        if (itemID == 2 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //pour les bracelets en fer iron bracer 
        if (itemID == 3 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0 || i == 1)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //pour le katana
        if (itemID == 4 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 1)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //pour les bracelets simple
        if (itemID == 5 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 2 || i == 3)
                {
                    itemList[i].SetActive(true);
                }
            }
        }
    }
}
