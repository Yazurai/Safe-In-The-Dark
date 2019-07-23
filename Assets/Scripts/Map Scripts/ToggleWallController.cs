using UnityEngine;
using Photon;
using System.Collections;

public class ToggleWallController : Photon.MonoBehaviour 
{
	float Timer;
	bool Active;

	[PunRPC]
	void Activate()
	{
		Timer = 3.5f;
		Active = true;
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
	}

    [PunRPC]
    void SetTransform(Vector3 position, Vector3 Rotation, Vector3 Scale)
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = Quaternion.Euler(Rotation);
        gameObject.transform.localScale = Scale;
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			coll.gameObject.GetComponent<PlayerController> ().ToggleWall = gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			coll.gameObject.GetComponent<PlayerController> ().ToggleWall = null;
		}
	}

	void Update () 
	{
		if (Active == true) 
		{
			Timer = Timer - Time.deltaTime;
			if (Timer < 0) 
			{
				Active = false;
				gameObject.transform.GetChild (0).gameObject.SetActive (false);
			}
		}
	}
}
