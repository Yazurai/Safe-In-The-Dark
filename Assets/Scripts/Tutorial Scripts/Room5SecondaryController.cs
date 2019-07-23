using UnityEngine;
using System.Collections;

public class Room5SecondaryController : MonoBehaviour
{
    public Room5Controller Room5Ctrl;
	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Room5Ctrl.isActive = true;
        }
    }
}
