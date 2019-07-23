using GameSparks.Core;
using System;
using UnityEngine;

public enum LevelUp { NOLEVELUP, SMALLLEVELUP, BIGLEVELUP };

public class PlayerData : MonoBehaviour {
    public string ID { get; private set; }
    public string Name { get; private set; }
    public int XP { get; private set; }
    public bool[] SkinUnlocks { get; private set; }
    public bool[] CharUnlocks { get; private set; }
    public bool[] TrailUnlocks { get; private set; }
    public bool[] TimeCapsuleUnlocks { get; private set; }
    public int Tokens { get; set; } //These setters are public for the gamesparksmanager(could be improved to private)
    public int SkinRelics { get; set; }
    public int CharacterBlueprint { get; set; }
    public int TimeCapsule { get; set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }

    private GameSparksManager GSMan;

    //TODO: Extract these numbers automatically from the inventory
    public void Start() {
        SkinUnlocks = new bool[35];
        CharUnlocks = new bool[6];
        TrailUnlocks = new bool[54];
    }

    public void SetUpData(GameSparksManager GSManager) {
        GSMan = GSManager;
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LoadPlayerData").Send((response) => {
            if (!response.HasErrors) {
                GSData data = response.ScriptData.GetGSData("player_Data");
                ID = data.GetString("playerID");
                Name = data.GetString("playerName");
                XP = (int)data.GetInt("playerXP");
                string skinUnlocks = data.GetString("playerSkinUnlocks");
                for (int i = 0; i < SkinUnlocks.GetLength(0); i++) {
                    if (skinUnlocks.Substring(i * 2, 2) == " 0")
                        SkinUnlocks[i] = false;
                    else
                        SkinUnlocks[i] = true;
                }
                string charUnlocks = data.GetString("playerCharUnlocks");
                for (int i = 0; i < CharUnlocks.GetLength(0); i++) {
                    if (charUnlocks.Substring(i * 2, 2) == " 0")
                        CharUnlocks[i] = false;
                    else
                        CharUnlocks[i] = true;
                }
                string trailUnlocks = data.GetString("playerTrailUnlocks");
                for (int i = 0; i < TrailUnlocks.GetLength(0); i++) {
                    if (trailUnlocks.Substring(i * 2, 2) == " 0")
                        TrailUnlocks[i] = false;
                    else
                        TrailUnlocks[i] = true;
                }
                Wins = (int)data.GetInt("playerWins");
                Losses = (int)data.GetInt("playerLosses");
            } else {
                //TODO: add error handling
            }
        });
    }

    public LevelUp SetXP(bool isWin) {
        int oldLevel = GetLevel();
        if (isWin) {
            Wins++;
        } else {
            Losses++;
        }
        XP += isWin ? 500 : 200;
        Tokens += isWin ? 25 : 10;
        if (oldLevel != GetLevel()) {
            if (GetLevel() % 5 == 0) {
                GSMan.BigLevelUp();
                return LevelUp.BIGLEVELUP;
            } else {
                return LevelUp.SMALLLEVELUP;
            }
        } else {
            return LevelUp.NOLEVELUP;
        }
    }

    public int GetLevel() {
        return XP / 2000 + 1;
    }

    public float GetWinLoseRatio() {
        return (float)Math.Round(1 / ((float)Wins + Losses) * Wins, 2);
    }

    public int GetCharUnlockCount() {
        int Count = 0;
        foreach (var item in CharUnlocks) {
            if (item == true)
                Count++;
        }
        return Count;
    }

    public int GetSkinUnlockCount() {
        int Count = 0;
        foreach (var item in SkinUnlocks) {
            if (item == true)
                Count++;
        }
        return Count;
    }

    public int GetTrailUnlockCount() {
        int Count = 0;
        foreach (var item in TrailUnlocks) {
            if (item == true)
                Count++;
        }
        return Count;
    }
}
