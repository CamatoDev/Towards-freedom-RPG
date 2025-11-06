using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Inventory inventoryPlayer;
    public GameObject shopPanel;
    public ItemDataBaseList itemDataBaseList;

    // Les id des items disponibles dans le shop
    [Header("Shop Items IDs")]
    public int item1ID;
    public int item2ID;
    public int item3ID;
    public int item4ID;

    // Les textes UI pour afficher les noms et prix des items
    [Header("Shop UI Texts")]
    public Text textItem1;
    public Text textItem2;
    public Text textItem3;
    public Text textItem4;

    // Les images UI pour afficher les icônes des items
    [Header("Shop UI Images")]
    public Image iconItem1;
    public Image iconItem2;
    public Image iconItem3;
    public Image iconItem4;

    // Les paramètres des objets su shop
    Item item1;
    Item item2;
    Item item3;
    Item item4;

    // Variable gestion de l'inventaire du joueur lors de l'achat
    private int amountSlots;
    private int slotsChecked;
    private bool transactionDone;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fonction de preparation des items du shop
    public void PrepareShopItems()
    {
        // Création des objets Item à partir des IDs et mise à jour de l'UI
        
        // Item 1
        item1 = itemDataBaseList.getItemByID(item1ID);
        textItem1.text = item1.itemName + "\n Prix : " + item1.itemValue + " fcfa";
        iconItem1.sprite = item1.itemIcon;
        // Item 2
        item2 = itemDataBaseList.getItemByID(item2ID);
        textItem2.text = item2.itemName + "\n Prix : " + item2.itemValue + " fcfa";
        iconItem2.sprite = item2.itemIcon;
        // Item 3
        item3 = itemDataBaseList.getItemByID(item3ID);
        textItem3.text = item3.itemName + "\n Prix : " + item3.itemValue + " fcfa";
        iconItem3.sprite = item3.itemIcon;
        // Item 4
        item4 = itemDataBaseList.getItemByID(item4ID);
        textItem4.text = item4.itemName + "\n Prix : " + item4.itemValue + " fcfa";
        iconItem4.sprite = item4.itemIcon;

        // Attribution dinamyque des évenements onclick au iconne (button)
        iconItem1.transform.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(item1); });
        iconItem2.transform.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(item2); });
        iconItem3.transform.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(item3); });
        iconItem4.transform.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(item4); });

        // Activation du shop 
        shopPanel.SetActive(true);
    }

    void BuyItem(Item finalItem)
    {
        amountSlots = inventoryPlayer.transform.GetChild(1).childCount;
        transactionDone = false;
        slotsChecked = 0;

        foreach (Transform child in inventoryPlayer.transform.GetChild(1))
        {
            if (child.childCount == 0)
            {
                if(playerInventory.playerMoney >= finalItem.itemValue)
                {
                    inventoryPlayer.addItemToInventory(finalItem.itemID);
                    // Enlever l'argent du joueur
                    playerInventory.playerMoney -= finalItem.itemValue;
                    transactionDone = true;
                    print("Le joueur a acheté : " + finalItem.itemName);
                    break;
                }
                else
                {
                    print("Le joueur n'a pas assez d'argent pour acheter l'item : " + finalItem.itemName);
                }
                
            }
            slotsChecked += 1;
        }

        if(slotsChecked == amountSlots && !transactionDone)
        {
            print("L'inventaire est plein, impossible d'acheter l'item : " + finalItem.itemName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            PrepareShopItems();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            // Désactivation des listeners pour éviter les fuites de mémoire
            iconItem1.GetComponent<Button>().onClick.RemoveAllListeners();
            iconItem2.GetComponent<Button>().onClick.RemoveAllListeners();
            iconItem3.GetComponent<Button>().onClick.RemoveAllListeners();
            iconItem4.GetComponent<Button>().onClick.RemoveAllListeners();

            // Désactivation du panel de magasin 
            shopPanel.SetActive(false);
        }
    }
}
