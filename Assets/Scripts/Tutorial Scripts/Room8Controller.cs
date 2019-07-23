using UnityEngine;
using System.Collections;

public class Room8Controller : MonoBehaviour
{
    public int Activated;
    public MessageManager MsgMan;
	
    public void ActivateOne()
    {
        Activated++;
        if(Activated == 2)
        {
            MsgMan.Next();
        }
    }
}
