using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour
{
    public MessageManager MessageManager;
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            MessageManager.Next();
            gameObject.SetActive(false);
        }
    }
}
