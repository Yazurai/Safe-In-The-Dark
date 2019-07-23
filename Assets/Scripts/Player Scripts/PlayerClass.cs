using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerBaseStat {
    public int Speed;
    public int Vision;
}

public abstract class PlayerClass : MonoBehaviour {
    protected string Name;
    protected string Ability1Name;
    protected string Ability2Name;
    protected string Ability1Desc;
    protected string Ability2Desc;



    protected PlayerBaseStat BaseStat;

    protected abstract void Ability1Activate();
    protected abstract void Ability1Deactivate();
    protected abstract void Ability2Activate();
    protected abstract void Ability2Deactivate();


}
