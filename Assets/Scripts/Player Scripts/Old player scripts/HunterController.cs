using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon;
using System.Collections;

public class HunterController : Photon.PunBehaviour
{
	public PhotonView PV;
	public GameObject Player;
	public bool GameStarted;
	public Image BackGround;
	public Image Tracker;
	public bool Ready;
	public Sprite[] ShadowHunterSkins;
	public Sprite[] TripTrapHunterSkins;
	public Sprite[] HackerHunterSkins;
	public Sprite[] GhostHunterSkins;
	public GameObject ActiveStation;

	public Material ShadowHunterTrail;
	public Material TripTrapHunterTrail;
	public Material HackerHunterTrail;
	public Material GhostHunterTrail;

	public BoxCollider2D ShadowHunterCollider;
	public PolygonCollider2D TripTrapHunterCollider;
	public PolygonCollider2D HackerHunterCollider;
	public PolygonCollider2D GhostHunterCollider;

	public Text Ability1ButtonText;
	public Text Ability2ButtonText;

    public EventTrigger Activate;
    public EventTrigger Interact;
    public Button Ability1;
    public Button Ability2;

	public string Class;

	GameObject NetworkManager;
	GameObject LobbyPanel;

	GameObject Station;
	float speed;
	public float speedMultiplier;
	public float ViewAreaMultiplier;
	public float Filter;
	public float CooldownMultiplier;

	public bool RadarIsOn;
	float RadarTimer;

	bool TrackerIsOn;
	bool QuickTracker;
	float TrackerTimer;

	Slider Ability1Slider;
	Slider Ability2Slider;

	public float Ability1CoolDown;
	public float Ability2CoolDown;

	float Ability1Timer;
	float Ability2Timer;

	bool Ability1Active;
	bool Ability2Active;

	public float Ability1Duration;
	public float Ability2Duration;

	float Ability1DurationTimer;
	float Ability2DurationTimer;

	public bool GameEnded;

	Color trailColor;

	public void SetShadowHunter()
	{
		Class = "Shadow Hunter";
		Ability1Timer = 60;
		Ability1CoolDown = 60;
		Ability2Timer = 30;
		Ability2CoolDown = 30;
		Ability1DurationTimer = 5;
		Ability1Duration = 5;
		Ability1DurationTimer = 5;
		Ability2Duration = 5;
		Ability1ButtonText.text = "Vanish";
		Ability2ButtonText.text = "Boost";
		ShadowHunterCollider.enabled = true;
		TripTrapHunterCollider.enabled = false;
		HackerHunterCollider.enabled = false;
		GhostHunterCollider.enabled = false;
	}

	public void SetTripTrapHunter()
	{
		Class = "Trip Trap Hunter";
		Ability1Timer = 15;
		Ability1CoolDown = 15;
		Ability2Timer = 45;
		Ability2CoolDown = 45;
		Ability1DurationTimer = 9;
		Ability1Duration = 9;
		Ability1DurationTimer = 5;
		Ability2Duration = 5;
		Ability1ButtonText.text = "Radar";
		Ability2ButtonText.text = "Trap";
		TripTrapHunterCollider.enabled = true;
		ShadowHunterCollider.enabled = false;
		HackerHunterCollider.enabled = false;
		GhostHunterCollider.enabled = false;
	}

	public void SetHackerHunter()
	{
		Class = "Hacker Hunter";
		Ability1Timer = 30;
		Ability1CoolDown = 30;
		Ability2Timer = 45;
		Ability2CoolDown = 45;
		Ability1DurationTimer = 9;
		Ability1Duration = 9;
		Ability1DurationTimer = 5;
		Ability2Duration = 5;
		Ability1ButtonText.text = "Hack";
		Ability2ButtonText.text = "Decoy";
		HackerHunterCollider.enabled = true;
		TripTrapHunterCollider.enabled = false;
		ShadowHunterCollider.enabled = false;
		GhostHunterCollider.enabled = false;
	}

	public void SetGhostHunter()
	{
		Class = "Ghost Hunter";
		Ability1Timer = 30;
		Ability1CoolDown = 30;
		Ability2Timer = 45;
		Ability2CoolDown = 45;
		Ability1DurationTimer = 5;
		Ability1Duration = 5;
		Ability1DurationTimer = 5;
		Ability2Duration = 5;
		Ability1ButtonText.text = "Banshee";
		Ability2ButtonText.text = "Vision";
		GhostHunterCollider.enabled = true;
		ShadowHunterCollider.enabled = false;
		TripTrapHunterCollider.enabled = false;
		HackerHunterCollider.enabled = false;
	}

