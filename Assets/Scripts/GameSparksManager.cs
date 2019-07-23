using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LootBox { SkinRelic, CharacterBlueprint, TimeCapsule };

[System.Serializable]
public struct BasicSkinData {
    public int index;
    public Sprite Skin;
    public string Name;
}

[System.Serializable]
public struct BasicCharData {
    public int index;
    public Sprite look;
    public string Name;
}

public class GameSparksManager : MonoBehaviour {
    public PlayerData playerData;

    public BasicSkinData[] CommonSkins;
    public BasicSkinData[] UnCommonSkins;
    public BasicSkinData[] RareSkins;
    public BasicSkinData[] LegendarySkins;

    public BasicCharData[] CharData;

    public MainMenuUi MenuUI;
    public GameObject RegisteringPanel;
    public GameObject LogInPanel;
    public GameObject NotificationPanel;

    private static GameSparksManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    //Save all the player data to the gamesparks server
    public bool SavePlayerData() {
        bool returnBool = false;
        string skinUnlocks = "";    //Skin unlocks are stored as a string of 0 and 1 (optimalization: change to binary representation)
        for (int i = 0; i < playerData.SkinUnlocks.GetLength(0); i++) {
            if (playerData.SkinUnlocks[i]) {
                skinUnlocks = skinUnlocks + " 1";
            } else {
                skinUnlocks = skinUnlocks + " 0";
            }
        }

        string charUnlocks = "";
        for (int i = 0; i < playerData.CharUnlocks.GetLength(0); i++) {
            if (playerData.CharUnlocks[i]) {
                charUnlocks = charUnlocks + " 1";
            } else {
                charUnlocks = charUnlocks + " 0";
            }
        }

        string trailUnlocks = "";
        for (int i = 0; i < playerData.TrailUnlocks.GetLength(0); i++) {
            if (playerData.TrailUnlocks[i]) {
                trailUnlocks = trailUnlocks + " 1";
            } else {
                trailUnlocks = trailUnlocks + " 0";
            }
        }

        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("SavePlayerData")
            .SetEventAttribute("NAME", playerData.Name)
            .SetEventAttribute("XP", playerData.XP)
            .SetEventAttribute("SKINUNLOCKS", skinUnlocks)
            .SetEventAttribute("CHARUNLOCKS", charUnlocks)
            .SetEventAttribute("TRAILUNLOCKS", trailUnlocks)
            .SetEventAttribute("WINS", playerData.Wins)
            .SetEventAttribute("LOSSES", playerData.Losses)
            .Send((response) => {
                if (!response.HasErrors) {
                    returnBool = true;
                } else {
                    returnBool = false;
                }
            });
        //TODO: add error handling
        return returnBool;
    }

    //Load player data from the gamesparks servers
    public void LoadPlayerData() {
        GetTokens();
        GetGoods();

        playerData.SetUpData(this);
    }

    //Level up the player a normal level(every non 5 divisible level)
    public void SmallLevelUp() {
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SimpleLevelUp").Send((response) => { });
        GetTokens();
    }

    //Level up the player a big level(every 5 levels, adds extra levelup rewards)
    public void BigLevelUp() {
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("BigLevelUp").Send((response) => { });
        GetTokens();
    }

    //Ends a match and hands out a win/loss and rewards accordingly
    public void EndMatch(bool Won) {
        if (Won) {
            new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("GiveCoin")
            .SetEventAttribute("amount", 25)
            .Send((response) => { /*TODO: add error handling*/ });
        } else {
            new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("GiveCoin")
            .SetEventAttribute("amount", 10)
            .Send((response) => { /*TODO: add error handling*/ });
        }
        SavePlayerData();
    }

    //Get the number of loot boxes a player has from the GameSparks server
    public void GetGoods() {
        new GameSparks.Api.Requests.AccountDetailsRequest().Send((response) => {
            if (!response.HasErrors) {
                if (response.VirtualGoods.GetInt("SkinRelic") != null)
                    playerData.SkinRelics = (int)response.VirtualGoods.GetInt("SkinRelic");
                else
                    playerData.SkinRelics = 0;

                if (response.VirtualGoods.GetInt("CharacterBlueprint") != null)
                    playerData.CharacterBlueprint = (int)response.VirtualGoods.GetInt("CharacterBlueprint");
                else
                    playerData.CharacterBlueprint = 0;

                if (response.VirtualGoods.GetInt("TimeCapsule") != null)
                    playerData.TimeCapsule = (int)response.VirtualGoods.GetInt("TimeCapsule");
                else
                    playerData.TimeCapsule = 0;
            } else {
                //TODO: add error handling
            }
        });
    }

