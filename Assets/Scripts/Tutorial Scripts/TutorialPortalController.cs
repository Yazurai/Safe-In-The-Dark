using UnityEngine;
using System.Collections;

public class TutorialPortalController : MonoBehaviour
{
    public MessageManager MessageManager;

	void OnTriggerEnter2D (Collider2D coll)
    {
	    if(coll.gameObject.tag == "Player")
        {
            MessageManager.Next();
            Destroy(gameObject);
        }
	}
}