	[PunRPC]
	public void ReadyUp()
	{
		Ready = true;
	}

	[PunRPC]
	public void SetSkin(int ClassIndex, int SkinIndex)
	{
		{
			switch (ClassIndex) 
			{
			case 0:
				if(PhotonNetwork.isMasterClient == false)
					GameObject.Find("NetworkManager").GetComponent<NetworkManager> ().LobbyPanel.GetComponent <LobbyPanelController> ().Hunter.sprite = ShadowHunterSkins [SkinIndex];
				gameObject.GetComponent<SpriteRenderer> ().sprite = ShadowHunterSkins [SkinIndex];
				break;
			case 1:
				if(PhotonNetwork.isMasterClient == false)
					GameObject.Find("NetworkManager").GetComponent<NetworkManager> ().LobbyPanel.GetComponent <LobbyPanelController> ().Hunter.sprite = TripTrapHunterSkins [SkinIndex];
				gameObject.GetComponent<SpriteRenderer> ().sprite = TripTrapHunterSkins [SkinIndex];
				break;
			case 2:
				if(PhotonNetwork.isMasterClient == false)
					GameObject.Find("NetworkManager").GetComponent<NetworkManager> ().LobbyPanel.GetComponent <LobbyPanelController> ().Hunter.sprite = HackerHunterSkins [SkinIndex];
				gameObject.GetComponent<SpriteRenderer> ().sprite = HackerHunterSkins [SkinIndex];
				break;
			case 3:
				if(PhotonNetwork.isMasterClient == false)
					GameObject.Find("NetworkManager").GetComponent<NetworkManager> ().LobbyPanel.GetComponent <LobbyPanelController> ().Hunter.sprite = GhostHunterSkins [SkinIndex];
				gameObject.GetComponent<SpriteRenderer> ().sprite = GhostHunterSkins [SkinIndex];
				break;
			default:
			    break;
			}
		}
	}

	[PunRPC]
	public void SetTrail(Quaternion TrailColor, int ClassIndex)
	{
        var main = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().main;
        main.startColor = new Color(TrailColor.x, TrailColor.y, TrailColor.z, TrailColor.w);
        switch (ClassIndex)
        {
            case 0:
                gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = ShadowHunterTrail;
                break;
            case 1:
                gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = TripTrapHunterTrail;
                break;
            case 2:
                gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = HackerHunterTrail;
                break;
            case 3:
                gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = GhostHunterTrail;
                break;
            default:
                break;
        }
    }

	[PunRPC]
	public void SetInvisible()
	{
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1,1,1,0);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

	[PunRPC]
	public void SetVisible()
	{
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1,1,1,1);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

	public void Track(Vector2 position, bool IsQuick)
	{
		Vector2 direction = new Vector2 (position.x - transform.position.x, position.y - transform.position.y);
		float new_rotation = 0;
		new_rotation = 57.2958f * Mathf.Atan (direction.y / direction.x);
		if (direction.x < 0)
			new_rotation = new_rotation + 180;
		if (direction.x == 0) 
		{
			if (direction.y > 0)
				new_rotation = 90;
			else
				new_rotation = -90;
		}
		if (direction.y == 0) 
		{
			if (direction.x > 0)
				new_rotation = 0;
			else
				new_rotation = 180;
		}
		new_rotation = new_rotation - 90;
		Tracker.gameObject.GetComponent<RectTransform> ().rotation = Quaternion.Euler (0, 0, new_rotation);
		if (IsQuick == false) 
		{
			Tracker.CrossFadeAlpha (1, 2, true);
			TrackerIsOn = true;
			TrackerTimer = 2;
			QuickTracker = false;
		} 
		else 
		{
			Tracker.CrossFadeAlpha (1, 0.3f, true);
			TrackerIsOn = true;
			TrackerTimer = 0.3f;
			QuickTracker = true;
		}
	}

	public void BansheeScreamBeginning()
	{
		Track (new Vector2 (Player.transform.position.x, Player.transform.position.y), false);
	}

	public void RadarBeginning()
	{
		RadarIsOn = true;
		RadarTimer = 1;
	}

	public void RadarEnding()
	{
		RadarTimer = 1;
		RadarIsOn = false;
	}

	public void Boost()
	{
		speed = 8.5f;
	}

