using UnityEngine;
using System.Collections;

public class Room1Manager : MonoBehaviour
{
    public int StationsLeft;
    public GameObject Portal;
    public GameObject OverloadButton;

    public MessageManager MsgMan;
    public TutorialPlayerController tutPlayCtrl;

	public void StartRoom ()
    {
        StationsLeft = 4;
        Portal.SetActive(false);
        OverloadButton.SetActive(true);
        tutPlayCtrl.canInteract = true;
	}

    public void StationActivated()
    {
        StationsLeft--;
        if(StationsLeft == 0)
        {
            Portal.SetActive(true);
            MsgMan.Next();
        }
    }
}
