using UnityEngine;

public class PlatformRotation : MonoBehaviour {
    public bool TurnDirection;

    public Animator anim;

    void Start() {
        if (TurnDirection)
            anim.SetTrigger("Clock Wise");
        else
            anim.SetTrigger("Not Clock Wise");
    }
}