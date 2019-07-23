using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SkinData {
    public string Name;
    public bool Unlocked;
    public string Rarity;
    public string Description;
    public Sprite skinSprite;
    public string DateAdded;
    public int index;
}

[System.Serializable]
public struct Class {
    public string Name;
    public bool Unlocked;
    public int index;
    public Sprite Look;
    public SkinData[] Skins;
}

public class InventoryManager : MonoBehaviour {
    public Class[] Hunters;
    public Class[] Players;

    public GameObject SideChoosePanel;
    public GameObject ClassChoosePanel;
    public GameObject SkinChoosePanel;
    public GameObject SkinPanel;
    public GameObject SkinRelicOpenPanel;
    public GameObject CharBlueprintOpenPanel;

    public GameObject SkinRelic;
    public GameObject Skin;
    public GameObject SkinRelicOpenButton;
    public GameObject CharBlueprint;
    public GameObject Character;
    public GameObject CharBlueprintOpenButton;
    public GameObject SkinRelicButton;
    public GameObject CharBlueprintButton;
    public GameObject[] ClassButtons;
    public GameObject[] SkinButtons;
    public Text skinName;
    public Image skinRarityBackground;
    public Text skinRarity;
    public Text skinDescription;
    public Text skinDate;
    public Image skinSkin;
    public Image SKinShine;
    public Image CharShine;

    bool IsOpeningChar;
    bool IsOpeningSkin;
    bool IsHunter;
    int ClassIndex;
    int SkinIndex;

    public PlayerData playerData;

    int newRarity;
    int NewSkinIndex;
    int NewSkinIndexUniversal;

    int NewCharIndexUniversal;

    public GameSparksManager GSMan;

    //This method unlocks a random skin via a Skin Relic
    public void OpenSkinRelic() {
        //We generate a random, which we will check to determine the rarity of the item || Common 60% || Uncommon 20% || Rare 15% || Legendary 5%
        int random = Random.Range(0, 100);
        if (random >= 0 && random < 60) {
            int index = Random.Range(0, GSMan.CommonSkins.GetLength(0));
            NewSkinIndex = index;
            newRarity = 0;
            NewSkinIndexUniversal = GSMan.CommonSkins[NewSkinIndex].index;
        }
        if (random >= 60 && random < 80) {
            int index = Random.Range(0, GSMan.UnCommonSkins.GetLength(0));
            NewSkinIndex = index;
            newRarity = 1;
            NewSkinIndexUniversal = GSMan.UnCommonSkins[NewSkinIndex].index;
        }
        if (random >= 80 && random < 95) {
            int index = Random.Range(0, GSMan.RareSkins.GetLength(0));
            NewSkinIndex = index;
            newRarity = 2;
            NewSkinIndexUniversal = GSMan.RareSkins[NewSkinIndex].index;
        }
        if (random >= 95 && random <= 100) {
            int index = Random.Range(0, GSMan.LegendarySkins.GetLength(0));
            NewSkinIndex = index;
            newRarity = 3;
            NewSkinIndexUniversal = GSMan.LegendarySkins[NewSkinIndex].index;
        }
        IsOpeningSkin = true;
        GSMan.Unlock(LootBox.SkinRelic, NewSkinIndexUniversal);
        UpdateInventory();
    }

    public void OpenCharBlueprint() {
        bool CanUnlock = false;
        foreach (var item in GSMan.playerData.CharUnlocks) {
            if (item == false)
                CanUnlock = true;
        }
        if (CanUnlock == true) {
            int index = 0;
            do {
                index = Random.Range(0, GSMan.playerData.CharUnlocks.GetLength(0));
            } while (GSMan.playerData.CharUnlocks[index] == true);
            GSMan.Unlock(LootBox.CharacterBlueprint, index);
            NewCharIndexUniversal = index;
            IsOpeningChar = true;
            UpdateInventory();
        } else {
            BackToSideChoosePanel();
        }
    }

