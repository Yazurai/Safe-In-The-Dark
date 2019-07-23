using UnityEngine;
using Photon;
using System.Collections;

public class TeleporterController : Photon.MonoBehaviour 
{
	public GameObject LinkedTeleporter;
	public GameObject [] Parts;
    public GameObject particleEffect;
	public int Mode;
	public bool Teleported;

	float Timer;
	bool Active;

	[PunRPC]
	void SetLinkedTeleporter(int TeleporterId)
	{
		LinkedTeleporter = PhotonView.Find (TeleporterId).gameObject;
	}

	[PunRPC]
	void SetNeutral () 
	{
		//gameObject.tag = "Neutral";
		Mode = 0;
		for (int i = 0; i < 4; i++)
		{
			Parts [i].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
		}
        var main = particleEffect.GetComponent<ParticleSystem>().main;
        main.startColor = new Color(1, 1, 1, 1);
	}

	[PunRPC]
	void SetHunterOnly () 
	{
		//gameObject.tag = "HunterOnly";
		Mode = 1;
		for (int i = 0; i < 4; i++)
		{
			Parts [i].GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0, 1);
		}
        var main = particleEffect.GetComponent<ParticleSystem>().main;
        main.startColor = new Color(1, 0, 0, 1);

    }

    [PunRPC]
	void SetPlayerOnly () 
	{
		//gameObject.tag = "PlayerOnly";
		Mode = 2;
		for (int i = 0; i < 4; i++)
		{
			Parts [i].GetComponent<SpriteRenderer> ().color = new Color (0, 0, 1, 1);
		}
        var main = particleEffect.GetComponent<ParticleSystem>().main;
        main.startColor = new Color(0, 0, 1, 1);

    }

    public void DisableTeleporter()
	{
		for (int i = 0; i < 4; i++)
		{
			Color temp = Parts [i].GetComponent<SpriteRenderer> ().color;
			temp.a = 0.4f;
			Parts [i].GetComponent<SpriteRenderer> ().color = temp;
		}
        particleEffect.SetActive(false);
        Timer = 10;
		Active = false;
	}

	public void EnableTeleporter()
	{
		for (int i = 0; i < 4; i++)
		{
			Color temp = Parts [i].GetComponent<SpriteRenderer> ().color;
			temp.a = 1;
			Parts [i].GetComponent<SpriteRenderer> ().color = temp;
		}
        particleEffect.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
		if (Teleported == false && Active == true) 
		{
			if (Mode == 0) 
			{
				if (coll.tag == "Player") 
				{
					if (PhotonNetwork.isMasterClient == false)
					{
                        LinkedTeleporter.GetComponent<TeleporterController> ().Teleported = true;
                        LinkedTeleporter.GetComponent<TeleporterController>().DisableTeleporter();
                        Vector3 newPosition = LinkedTeleporter.transform.position;
                        newPosition.z = -1;
                        coll.gameObject.transform.position = newPosition;
                        coll.gameObject.GetComponent<PlayerController>().Move(newPosition);
                        DisableTeleporter();
					}
				}
				if (coll.tag == "Hunter")
				{
					if (PhotonNetwork.isMasterClient == true)
					{
						LinkedTeleporter.GetComponent<TeleporterController> ().Teleported = true;
                        LinkedTeleporter.GetComponent<TeleporterController>().DisableTeleporter();
                        Vector3 newPosition = LinkedTeleporter.transform.position;
                        newPosition.z = -1;
                        coll.gameObject.transform.position = newPosition;
                        coll.gameObject.GetComponent<HunterController>().Move(newPosition);
                        DisableTeleporter();
					}
				}
			}
			if (Mode == 1) 
			{
				if (coll.tag == "Hunter") 
				{
					if (PhotonNetwork.isMasterClient == true) 
					{
						LinkedTeleporter.GetComponent<TeleporterController> ().Teleported = true;
                        LinkedTeleporter.GetComponent<TeleporterController>().DisableTeleporter();
                        Vector3 newPosition = LinkedTeleporter.transform.position;
                        newPosition.z = -1;
                        coll.gameObject.transform.position = newPosition;
                        coll.gameObject.GetComponent<HunterController>().Move(newPosition);
                        DisableTeleporter();
					}
				}
			}
			if (Mode == 2) 
			{
				if (coll.tag == "Player") 
				{
					if (PhotonNetwork.isMasterClient == false) 
					{
						LinkedTeleporter.GetComponent<TeleporterController> ().Teleported = true;
                        LinkedTeleporter.GetComponent<TeleporterController>().DisableTeleporter();
                        Vector3 newPosition = LinkedTeleporter.transform.position;
                        newPosition.z = -1;
                        coll.gameObject.transform.position = newPosition;
                        coll.gameObject.GetComponent<PlayerController>().Move(newPosition);
                        DisableTeleporter();
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Player") 
		{
			if (PhotonNetwork.isMasterClient == false)
				Teleported = false;
		}
		if (coll.tag == "Hunter") 
		{
			if (PhotonNetwork.isMasterClient == true)
				Teleported = false;
		}
	}

	void Start()
	{
		Timer = 10;
		Active = true;
        Teleported = false;
	}

	void Update()
	{
		if (Active == false) 
		{
			Timer = Timer - Time.deltaTime;
			if (Timer < 0) 
			{
				Active = true;
				EnableTeleporter ();
			}
		}
	}
}
