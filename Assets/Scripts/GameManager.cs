using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;
using System.Collections;

public class GameManager : Photon.PunBehaviour 
{
	public int StationsLeft;
	public Text information;
	public Text Notification;
	public NetworkManager NetworkMan;
	public Text EventNotification;
	bool EventNotificationIsShown;
	public GameObject StationSpawner;

	bool GivenXP;
	int ActiveEvent;
	float EventTimer;
	bool GameEnded;
	float timer;
	float EventNotificationTimer;
	bool EventActive;
	float EventDurationTimer;
	bool Lost;

	Vector3 PlayerPreviousPosition;
	Vector3 HunterPreviousPosition;

	[PunRPC]
	public void HunterSpeedIncreased()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The speed of the hunter is now multiplied by 1.5x!!!";
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().speedMultiplier = 1.5f;
		}
	}

	[PunRPC]
	public void HunterSpeedIncreasedEnding()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().speedMultiplier = 1;
		}
	}

	[PunRPC]
	public void HunterViewRadiusIncreased()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The hunter now has 2 times bigger viewing radius!!!";
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().ViewAreaMultiplier = 2;
			if (GameObject.FindGameObjectWithTag ("Hunter").transform.GetChild (0).GetComponent<Camera> ().orthographicSize == 7) 
			{
				GameObject.FindGameObjectWithTag ("Hunter").transform.GetChild (0).GetComponent<Camera> ().orthographicSize = 14;
			}
			else
			{
				GameObject.FindGameObjectWithTag ("Hunter").transform.GetChild (0).GetComponent<Camera> ().orthographicSize = 5;
			}
		}
	}

	[PunRPC]
	public void HunterViewRadiusIncreasedEnding()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().ViewAreaMultiplier = 1;
			if (GameObject.FindGameObjectWithTag ("Hunter").transform.GetChild (0).GetComponent<Camera> ().orthographicSize == 14) 
			{
				GameObject.FindGameObjectWithTag ("Hunter").transform.GetChild (0).GetComponent<Camera> ().orthographicSize = 7;
			}
			else
			{
				GameObject.FindGameObjectWithTag ("Hunter").transform.GetChild (0).GetComponent<Camera> ().orthographicSize = 2.5f;
			}
		}
	}

	[PunRPC]
	public void HunterBetterVisibility()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The hunter now can see much better even when close to the player!!!";
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().Filter = 0.4f;
		}
	}

	[PunRPC]
	public void HunterBetterVisibilityEnding()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().Filter = 0.75f;
		}
	}

	[PunRPC]
	public void HunterShorterCooldown()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The hunter's ability cooldown times are now half of what they used to be!!!";
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().CooldownMultiplier = 0.5f;
		}
	}

	[PunRPC]
	public void HunterShorterCooldownEnding()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().CooldownMultiplier = 1;
		}
	}

	[PunRPC]
	public void ArenaTransfer()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The Players are transferred into a small EMPTY area!!!";
		if (PhotonNetwork.isMasterClient) 
		{
			HunterPreviousPosition = GameObject.FindGameObjectWithTag ("Hunter").transform.position;
			GameObject.FindGameObjectWithTag ("Hunter").transform.position = new Vector3 (90, 7, -2);
		}
		else
		{
			PlayerPreviousPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameObject.FindGameObjectWithTag ("Player").transform.position = new Vector3 (125, -28, -2);
		}
	}

	[PunRPC]
	public void ArenaTransferEnding()
	{
		if (PhotonNetwork.isMasterClient) 
		{
			GameObject.FindGameObjectWithTag ("Hunter").transform.position = HunterPreviousPosition;
		}
		else
		{
			GameObject.FindGameObjectWithTag ("Player").transform.position = PlayerPreviousPosition;
		}
	}

	[PunRPC]
	public void StationsInvisible()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "All stations are now invisible!!!";
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Station");
		for (int i = 0; i < temp.GetLength(0); i++) 
		{
			temp [i].GetComponent<StationBehaviour> ().IsShown = false;
		}
	}

	[PunRPC]
	public void StationsInvisibleEnding()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Station");
		for (int i = 0; i < temp.GetLength(0); i++) 
		{
			temp [i].GetComponent<StationBehaviour> ().IsShown = true;
		}
	}

	[PunRPC]
	public void MoreStations()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "Now there are permanently more stations!!!";
		GameObject[] SpawnPoints = StationSpawner.GetComponent<StationSpawnerController> ().GetSpawns ();
		if (PhotonNetwork.isMasterClient) 
		{
			for (int i = 0; i < 10; i++) 
			{
				PhotonNetwork.Instantiate ("Station", SpawnPoints [i].transform.position, Quaternion.identity, 0);
			}
		}
	}

	[PunRPC]
	public void MoreStationEnding()
	{
	}

	[PunRPC]
	public void FasterPlayer()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The Player is 1.5 times faster than before!!!";
		if (!NetworkMan.IsHunter) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovementController> ().SpeedMultiplier = 1.5f;
		}
	}

	[PunRPC]
	public void FasterPlayerEnding()
	{
		if (!NetworkMan.IsHunter) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovementController>().SpeedMultiplier = 1;
		}
	}

	[PunRPC]
	public void PlayerBetterVisibility()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The Player now can see better than before!!!";
		if (PhotonNetwork.isMasterClient == false) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().filter = 75;
		}
	}

	[PunRPC]
	public void PlayerBetterVisibilityEnding()
	{
		if (PhotonNetwork.isMasterClient == false) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().filter = 40;
		}
	}

	[PunRPC]
	public void ShowAllStation()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "All stations are now visible!!!";
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Station");
		for (int i = 0; i < temp.GetLength(0); i++) 
		{
			temp [i].GetComponent<StationBehaviour> ().TotallyShown = true;
		}
	}

	[PunRPC]
	public void ShowAllStationEnding()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Station");
		for (int i = 0; i < temp.GetLength(0); i++) 
		{
			temp [i].GetComponent<StationBehaviour> ().TotallyShown = false;
		}
	}

	[PunRPC]
	public void PlayerShorterCooldown()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "The player's ability cooldown times are now half of what they used to be!!!";
		if (PhotonNetwork.isMasterClient == false) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().CooldownMultiplier = 0.5f;
		}
	}

	[PunRPC]
	public void PlayerShorterCooldownEnding()
	{
		if (PhotonNetwork.isMasterClient == false) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().CooldownMultiplier = 1;
		}
	}

	[PunRPC]
	public void SuperSpeed()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
		EventNotification.text = "SUPER SPEED! All movement speed is now 5 times faster than before!";
        NetworkMan.Player.GetComponent<PlayerMovementController>().SpeedMultiplier = 5;
	}

	[PunRPC]
	public void SuperSpeedEnding()
	{
        NetworkMan.Player.GetComponent<PlayerMovementController>().SpeedMultiplier = 1;
    }

	[PunRPC]
	public void HunterWin()
	{
		if (NetworkMan.IsHunter) 
		{
			//Vibrate ();
			Notification.transform.parent.gameObject.SetActive (true);
			Notification.gameObject.SetActive (true);
			Notification.text = "You've Won!";
			Lost = false;
			GameEnded = true;
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().GameEnded = true;            
		} 
		else 
		{
			//Vibrate ();
			Notification.transform.parent.gameObject.SetActive (true);
			Notification.gameObject.SetActive (true);
			Notification.text = "You've Lost!";
			Lost = true;
			GameEnded = true;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().GameEnded = true;            
        }
	}

	[PunRPC]
	public void PlayerWin()
	{
		if (NetworkMan.IsHunter) 
		{
			//Vibrate ();
			Notification.transform.parent.gameObject.SetActive (true);
			Notification.gameObject.SetActive (true);
			Notification.text = "You've Lost!";
			Lost = true;
			GameEnded = true;
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<HunterController> ().GameEnded = true;
		} 
		else 
		{
			//Vibrate ();
			Notification.transform.parent.gameObject.SetActive (true);
			Notification.gameObject.SetActive (true);
			Notification.text = "You've Won!";
			Lost = false;
			GameEnded = true;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().GameEnded = true;
		}
	}

	[PunRPC]
	public void DestroyStation()
	{
		StationsLeft--;
	}

	void Start()
	{
		information = GameObject.FindGameObjectWithTag ("Information").GetComponent<Text>();
		NetworkMan = GameObject.Find ("NetworkManager").GetComponent<NetworkManager>();
		StationsLeft = 10;
		timer = 5;
		Notification = NetworkMan.Notification;
		EventNotificationTimer = 5;
		EventTimer = 60;
		StationSpawner = GameObject.Find ("Station Spawns");
		EventActive = false;
		EventNotification = GameObject.FindGameObjectWithTag ("Event Notification").GetComponent<Text>();
		EventNotification.transform.parent.gameObject.SetActive (false);
		GivenXP = false;
	}

    /*
	void Vibrate()
	{
		Handheld.Vibrate ();
	}*/

	[PunRPC]
	void DisableNotification()
	{
		EventNotification.gameObject.transform.parent.gameObject.SetActive (false);
	}

	void Update()
	{
		if (PhotonNetwork.isMasterClient) 
		{
            //TODO: use a coroutine to do the timing
			if (EventNotificationIsShown == true)
			{
				EventNotificationTimer = EventNotificationTimer - Time.deltaTime;
				if (EventNotificationTimer < 0)
				{
					EventNotificationTimer = 5;
					EventNotificationIsShown = false;
					this.photonView.RPC ("DisableNotification", PhotonTargets.All, null);
					EventNotification.gameObject.transform.parent.gameObject.SetActive (false);
				}
			}

            //Use a coroutine for the whole event system
			if (EventActive)
			{
				EventDurationTimer = EventDurationTimer - Time.deltaTime;
				if (EventDurationTimer < 0) 
				{
					switch (ActiveEvent)
					{
					case 0:
						this.photonView.RPC ("HunterSpeedIncreasedEnding", PhotonTargets.All, null);
						break;
					case 1:
						this.photonView.RPC ("HunterViewRadiusIncreasedEnding", PhotonTargets.All, null);
						break;
					case 2:
						this.photonView.RPC ("HunterBetterVisibilityEnding", PhotonTargets.All, null);
						break;
					case 3:
						this.photonView.RPC ("HunterShorterCooldownEnding", PhotonTargets.All, null);
						break;
					case 4:
						this.photonView.RPC ("ArenaTransferEnding", PhotonTargets.All, null);
						break;
					case 5:
						this.photonView.RPC ("StationsInvisibleEnding", PhotonTargets.All, null);
						break;
					case 6:
						break;
					case 7:
						this.photonView.RPC ("FasterPlayerEnding", PhotonTargets.All, null);
						break;
					case 8:
						this.photonView.RPC ("PlayerBetterVisibilityEnding", PhotonTargets.All, null);
						break;
					case 9:
						this.photonView.RPC ("ShowAllStationEnding", PhotonTargets.All, null);
						break;
					case 10:
						this.photonView.RPC ("PlayerShorterCooldownEnding", PhotonTargets.All, null);
						break;
					case 11:
						this.photonView.RPC ("SuperSpeedEnding", PhotonTargets.All, null);
						break;
					default:
						break;
					}
					EventActive = false;
				}
			}

			if (GameEnded == false)
			{
				EventTimer = EventTimer - Time.deltaTime;
				if (EventTimer < 0) 
				{
					int random = Random.Range(0,11);
					ActiveEvent = random;
					switch (random)
					{
					case 0:
						this.photonView.RPC ("HunterSpeedIncreased", PhotonTargets.All, null);
						break;
					case 1:
						this.photonView.RPC ("HunterViewRadiusIncreased", PhotonTargets.All, null);
						break;
					case 2:
						this.photonView.RPC ("HunterBetterVisibility", PhotonTargets.All, null);
						break;
					case 3:
						this.photonView.RPC ("HunterShorterCooldown", PhotonTargets.All, null);
						break;
					case 4:
						this.photonView.RPC ("ArenaTransfer", PhotonTargets.All, null);
						break;
					case 5:
						this.photonView.RPC ("StationsInvisible", PhotonTargets.All, null);
						break;
					case 6:
						this.photonView.RPC ("MoreStations", PhotonTargets.All, null);
						break;
					case 7:
						this.photonView.RPC ("FasterPlayer", PhotonTargets.All, null);
						break;
					case 8:
						this.photonView.RPC ("PlayerBetterVisibility", PhotonTargets.All, null);
						break;
					case 9:
						this.photonView.RPC ("ShowAllStation", PhotonTargets.All, null);
						break;
					case 10:
						this.photonView.RPC ("PlayerShorterCooldown", PhotonTargets.All, null);
						break;
					case 11:
						this.photonView.RPC ("SuperSpeed", PhotonTargets.All, null);
						break;
					default:
						break;
					}
					EventActive = true;
					EventDurationTimer = 20;
					EventTimer = Random.Range (60, 90);
					EventNotificationIsShown = true;
					EventNotification.gameObject.transform.parent.gameObject.SetActive (true);
				}
			}
		}

		information.text = "Stations left: " + StationsLeft.ToString();
		if (GameEnded && GivenXP == false) 
		{
			timer = timer - Time.deltaTime;
			if (timer < 0) 
			{
				NetworkMan.XPPanel.SetActive (true);
				if (Lost == true) 
				{
					NetworkMan.XPPanel.GetComponent<XPScreenController> ().EndMatch (false);
					GivenXP = true;
				} 
				else 
				{
					NetworkMan.XPPanel.GetComponent<XPScreenController> ().EndMatch (true);
					GivenXP = true;
				}
			}
		}
		if (StationsLeft == 0) 
		{
			if (PhotonNetwork.isMasterClient == true) 
			{
				GameObject SpawnLocation = GameObject.Find ("Station Spawns").GetComponent<StationSpawnerController> ().GetSpawn ();
				PhotonNetwork.Instantiate ("Portal", SpawnLocation.transform.position, Quaternion.identity, 0);
			} 
		}
	}
}