	public void SetNormalSpeed()
	{
		speed = 15;
	}

	public void  IncreaseViewArea()
	{
		gameObject.transform.GetChild (0).GetComponent<Camera> ().orthographicSize = 7 * ViewAreaMultiplier;
	}

	public void SetNormalViewArea()
	{
		gameObject.transform.GetChild (0).GetComponent<Camera> ().orthographicSize = 2.5f * ViewAreaMultiplier;
	}

	public void TrapPlayerBeginning()
	{
		PhotonNetwork.Instantiate ("Trap", Player.transform.position,Quaternion.identity,0);
	}

	public void SpawnFakeStation()
	{
		GameObject newStation = PhotonNetwork.Instantiate ("Station", transform.position, Quaternion.identity, 0);
		newStation.GetComponent<PhotonView> ().RPC ("SetFake", PhotonTargets.All, null);
	}

	public void HackStation()
	{
		if(ActiveStation != null)
		{
			ActiveStation.GetComponent<PhotonView>().RPC("GetHacked", PhotonTargets.MasterClient, null);
		}
		else
		{
			Ability1Timer = 0;
		}
	}

    public void Move(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }

    public void Awaking()
	{
		Ability1ButtonText = GameObject.FindGameObjectWithTag ("Ability 1 Label").GetComponent<Text>();
		Ability2ButtonText = GameObject.FindGameObjectWithTag ("Ability 2 Label").GetComponent<Text>();
	}

