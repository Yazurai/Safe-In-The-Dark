using UnityEngine;
using System.Collections;

public class Room7Controller : MonoBehaviour
{
    public TutorialPlayerController TutPlayerCtrl;

	public void StartRoom ()
    {
        TutPlayerCtrl.Simulation = true;
	}
}
