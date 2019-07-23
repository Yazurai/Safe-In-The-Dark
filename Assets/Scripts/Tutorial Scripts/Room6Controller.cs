using UnityEngine;
using System.Collections;

public class Room6Controller : MonoBehaviour
{
    public TutorialOverwatchController Bot;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Bot.Activated = true;
        }
    } 
}
