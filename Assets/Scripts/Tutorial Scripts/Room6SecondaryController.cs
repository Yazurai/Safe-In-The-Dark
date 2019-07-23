using UnityEngine;
using System.Collections;

public class Room6SecondaryController : MonoBehaviour
{
    public GameObject Bot;

	void OnTriggerEnter2D (Collider2D coll)
    {
	    if(coll.gameObject.tag == "Player")
        {
            Bot.SetActive(false);
        }
	}
}
