using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class XPScreenController : MonoBehaviour {
    GameSparksManager GameSparksManager;
    PlayerData PData;

    public GameObject LevelUpPanel;
    public Slider XpSlider;
    public Text PlayerName;
    public Text PlayerLevel;
    public Text Tokens;
    public Text PreviousLevel;
    public Text NextLevel;
    public GameObject ContinueButton;

    float XpTimer;
    float TokenTimer;

    bool IsAfterLevelUp;
    LevelUp LvlUpType;

    int EndToken;
    float currentToken;

    void Awake() {
        IsAfterLevelUp = false;
        LevelUpPanel.SetActive(false);
        XpTimer = 3;
        TokenTimer = 3;
        GameSparksManager = GameObject.Find("GameSparksManager").GetComponent<GameSparksManager>();
        PData = GameSparksManager.playerData;
        ContinueButton.SetActive(false);
    }

    public void RestartGame() {
        PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene ("Test Scene");
    }

    public void setDefault() {
        LevelUpPanel.SetActive(false);
        Tokens.gameObject.SetActive(false);
        IsAfterLevelUp = false;

        XpSlider.value = PData.XP % 2000;
        Tokens.text = PData.Tokens.ToString() + " Tokens";
        PreviousLevel.text = "LVL " + PData.GetLevel().ToString();
        NextLevel.text = "LVL " + (PData.GetLevel() + 1).ToString();

        PlayerName.text = PData.Name;

        PlayerLevel.text = "Level " + PData.GetLevel().ToString();
    }

    public void EndMatch(bool isWin) {
        setDefault();
        currentToken = PData.Tokens;

        LvlUpType = PData.SetXP(isWin);
        EndToken = GameSparksManager.playerData.Tokens;

        GameSparksManager.EndMatch(isWin);

        StartCoroutine("AnimateXpScreen");
    }

    IEnumerator AnimateXpScreen() {
        while (XpTimer >= 0) {
            XpTimer = XpTimer - Time.deltaTime;

            if (LvlUpType == LevelUp.NOLEVELUP || IsAfterLevelUp) {
                XpSlider.value = Mathf.Lerp(XpSlider.value, PData.XP % 2000, Time.deltaTime / 3);
            } else {
                XpSlider.value = Mathf.Lerp(XpSlider.value, 2000, Time.deltaTime / 3);
            }
            yield return null;
        }
        if (LvlUpType == LevelUp.NOLEVELUP || IsAfterLevelUp) {
            XpSlider.value = PData.XP % 2000;
            StartCoroutine("AnimateTokens");
        } else {
            StartCoroutine("ShowLevelUpPanel");
        }
    }

    IEnumerator ShowLevelUpPanel() {
        LevelUpPanel.SetActive(true);
        LevelUpPanelController LvLUpCtrl = LevelUpPanel.GetComponent<LevelUpPanelController>();
        LvLUpCtrl.NewLevelText.text = PData.GetLevel().ToString();
        if (LvlUpType == LevelUp.BIGLEVELUP) {
            LvLUpCtrl.Awards.text = "Awards   500 Tokens   1 Blueprint";
        } else {
            LvLUpCtrl.Awards.text = "Awards   " + "250 Tokens";
        }
        PlayerLevel.text = "Level " + PData.GetLevel().ToString();

        yield return new WaitForSeconds(3);

        LevelUpPanel.SetActive(false);
        XpSlider.value = 0;
        IsAfterLevelUp = true;

        StartCoroutine("AnimateXpScreen");
    }

    IEnumerator AnimateTokens() {
        Tokens.gameObject.SetActive(true);
        while (TokenTimer >= 0) {
            TokenTimer = TokenTimer - Time.deltaTime;

            currentToken = Mathf.Lerp(currentToken, EndToken, Time.deltaTime / 3);
            Tokens.text = Mathf.RoundToInt(currentToken).ToString() + " Tokens";
            yield return null;
        }
        Tokens.text = EndToken.ToString() + " Tokens";
        ContinueButton.SetActive(true);
    }
}
