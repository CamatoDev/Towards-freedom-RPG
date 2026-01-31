using UnityEngine;
using UnityEngine.UI;

public class PrimaryQuest : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Inventory inventoryPlayer;
    public GameObject questPanel;
    public Button claimRewardBtn;

    // Conteur du nombre d'ennemi tuer lors de la quête
    public Text enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        questPanel.SetActive(false);
        enemyCount.gameObject.SetActive(false);
        enemyCount.text = "Monstre éliminé : " + 0 + " / 10";
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount.text = "Monstre éliminé : " + playerInventory.enemyKillNumber + " / 10";

        if(playerInventory.enemyKillNumber >= 10)
        {
            claimRewardBtn.interactable = true;
        }
        else
        {
            claimRewardBtn.interactable = false;
        }
    }

    // Fontion pour le bouton de validation de la quête
    public void OnValidate()
    {
        enemyCount.gameObject.SetActive(true);
    }

    // Fonction pour le bouton pour récuperer la récompense
    public void ClaimRewards()
    {
        // Donne de l'argent quand le jouer à tuer 10 ennemis
        playerInventory.playerMoney += 5000;
        claimRewardBtn.interactable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            // Activation du shop 
            questPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            // Désactivation du panel de magasin 
            questPanel.SetActive(false);
        }
    }
}
