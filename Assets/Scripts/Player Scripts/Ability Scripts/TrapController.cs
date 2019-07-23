using UnityEngine;
using Photon;
using System.Collections;

public class TrapController : Photon.MonoBehaviour 
{
	float Timer;

	void Start()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			Timer = 7.5f;
		}
	}

	void Update()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			Timer = Timer - Time.deltaTime;
			if (Timer < 0) 
			{
				PhotonNetwork.Destroy (gameObject);
			}
		}
	}
}
