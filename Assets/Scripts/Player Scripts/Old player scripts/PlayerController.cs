using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon;
using System.Collections;

public class PlayerController : Photon.PunBehaviour
{
    public PlayerMovementController MovCtrl;
	public PhotonView PV;
	public GameObject Hunter;
	public bool GameStarted;
	public Image BackGround;
	public Image ProgressBar;
	public Image ProgressBar2;
	public bool Ready;
	public Sprite[] NinjaSkins;
	public Sprite[] Professor;

	public Material NinjaTrail;
	public Material ProfessorTrail;

	public CircleCollider2D NinjaCollider;
	public PolygonCollider2D ProfessorCollider;

	public Text Ability1ButtonText;
	public Text Ability2ButtonText;

    public Button Ability1;
    public Button Ability2;

	public string Class;

	public bool GameEnded;

	bool isStealth;

	Slider Ability1Slider;
	Slider Ability2Slider;

	public GameObject ToggleWall;
	public GameObject Station;
	public float filter;
	public float CooldownMultiplier;

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

	float Timer;
	float Timer2;
	public bool InteractButtonIsPushed;
	public bool ActivateButtonIsPushed;

	public void SetNinja()
	{
		Class = "Ninja";
		Ability1Timer = 0;
		Ability1CoolDown = 45;
		Ability2Timer = 0;
		Ability2CoolDown = 30;
		Ability1DurationTimer = 5;
		Ability1Duration = 5;
		Ability1DurationTimer = 5;
		Ability2Duration = 5;
		Ability1ButtonText.text = "Vanish";
		Ability2ButtonText.text = "Boost";
		NinjaCollider.enabled = true;
		ProfessorCollider.enabled = false;
	}

	public void SetProfessor()
	{
		Class = "Professor";
		Ability1Timer = 0;
		Ability1CoolDown = 30;
		Ability2Timer = 0;
		Ability2CoolDown = 30;
		Ability1DurationTimer = 5;
		Ability1Duration = 5;
		Ability1DurationTimer = 5;
		Ability2Duration = 5;
		Ability1ButtonText.text = "Stealth";
		Ability2ButtonText.text = "Decoy";
		ProfessorCollider.enabled = true;
		NinjaCollider.enabled = false;
	}

