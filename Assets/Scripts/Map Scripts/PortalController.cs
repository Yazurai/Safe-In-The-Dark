using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D coll)
	{
		GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<GameManager> ().PlayerWin ();
	}
}
