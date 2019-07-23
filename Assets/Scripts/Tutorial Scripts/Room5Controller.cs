using UnityEngine;
using System.Collections;

public class Room5Controller : MonoBehaviour
{
    public GameObject Bot;
    public GameObject BoostButton;
    public bool isActive;
    public MessageManager MsgMan;
    public TutorialPlayerController tutPlayCtrl;

    public void StartRoom ()
    {
        BoostButton.SetActive(true);
        tutPlayCtrl.canUseAbility2 = true;
    }

    void Update ()
    {
	    if(isActive == true)
        {
            Bot.transform.Translate(1 / 17.5f, 0, 0);
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            MsgMan.Next();
            isActive = false;
        }
        if (coll.gameObject.tag == "Bot")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(131.77f, -19.99f, 0);
            Bot.transform.position = new Vector3(134.321f, -22.05f, 0);
            isActive = false;
        }
    }
}