    //Buy a new lootbox of the given "loot" type
    public void BuyLootBox(LootBox loot) {
        string shortCode = "";
        switch (loot) {
            case LootBox.SkinRelic:
                shortCode = "SkinRelic";
                break;
            case LootBox.CharacterBlueprint:
                shortCode = "CharacterBlueprint";
                break;
            case LootBox.TimeCapsule:
                shortCode = "TimeCapsule";
                break;
            default:
                break;
        }
        new GameSparks.Api.Requests.BuyVirtualGoodsRequest()
            .SetCurrencyType(1)
            .SetQuantity(1)
            .SetShortCode(shortCode)
            .Send((response) => {
                if (!response.HasErrors) {
                    GetGoods();
                    GetTokens();
                } else {
                    //TODO: add error handling
                }
            });
    }

    //Loads in the amount of tokens the player has (excluded from loadPlayerData for performance)
    public void GetTokens() {
        new GameSparks.Api.Requests.AccountDetailsRequest().Send((response) => {
            if (!response.HasErrors) {
                int Tokens = int.Parse(response.Currency1.ToString());
                playerData.Tokens = Tokens;
            } else {
                //TODO: add error handling
            }
        });
    }

    //Unlocks a given type of loot box of a given index
    public void Unlock(LootBox loot, int index) {
        string shortCode = "";
        switch (loot) {
            case LootBox.SkinRelic:
                shortCode = "SkinRelic";
                playerData.SkinUnlocks[index] = true;
                break;
            case LootBox.CharacterBlueprint:
                shortCode = "CharacterBlueprint";
                playerData.SkinUnlocks[index] = true;
                break;
            case LootBox.TimeCapsule:
                shortCode = "TimeCapsule";
                playerData.CharUnlocks[index] = true;
                break;
        }
        SavePlayerData();
        new GameSparks.Api.Requests.ConsumeVirtualGoodRequest()
            .SetQuantity(1)
            .SetShortCode(shortCode)
            .Send((response) => {
                if (!response.HasErrors) {
                    LoadPlayerData();
                } else {
                    //TODO: add error handling
                }
            });
    }

    //Registers a new players data in the gamesparks server and gives them the initial player data
    //TODO: automize this so that its not so hard coded
    public void RegisterPlayerData(string name) {
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("SavePlayerData")
            .SetEventAttribute("NAME", name)
            .SetEventAttribute("LEVEL", 1)
            .SetEventAttribute("XP", 0)
            .SetEventAttribute("SKINUNLOCKS", " 1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0")
            .SetEventAttribute("CHARUNLOCKS", " 1 0 0 0 1 0")
            .SetEventAttribute("TRAILUNLOCKS", " 1 0 0 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0")
            .SetEventAttribute("WINS", 0)
            .SetEventAttribute("LOSSES", 0)
            .Send((response) => {
                if (!response.HasErrors) {

                } else {

                }
            });

        LoadPlayerData();
    }

    //Registers a new player
    public void RegisterPlayer() {
        new GameSparks.Api.Requests.RegistrationRequest()
            .SetDisplayName(GameObject.Find("Display Name Text").GetComponent<Text>().text)
            .SetPassword(GameObject.Find("Password Input Field Register").GetComponent<InputField>().text)
            .SetUserName(GameObject.Find("Username Text").GetComponent<Text>().text)
            .Send((response) => {
                if (!response.HasErrors) {
                    RegisterPlayerData(GameObject.Find("Display Name Text").GetComponent<Text>().text);

                    MenuUI.ToggleNotificationPanel("Registration complete");
                    SceneManager.LoadScene("Tutorial");
                } else {
                    MenuUI.ToggleNotificationPanel("Registration failed");
                }
            }
        );
        RegisterPlayerData(GameObject.Find("Display Name Text").GetComponent<Text>().text);
    }

    //Logins a player
    public void LoginPlayer() {
        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(GameObject.Find("Username Text").GetComponent<Text>().text)
            .SetPassword(GameObject.Find("Password Input Field Login").GetComponent<InputField>().text)
            .Send((response) => {
                if (!response.HasErrors) {
                    LoadPlayerData();
                    SceneManager.LoadScene("Test Scene");
                } else {
                    MenuUI.ToggleNotificationPanel("Login failed");
                }
            }
        );
    }
}


