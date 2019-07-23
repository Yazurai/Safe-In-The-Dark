using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    GameSparksManager GSMan;

    public Text Tokens;
    public GameObject Notification;
    public GameObject Confirmation;

    float NotificationTimer;
    bool NotificationIsShown;

    int Good;

    void Awake() {
        GSMan = GameObject.Find("GameSparksManager").GetComponent<GameSparksManager>();
        Notification.SetActive(false);
        Confirmation.SetActive(false);
    }

    public void LoadTokens() {
        string newTokensText = GSMan.playerData.Tokens.ToString();
        newTokensText = newTokensText + " Tokens";
        Tokens.text = newTokensText;
    }

    public void Cancel() {
        Confirmation.SetActive(false);
    }

    public void SetSkinRelic() {
        Good = 0;
        Confirmation.SetActive(true);
    }

    public void SetCharacterBlueprint() {
        Good = 1;
        Confirmation.SetActive(true);
    }

    public void SetTimeCapsule() {
        Good = 2;
        Confirmation.SetActive(true);
    }

    public void Buy() {
        switch (Good) {
            case 0:
                BuySkinRelic();
                break;
            case 1:
                BuyCharacterBlueprint();
                break;
            case 2:
                BuyTimeCapsule();
                break;
            default:
                break;
        }
    }

    public void BuySkinRelic() {
        if (GSMan.playerData.Tokens < 50) {
            Confirmation.SetActive(false);
            NotificationIsShown = true;
            Notification.SetActive(true);
            NotificationTimer = 3f;
            Notification.transform.GetChild(0).GetComponent<Text>().text = "Not enough tokens to purchase";
        } else {
            GSMan.BuyLootBox(LootBox.SkinRelic);
            Confirmation.SetActive(false);
            BackToMainMenu();
            LoadTokens();
        }
    }

    public void BuyCharacterBlueprint() {
        if (GSMan.playerData.Tokens < 250) {
            NotificationIsShown = true;
            Notification.SetActive(true);
            NotificationTimer = 2.5f;
            Notification.transform.GetChild(0).GetComponent<Text>().text = "Not enough tokens to purchase";
        } else {
            GSMan.BuyLootBox(LootBox.CharacterBlueprint);
            LoadTokens();
            BackToMainMenu();
            Confirmation.SetActive(false);
        }
    }

    public void BuyTimeCapsule() {
        if (GSMan.playerData.Tokens < 250) {
            NotificationIsShown = true;
            Notification.SetActive(true);
            NotificationTimer = 2.5f;
            Notification.transform.GetChild(0).GetComponent<Text>().text = "Not enough tokens to purchase";
        } else {
            GSMan.BuyLootBox(LootBox.CharacterBlueprint);
            LoadTokens();
            BackToMainMenu();
        }
    }

    public void BackToMainMenu() {
        gameObject.SetActive(false);
    }

    void Update() {
        if (NotificationIsShown == true) {
            NotificationTimer = NotificationTimer - Time.deltaTime;
            if (NotificationTimer < 0) {
                Notification.SetActive(false);
                NotificationIsShown = false;
            }
        }
    }
}
