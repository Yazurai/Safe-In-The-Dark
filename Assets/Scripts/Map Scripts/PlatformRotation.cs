using UnityEngine;

public class PlatformRotation : Photon.MonoBehaviour
{
    public Animator anim;
    
    [PunRPC]
    void TurnDirection(bool isClockwise)
    {
        if (isClockwise)
            anim.SetTrigger("Clock Wise");
        else
            anim.SetTrigger("Not Clock Wise");
    }
}