    public void UpdateInventory() {
        for (int i = 0; i < Hunters.GetLength(0); i++) {
            if (GSMan.playerData.CharUnlocks[Hunters[i].index] == false)
                Hunters[i].Unlocked = false;
            else
                Hunters[i].Unlocked = true;
            for (int a = 0; a < Hunters[i].Skins.GetLength(0); a++) {
                if (GSMan.playerData.SkinUnlocks[Hunters[i].Skins[a].index] == false)
                    Hunters[i].Skins[a].Unlocked = false;
                else
                    Hunters[i].Skins[a].Unlocked = true;
            }
        }
        for (int i = 0; i < Players.GetLength(0); i++) {
            if (playerData.CharUnlocks[Players[i].index] == false)
                Players[i].Unlocked = false;
            else
                Players[i].Unlocked = true;
            for (int a = 0; a < Players[i].Skins.GetLength(0); a++) {
                if (playerData.SkinUnlocks[Players[i].Skins[a].index] == false)
                    Players[i].Skins[a].Unlocked = false;
                else
                    Players[i].Skins[a].Unlocked = true;
            }
        }
    }

    public void LoadSideChoosePanel() {
        UpdateInventory();
        if (GSMan.playerData.SkinRelics == 0) {
            SkinRelicButton.SetActive(false);
        } else {
            SkinRelicButton.SetActive(true);
            SkinRelicButton.transform.GetChild(0).GetComponent<Text>().text = "Open Camo Relic (" + GSMan.playerData.SkinRelics + ")";
        }
        if (GSMan.playerData.CharacterBlueprint == 0) {
            CharBlueprintButton.SetActive(false);
        } else {
            CharBlueprintButton.SetActive(true);
            CharBlueprintButton.transform.GetChild(0).GetComponent<Text>().text = "Open character blueprint (" + GSMan.playerData.CharacterBlueprint + ")";
        }
    }

    public void LoadClassChoosePanel() {
        int ClassCount = IsHunter ? Hunters.Length : Players.Length;
        for (int i = 0; i < 8; i++) {
            if (i < ClassCount) {
                ClassButtons[i].SetActive(true);
                Class c = IsHunter ? Hunters[i] : Players[i];
                ClassButtons[i].GetComponent<Image>().sprite = c.Look;
                ClassButtons[i].transform.GetChild(0).GetComponent<Text>().text = c.Name;
                if (!c.Unlocked) {
                    ClassButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                    ClassButtons[i].GetComponent<Button>().interactable = false;
                } else {
                    ClassButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                    ClassButtons[i].GetComponent<Button>().interactable = true;
                }
            } else {
                ClassButtons[i].SetActive(false);
            }
        }
    }

    public void LoadSkinChoosePanel() {
        if (IsHunter == true) {
            int SkinCount = Hunters[ClassIndex].Skins.GetLength(0);
            for (int i = 0; i < SkinCount; i++) {
                SkinButtons[i].SetActive(true);
                SkinButtons[i].GetComponent<Image>().sprite = Hunters[ClassIndex].Skins[i].skinSprite;
                SkinButtons[i].transform.GetChild(0).GetComponent<Text>().text = Hunters[ClassIndex].Skins[i].Name;
                if (Hunters[ClassIndex].Skins[i].Unlocked == false) {
                    SkinButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                    SkinButtons[i].GetComponent<Button>().interactable = false;
                } else {
                    SkinButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                    SkinButtons[i].GetComponent<Button>().interactable = true;
                }
            }
            for (int i = SkinCount; i < 12; i++) {
                SkinButtons[i].SetActive(false);
            }
        } else {
            int SkinCount = Players[ClassIndex].Skins.GetLength(0);
            for (int i = 0; i < SkinCount; i++) {
                SkinButtons[i].SetActive(true);
                SkinButtons[i].GetComponent<Image>().sprite = Players[ClassIndex].Skins[i].skinSprite;
                SkinButtons[i].transform.GetChild(0).GetComponent<Text>().text = Players[ClassIndex].Skins[i].Name;
                if (Players[ClassIndex].Skins[i].Unlocked == false) {
                    SkinButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                    SkinButtons[i].GetComponent<Button>().interactable = false;
                } else {
                    SkinButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                    SkinButtons[i].GetComponent<Button>().interactable = true;
                }
            }
            for (int i = SkinCount; i < 12; i++) {
                SkinButtons[i].SetActive(false);
            }
        }
    }

