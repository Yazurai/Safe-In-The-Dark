using UnityEngine;
using UnityEngine.UI;

public class PlayerStatPanelController : MonoBehaviour {
    public Text NameText;
    public Text LevelText;
    public Slider XPSlider;
    public Text SliderLeft;
    public Text SliderRight;
    public Text XPText;

    public Text TokenText;
    public Text WinText;
    public Text LossesText;
    public Text PercentageText;

    public Text CharacterText;
    public Text SkinText;
    public Text TrailText;
    public Text TimeCapsuleText;

    GameSparksManager GSMan;
    PlayerData PData;

    public void Awake() {
        GSMan = GameObject.Find("GameSparksManager").GetComponent<GameSparksManager>();
        PData = GSMan.playerData;
    }

    public void UpdateData() {
        NameText.text = PData.Name;
        LevelText.text = "Level : " + PData.GetLevel().ToString();
        XPSlider.value = PData.XP % 2000;
        SliderLeft.text = "LVL " + PData.GetLevel().ToString();
        SliderRight.text = "LVL " + System.Convert.ToString(PData.GetLevel() + 1);
        XPText.text = GSMan.playerData.XP.ToString() + " Exp.";
        TokenText.text = "Tokens : " + PData.Tokens; ;
        WinText.text = "Wins :" + PData.Wins.ToString(); ;
        LossesText.text = "Losses : " + PData.Losses.ToString();

        PercentageText.text = "Win Ratio : " + PData.GetWinLoseRatio().ToString();

        int AllCharCount = 0;
        foreach (var item in GSMan.playerData.CharUnlocks) {
            AllCharCount++;
        }
        CharacterText.text = "Char. Unlocks : " + PData.GetCharUnlockCount().ToString() + "/" + AllCharCount.ToString();

        int AllSkinCounter = 0;
        foreach (var item in GSMan.playerData.SkinUnlocks) {
            AllSkinCounter++;
        }
        SkinText.text = "Skin Unlocks : " + PData.GetSkinUnlockCount().ToString() + "/" + AllSkinCounter.ToString();

        int AllTrailCounter = 0;
        foreach (var item in GSMan.playerData.TrailUnlocks) {
            AllTrailCounter++;
        }
        TrailText.text = "Trail Unlocks : " + PData.GetTrailUnlockCount().ToString() + "/" + AllTrailCounter.ToString();

        /*
        int TimeCapsuleCounter;
		foreach (var item in GSMan.playerData.TimeCapsuleUnlocks) 
		{
			if (item == true)
				TimeCapsuleCounter++;
		}
		string timecapsuleText = "Time Capsules : " + CharCounter.ToString();
		TimeCapsuleText.text = "";
        */
    }
}
