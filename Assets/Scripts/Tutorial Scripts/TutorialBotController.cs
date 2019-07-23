using UnityEngine;
using System.Collections;

public class TutorialBotController : MonoBehaviour
{
    public MessageManager MsgMan;
    public TutorialPlayerController TutPlayMan;
    public GameObject Gate;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            MsgMan.Next();
            TutPlayMan.Simulation = false;
            Gate.SetActive(true);
        }
    }
}