    public void LoadSkinPanel() {
        if (IsHunter) {
            skinName.text = Hunters[ClassIndex].Skins[SkinIndex].Name;
            Color newColor = new Color32(0, 0, 0, 0);
            switch (Hunters[ClassIndex].Skins[SkinIndex].Rarity) {
                case "Basic":
                    newColor = new Color32(0, 55, 255, 60);
                    break;
                case "Common":
                    newColor = new Color32(0, 223, 255, 60);
                    break;
                case "Uncommon":
                    newColor = new Color32(0, 255, 76, 60);
                    break;
                case "Rare":
                    newColor = new Color32(241, 255, 0, 60);
                    break;
                case "Legendary":
                    newColor = new Color32(241, 143, 0, 60);
                    break;
                default:
                    break;
            }
            skinRarity.text = Hunters[ClassIndex].Skins[SkinIndex].Rarity;
            skinRarity.transform.parent.gameObject.GetComponent<Image>().color = newColor;
            newColor.a = 1;
            skinRarity.color = newColor;
            skinDescription.text = Hunters[ClassIndex].Skins[SkinIndex].Description;
            string temp = "Added to the game on";
            temp = temp + Hunters[ClassIndex].Skins[SkinIndex].DateAdded;
            skinDate.text = temp;
            skinSkin.sprite = Hunters[ClassIndex].Skins[SkinIndex].skinSprite;
        } else {
            skinName.text = Players[ClassIndex].Skins[SkinIndex].Name;
            Color newColor = new Color32(0, 0, 0, 0);
            switch (Players[ClassIndex].Skins[SkinIndex].Rarity) {
                case "Basic":
                    newColor = new Color32(0, 55, 255, 60);
                    break;
                case "Common":
                    newColor = new Color32(0, 223, 255, 60);
                    break;
                case "Uncommon":
                    newColor = new Color32(0, 255, 76, 60);
                    break;
                case "Rare":
                    newColor = new Color32(241, 255, 0, 60);
                    break;
                case "Legendary":
                    newColor = new Color32(241, 143, 0, 60);
                    break;
                default:
                    break;
            }
            skinRarity.text = Players[ClassIndex].Skins[SkinIndex].Rarity;
            skinRarity.transform.parent.gameObject.GetComponent<Image>().color = newColor;
            newColor.a = 1;
            skinRarity.color = newColor;
            skinDescription.text = Players[ClassIndex].Skins[SkinIndex].Description;
            string temp = "Added to the game on";
            temp = temp + Players[ClassIndex].Skins[SkinIndex].DateAdded;
            skinDate.text = temp;
            skinSkin.sprite = Players[ClassIndex].Skins[SkinIndex].skinSprite;
        }

    }

    public void ChooseCharBlueprint() {
        SideChoosePanel.SetActive(false);
        CharBlueprintOpenPanel.SetActive(true);
        CharShine.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        CharBlueprintOpenButton.SetActive(true);
        Character.SetActive(false);
        CharBlueprint.SetActive(true);
    }

    public void ChooseSkinRelic() {
        SideChoosePanel.SetActive(false);
        SkinRelicOpenPanel.SetActive(true);
        SKinShine.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        SkinRelicOpenButton.SetActive(true);
        Skin.SetActive(false);
        SkinRelic.SetActive(true);
    }

    public void ChooseSide(bool isHunter) {
        IsHunter = isHunter;
        LoadClassChoosePanel();
        SideChoosePanel.SetActive(false);
        ClassChoosePanel.SetActive(true);
    }