	[PunRPC]
	public void ReadyUp()
	{
		Ready = true;
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

	[PunRPC]
	public void SetSkin(int ClassIndex, int SkinIndex)
	{
		{
			switch (ClassIndex) 
			{
			case 0:
				if(PhotonNetwork.isMasterClient == true)
					GameObject.Find("NetworkManager").GetComponent<NetworkManager> ().LobbyPanel.GetComponent <LobbyPanelController> ().Player.sprite = NinjaSkins [SkinIndex];
				gameObject.GetComponent<SpriteRenderer> ().sprite = NinjaSkins [SkinIndex];
				break;
			case 1:
				if(PhotonNetwork.isMasterClient == true)
					GameObject.Find("NetworkManager").GetComponent<NetworkManager> ().LobbyPanel.GetComponent <LobbyPanelController> ().Player.sprite = Professor [SkinIndex];
				gameObject.GetComponent<SpriteRenderer> ().sprite = Professor [SkinIndex];
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
                gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = NinjaTrail;
                break;
            case 1:
                gameObject.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = ProfessorTrail;
                break;
            default:
                break;
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
			Timer = 10;
			Timer2 = 1;
			Ability1Timer = 0;
			Ability2Timer = 0;
			Ability1DurationTimer = Ability1Duration;
			Ability2DurationTimer = Ability2Duration;
			BackGround = GameObject.Find ("BackGround").GetComponent<Image> ();
			ProgressBar = GameObject.Find ("Progress Bar").GetComponent<Image> ();
			ProgressBar.color = new Color (0, 1, 0, 0);
			ProgressBar2 = GameObject.Find ("Progress Bar2").GetComponent<Image> ();
			ProgressBar2.color = new Color (1, 0, 0, 0);
			Ability1Slider = GameObject.Find ("Ability 1 Slider").GetComponent<Slider> ();
			Ability2Slider = GameObject.Find ("Ability 2 Slider").GetComponent<Slider> ();
			Ability1ButtonText = GameObject.FindGameObjectWithTag ("Ability 1 Label").GetComponent<Text>();
			Ability2ButtonText = GameObject.FindGameObjectWithTag ("Ability 2 Label").GetComponent<Text>();
			GameObject.Find ("Tracker").SetActive (false);
			filter = 40;
			CooldownMultiplier = 1;

            Ability1 = GameObject.Find("Ability 1 Button").GetComponent<Button>();
            Ability2 = GameObject.Find("Ability 2 Button").GetComponent<Button>();
        }
	}

    void InteractPressDown()
    {
        InteractButtonIsPushed = true;
        ProgressBar.color = new Color(0, 1, 0, 0.8f);
        MovCtrl.CanMove = false;
    }

    void InteractPressUp()
    {
        InteractButtonIsPushed = false;
        MovCtrl.CanMove = true;
        Timer = 10;
        ProgressBar.color = new Color(0, 1, 0, 0);
        ProgressBar.gameObject.GetComponent<RectTransform>().localScale = new Vector3(10, 1, 1);
        if (Station != null)
        {
            Station.GetComponent<StationBehaviour>().Speed = 1;
            Station.GetComponent<StationBehaviour>().Glow.color = new Color(1, 1, 1, 0);
        }
    }

    void ActivatePressDown()
    {
        ActivateButtonIsPushed = true;
        ProgressBar2.color = new Color(1, 0, 0, 0.8f);
        MovCtrl.CanMove = false;
    }

    void ActivatePressUp()
    {
        ActivateButtonIsPushed = false;
        MovCtrl.CanMove = true;
        Timer2 = 1;
        ProgressBar2.color = new Color(1, 0, 0, 0);
        ProgressBar2.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    void Ability1Method()
    {
        if(Ability1Timer < 0)
        {
            Ability1Timer = Ability1CoolDown * CooldownMultiplier;
            Ability1Active = true;
            switch (Class)
            {
                case "Ninja":
                    PV.RPC("SetInvisible", PhotonTargets.All, null);
                    break;
                case "Professor":
                    isStealth = true;
                    break;
                default:
                    break;
            }
        }
    }

    void Ability2Method()
    {
        if(Ability2Timer < 0)
        {
            Ability2Timer = Ability2CoolDown * CooldownMultiplier;
            Ability2Active = true;
            switch (Class)
            {
                case "Ninja":
                    MovCtrl.SetSpeed(11);
                    break;
                case "Professor":
                    PhotonNetwork.Instantiate("Player Decoy", transform.position, Quaternion.identity, 0);
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
			if(!GameEnded)
			{
				Vector2 Distance = new Vector2 (transform.position.x - Hunter.transform.position.x, transform.position.y - Hunter.transform.position.y);
				float temp = (Distance.magnitude / filter);
				if (temp > 0.75f)
					temp = 0.75f;
				BackGround.color = new Color (0, 0, 0, temp);
			}

            if (Input.GetKeyDown(KeyCode.Q))
                InteractPressDown();

            if (Input.GetKeyUp(KeyCode.Q))
                InteractPressUp();

            if (Input.GetKeyDown(KeyCode.E))
                ActivatePressDown();

            if (Input.GetKeyUp(KeyCode.E))
                ActivatePressUp();

            if (Input.GetKeyUp(KeyCode.Alpha1))
                Ability1Method();

            if (Input.GetKeyUp(KeyCode.Alpha2))
                Ability2Method();

            if (InteractButtonIsPushed) 
			{
				Timer = Timer - Time.deltaTime;
                ProgressBar.gameObject.GetComponent<RectTransform> ().localScale = new Vector3(Timer,1,1);
				if (Station != null) 
				{
					Station.GetComponent<StationBehaviour> ().Speed = Station.GetComponent<StationBehaviour> ().Speed + Time.deltaTime;
					Station.GetComponent<StationBehaviour> ().Glow.color = new Color (1, 1, 1, (10 - Timer) / 10);
				}
				if (Timer < 0) 
				{
					if (Station != null) 
					{
						PhotonView tempPV = Station.GetComponent<PhotonView> ();
						if(isStealth == false)
							tempPV.RPC ("DestroyObjectHunterVer", PhotonTargets.MasterClient, null);
						else
							tempPV.RPC ("DestroyObjectHunterVerStealth", PhotonTargets.MasterClient, null);
						Timer = 10;
						if (Station.GetComponent<StationBehaviour> ().IsFake == false) 
						{
							PhotonView GameManagerPUNView = GameObject.FindGameObjectWithTag ("Game Manager").GetComponent<PhotonView> ();
							GameManagerPUNView.RPC ("DestroyStation", PhotonTargets.All, null);
						}
						InteractButtonIsPushed = false;
						ProgressBar.color = new Color (0, 1, 0, 0);
						ProgressBar.gameObject.GetComponent<RectTransform> ().localScale = new Vector3(2,1,1);
					} 
					else 
					{
						Timer = 10;
						InteractButtonIsPushed = false;
						ProgressBar.color = new Color (0, 1, 0, 0);
						ProgressBar.gameObject.GetComponent<RectTransform> ().localScale = new Vector3(2,1,1);
					}
				}
			}

			if (ActivateButtonIsPushed) 
			{
				Timer2 = Timer2 - Time.deltaTime;
				ProgressBar2.color = new Color (1, 0, 0, 0.8f);
				ProgressBar2.gameObject.GetComponent<RectTransform> ().localScale = new Vector3(Timer2,1,1);
				if (Timer2 < 0) 
				{
					if (ToggleWall != null) 
					{
						PhotonView tempPV = ToggleWall.GetComponent<PhotonView> ();
						tempPV.RPC ("Activate", PhotonTargets.All, null);
						Timer2 = 1;
					} 
					else 
					{
						Timer2 = 1;
						ActivateButtonIsPushed = false;
						ProgressBar2.color = new Color (0, 1, 0, 0);
						ProgressBar2.gameObject.GetComponent<RectTransform> ().localScale = new Vector3(1,1,1);
					}
				}
			}

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
					Ability1Active = false;
					Ability1DurationTimer = Ability1Duration;
					switch (Class) 
					{
					case "Ninja":
						PV.RPC ("SetVisible",PhotonTargets.All,null);
						break;
					case "Professor":
						isStealth = false;
						break;
					default:
						break;
					}
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
					case "Ninja":
						MovCtrl.SetSpeed (17.5f);
						break;
					case "Professor":
						break;
					default:
						break;
					}
				}
			}
		}
	}
}