	void Start () 
	{
		if (PV.isMine) 
		{
			Ability1Timer = 0;
			Ability2Timer = 0;
			Ability1DurationTimer = Ability1Duration;
			Ability2DurationTimer = Ability2Duration;
			speed = 15;
			BackGround = GameObject.Find ("BackGround").GetComponent<Image> ();
			GameObject.Find ("Interact Button").SetActive (false);
            GameObject.Find ("Activate Button").SetActive(false);
            GameStarted = false;
			Ability1Slider = GameObject.Find ("Ability 1 Slider").GetComponent<Slider> ();
			Ability2Slider = GameObject.Find ("Ability 2 Slider").GetComponent<Slider> ();
			Tracker = GameObject.Find ("Tracker").GetComponent<Image> ();
			Tracker.CrossFadeAlpha (0, 1, true);
			TrackerIsOn = false;
			TrackerTimer = 2;
			NetworkManager = GameObject.Find ("NetworkManager");
			Ability1ButtonText = GameObject.FindGameObjectWithTag ("Ability 1 Label").GetComponent<Text>();
			Ability2ButtonText = GameObject.FindGameObjectWithTag ("Ability 2 Label").GetComponent<Text>();
			GameObject.Find ("Progress Bar").SetActive (false);
			GameObject.Find ("Progress Bar2").SetActive (false);
			SetShadowHunter ();
			speedMultiplier = 1;
			ViewAreaMultiplier = 1;
			CooldownMultiplier = 1;
			Filter = 0.75f;

            Ability1 = GameObject.Find("Ability 1 Button").GetComponent<Button>();
            Ability2 = GameObject.Find("Ability 2 Button").GetComponent<Button>();

            Ability1.onClick.AddListener(Ability1Method);
            Ability2.onClick.AddListener(Ability2Method);
        }
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag == "Player" && GameEnded == false) 
		{
			if (GameObject.FindGameObjectWithTag ("Game Manager") != null) 
			{
				GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<PhotonView> ().RPC ("HunterWin", PhotonTargets.All, null);
			}
			else
			{
				GameObject temp;
				do 
				{
					temp = NetworkManager.GetComponent<NetworkManager> ().SpawnPoints.GetComponent<StationSpawnerController> ().GetSpawn ();
					gameObject.transform.position = temp.transform.position;
				} 
				while (temp.transform.position == gameObject.transform.position);
			}
		}
	}

    void Ability1Method()
    {
        if (Ability1Timer <= 0)
        {
            Ability1Timer = Ability1CoolDown * CooldownMultiplier;
            Ability1Active = true;
            switch (Class)
            {
                case "Shadow Hunter":
                    PV.RPC("SetInvisible", PhotonTargets.All, null);
                    break;
                case "Trip Trap Hunter":
                    RadarBeginning();
                    break;
                case "Hacker Hunter":
                    HackStation();
                    break;
                case "Ghost Hunter":
                    BansheeScreamBeginning();
                    break;
                default:
                    break;
            }
        }
    }

    void Ability2Method()
    {
        if(Ability2Timer <= 0)
        {
            Ability2Timer = Ability2CoolDown * CooldownMultiplier;
            Ability2Active = true;
            switch (Class)
            {
                case "Shadow Hunter":
                    Boost();
                    break;
                case "Trip Trap Hunter":
                    TrapPlayerBeginning();
                    break;
                case "Hacker Hunter":
                    SpawnFakeStation();
                    break;
                case "Ghost Hunter":
                    IncreaseViewArea();
                    break;
                default:
                    break;
            }
        }
    }

	void Update () 
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		if (PV.isMine && GameStarted) 
		{
			Vector2 Direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			Direction = Direction / speed * speedMultiplier;
			Vector3 NextPosition = transform.position;
			NextPosition.x = NextPosition.x + Direction.x;
			NextPosition.y = NextPosition.y + Direction.y;
			GetComponent<Rigidbody2D> ().MovePosition (NextPosition);

			if (GameEnded == false) 
			{
				Vector2 Distance = new Vector2 (transform.position.x - Player.transform.position.x, transform.position.y - Player.transform.position.y);
				float temp = Filter - (Distance.magnitude / 40);
				if (temp < 0)
					temp = 0;
				BackGround.color = new Color (0, 0, 0, temp);
			}

			if (TrackerIsOn) 
			{
				TrackerTimer = TrackerTimer - Time.deltaTime;
				if (TrackerTimer < 0) 
				{
					if (QuickTracker == true)
						Tracker.CrossFadeAlpha (0, 1.2f, true);
					else
						Tracker.CrossFadeAlpha (0, 3, true);
					TrackerTimer = 2;
					TrackerIsOn = false;
				}
			}

			if (RadarIsOn) 
			{
				RadarTimer = RadarTimer - Time.deltaTime;
				if (RadarTimer < 0) 
				{
					Vector2 RadarDistance = new Vector2 (transform.position.x - Player.transform.position.x, transform.position.y - Player.transform.position.y);
					RadarTimer = 1f;
					if (RadarDistance.magnitude < 20) 
					{
						Track (new Vector2 (Player.transform.position.x, Player.transform.position.y) , true);
					}
				}
			}

            if (Input.GetKeyUp(KeyCode.Alpha1))
                Ability1Method();

            if (Input.GetKeyUp(KeyCode.Alpha2))
                Ability2Method();

            if (Ability1Timer >= 0) 
			{
				Ability1Timer = Ability1Timer - Time.deltaTime;
				float Ability1temp;
				if(Ability1Timer < 0)
					Ability1temp = 0;
				else
					Ability1temp = Ability1Timer;
				Ability1temp = 1 - Ability1temp / Ability1CoolDown;
				Ability1Slider.value = Ability1temp;
			}

			if (Ability1Active) 
			{
				Ability1DurationTimer = Ability1DurationTimer - Time.deltaTime;
				if (Ability1DurationTimer < 0) 
				{
					switch (Class) 
					{
					case "Shadow Hunter":
						PV.RPC ("SetVisible",PhotonTargets.All,null);
						break;
					case "Trip Trap Hunter":
						RadarEnding();
						break;
					case "Hacker Hunter":
						break;
					case "Ghost Hunter":
						break;
					default:
						break;
					}


					Ability1Active = false;
					Ability1DurationTimer = Ability1Duration;
				}
			}
				
			if (Ability2Timer >= 0) 
			{
				Ability2Timer = Ability2Timer - Time.deltaTime;
				float Ability2temp;
				if(Ability2Timer < 0)
					Ability2temp = 0;
				else
					Ability2temp = Ability2Timer;
				Ability2temp = 1 - Ability2temp / Ability2CoolDown;
				Ability2Slider.value = Ability2temp;
			}

            if (Ability2Active)
            {
                Ability2DurationTimer = Ability2DurationTimer - Time.deltaTime;
                if (Ability2DurationTimer < 0)
                {
                    Ability2Active = false;
                    Ability2DurationTimer = Ability2Duration;
                    switch (Class)
                    {
                        case "Shadow Hunter":
                            SetNormalSpeed();
                            break;
                        case "Trip Trap Hunter":
                            break;
                        case "Hacker Hunter":
                            break;
                        case "Ghost Hunter":
                            SetNormalViewArea();
                            break;
                        default:
                            break;
                    }
                }
            }
		}
	}
}