    public void ChooseClass(int classIndex) {
        bool temp = false;
        if (IsHunter) {
            if (Hunters[ClassIndex].Unlocked == true)
                temp = true;
        }
        if (IsHunter == false) {
            if (Players[ClassIndex].Unlocked == true)
                temp = true;
        }
        if (temp) {
            ClassIndex = classIndex;
            LoadSkinChoosePanel();
            ClassChoosePanel.SetActive(false);
            SkinChoosePanel.SetActive(true);
        }
    }

    public void ChooseSkin(int skinIndex) {
        bool temp = false;
        if (IsHunter) {
            if (Hunters[ClassIndex].Skins[skinIndex].Unlocked == true)
                temp = true;
        }
        if (IsHunter == false) {
            if (Players[ClassIndex].Skins[skinIndex].Unlocked == true)
                temp = true;
        }
        if (temp) {
            SkinIndex = skinIndex;
            LoadSkinPanel();
            SkinChoosePanel.SetActive(false);
            SkinPanel.SetActive(true);
        }
    }

    public void BackToSkinChoosePanel() {
        LoadSkinChoosePanel();
        SkinPanel.SetActive(false);
        SkinChoosePanel.SetActive(true);
    }

    public void BackToClassChoosePanel() {
        LoadClassChoosePanel();
        SkinChoosePanel.SetActive(false);
        ClassChoosePanel.SetActive(true);
    }

    public void BackToSideChoosePanel() {
        LoadSideChoosePanel();
        SideChoosePanel.SetActive(true);
        ClassChoosePanel.SetActive(false);
        SkinRelicOpenPanel.SetActive(false);
        CharBlueprintOpenPanel.SetActive(false);
    }

    void Awake() {
        GSMan = GameObject.Find("GameSparksManager").GetComponent<GameSparksManager>();
        playerData = GSMan.playerData;
    }

    void Update() {
        if (IsOpeningSkin == true) {
            SKinShine.transform.localScale = SKinShine.transform.localScale * (1 + Time.deltaTime * 5);
            if (SKinShine.transform.localScale.x > 50) {
                IsOpeningSkin = false;
                Skin.SetActive(true);
                switch (newRarity) {
                    case 0:
                        Skin.GetComponent<Image>().sprite = GSMan.CommonSkins[NewSkinIndex].Skin;
                        Skin.transform.GetChild(0).gameObject.GetComponent<Text>().text = GSMan.CommonSkins[NewSkinIndex].Name;
                        break;
                    case 1:
                        Skin.GetComponent<Image>().sprite = GSMan.UnCommonSkins[NewSkinIndex].Skin;
                        Skin.transform.GetChild(0).gameObject.GetComponent<Text>().text = GSMan.UnCommonSkins[NewSkinIndex].Name;
                        break;
                    case 2:
                        Skin.GetComponent<Image>().sprite = GSMan.RareSkins[NewSkinIndex].Skin;
                        Skin.transform.GetChild(0).gameObject.GetComponent<Text>().text = GSMan.RareSkins[NewSkinIndex].Name;
                        break;
                    case 3:
                        Skin.GetComponent<Image>().sprite = GSMan.RareSkins[NewSkinIndex].Skin;
                        Skin.transform.GetChild(0).gameObject.GetComponent<Text>().text = GSMan.RareSkins[NewSkinIndex].Name;
                        break;
                    default:
                        break;
                }
                SkinRelic.SetActive(false);
                SkinRelicOpenButton.SetActive(false);
                SKinShine.CrossFadeAlpha(0, 2, true);
            }
        }
        if (IsOpeningChar == true) {
            CharShine.transform.localScale = CharShine.transform.localScale * (1 + Time.deltaTime * 5);
            if (CharShine.transform.localScale.x > 50) {
                IsOpeningChar = false;
                Character.SetActive(true);
                CharBlueprint.SetActive(false);
                CharShine.CrossFadeAlpha(0, 2, true);
                CharBlueprintOpenButton.SetActive(false);
                Character.GetComponent<Image>().sprite = GSMan.CharData[NewCharIndexUniversal].look;
                Character.transform.GetChild(0).gameObject.GetComponent<Text>().text = GSMan.CharData[NewCharIndexUniversal].Name;
            }
        }
    }
}
