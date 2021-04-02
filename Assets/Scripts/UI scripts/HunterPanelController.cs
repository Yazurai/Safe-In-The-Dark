using UnityEngine;
using UnityEngine.UI;
using Photon;
using System.Collections;

[System.Serializable]
public struct Skin
{
	public int index;
	public bool Unlocked;
	public Sprite SkinSprite;
}

[System.Serializable]
public struct trail
{
	public int index;
	public bool Unlocked;
	public Color TrailColor;
}

[System.Serializable]
public struct HunterPanel
{
	public string Title;
	public int index;
	public bool Unlocked;
	public string Intro;
	public Skin[] Skins;
	public trail [] TrailColors;
	public Sprite Ability1Icon;
	public string Ability1Title;
	public string Ability1Description;
	public Sprite Ability2Icon;
	public string Ability2Title;
	public string Ability2Description;
	public Sprite TrailIcon;
}

public class HunterPanelController : Photon.MonoBehaviour 
{
	GameSparksManager GameSparksManager;

	public Image ClassDisabled;
	public Image SkinDisabled;
	public Image TrailDisabled;

	public GameObject AcceptButton;

	public Text Title;
	public Text Intro;
	public Image Skin;
	public Image Trail;
	public Image Ability1Icon;
	public Text Ability1Title;
	public Text Ability1Description;
	public Image Ability2Icon;
	public Text Ability2Title;
	public Text Ability2Description;
	public Image TrailIcon;

	bool CanClick;

    public TransitionController TransCtrl;

    HunterController hunterCtrl;
    PlayerController playerCtrl;

    public NetworkManager networkMan;
	public LobbyPanelController lobbyPanCtrl;

	public int ActivePanel;
	public int ActiveSkin;
	public int ActiveTrail;

	public HunterPanel [] Hunters;

	float PanelChangeTimer;
	bool PanelIsChanging;

    void Awake()
    {
        GameSparksManager = GameObject.Find("GameSparksManager").GetComponent<GameSparksManager>();
        UpdateSparksManager();
    }

    void Start()
    {
        LoadPanel();
        CanClick = true;
    }

    public void setup()
    {
        hunterCtrl = GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterController>();
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //Loads in the data and updates the panel UI elements accordingly
    public void LoadPanel()
	{
		if (Hunters [ActivePanel].Unlocked == false)
			ClassDisabled.gameObject.SetActive (true);
		else
			ClassDisabled.gameObject.SetActive (false);
		Title.text = Hunters [ActivePanel].Title;
		Intro.text = Hunters [ActivePanel].Intro;
		LoadSkin ();
		LoadTrail ();
		Ability1Icon.sprite = Hunters [ActivePanel].Ability1Icon;
		Ability1Title.text = Hunters [ActivePanel].Ability1Title;
		Ability1Description.text = Hunters [ActivePanel].Ability1Description;
		Ability2Icon.sprite = Hunters [ActivePanel].Ability2Icon;
		Ability2Title.text = Hunters [ActivePanel].Ability2Title;
		Ability2Description.text = Hunters [ActivePanel].Ability2Description;
		TrailIcon.sprite = Hunters [ActivePanel].TrailIcon;
	}

	private void LoadSkin()
	{
		if (Hunters [ActivePanel].Skins [ActiveSkin].Unlocked == false) 
		{
			SkinDisabled.gameObject.SetActive (true);
			AcceptButton.SetActive (false);
		} 
		else 
		{
			SkinDisabled.gameObject.SetActive (false);
			AcceptButton.SetActive (true);
		}
		Skin.sprite = Hunters [ActivePanel].Skins [ActiveSkin].SkinSprite;
	}
		
	private void LoadTrail()
	{
		if (Hunters [ActivePanel].TrailColors [ActiveTrail].Unlocked == false)
		{
			TrailDisabled.gameObject.SetActive (true);
			AcceptButton.SetActive (false);
		}
		else 
		{
			TrailDisabled.gameObject.SetActive (false);
			AcceptButton.SetActive (true);
		}
		Trail.color = Hunters [ActivePanel].TrailColors [ActiveTrail].TrailColor;
	}

    public void ChangeSkin(bool isNext)
    {
        int SkinCount = Hunters[ActivePanel].Skins.GetLength(0);
        ActiveSkin = isNext ? ActiveSkin + 1 : ActiveSkin - 1;
        ActiveSkin = mod(ActiveSkin, SkinCount);
        LoadSkin();
    }

    public void ChangeTrail(bool isNext)
    {
        int TrailCount = Hunters[ActivePanel].TrailColors.GetLength(0);
        ActiveTrail = isNext ? ActiveTrail + 1 : ActiveTrail - 1;
        ActiveTrail = mod(ActiveTrail, TrailCount);
        LoadTrail();
    }

    public void ChangeClass(bool isNext)
    {
        if (CanClick)
        {
            CanClick = false;
            ActivePanel = isNext ? ActivePanel + 1 : ActivePanel - 1;
            ActivePanel = mod(ActivePanel, Hunters.GetLength(0));
            
            ActiveSkin = 0;
            ActiveTrail = 0;

            TransCtrl.Transition();
            StartCoroutine("Switch");
            StartCoroutine("Transition");
        }
    }

    //The moment you ready up
	public void Accept()
	{
		if (CanClick)
		{
            string playerType = PhotonNetwork.isMasterClient ? "Hunter" : "Player";
            GameObject.FindGameObjectWithTag(playerType).GetComponent<PhotonView>().RPC("SetSkin", PhotonTargets.All, ActivePanel, ActiveSkin);
            Quaternion colour = new Quaternion(Hunters[ActivePanel].TrailColors[ActiveTrail].TrailColor.r, Hunters[ActivePanel].TrailColors[ActiveTrail].TrailColor.g, Hunters[ActivePanel].TrailColors[ActiveTrail].TrailColor.b, Hunters[ActivePanel].TrailColors[ActiveTrail].TrailColor.a);
            GameObject.FindGameObjectWithTag(playerType).GetComponent<PhotonView>().RPC("SetTrail", PhotonTargets.All, colour, ActivePanel);
            if (PhotonNetwork.isMasterClient) 
			{
                switch (ActivePanel) 
				{
				case 0:
					hunterCtrl.SetShadowHunter ();
					break;
				case 1:
					hunterCtrl.SetTripTrapHunter ();
					break;
				case 2:
					hunterCtrl.SetHackerHunter ();
					break;
				case 3:
					hunterCtrl.SetGhostHunter ();
					break;
				default:
					break;
				}
			} 
			else 
			{
				switch (ActivePanel) 
				{
				case 0:
					playerCtrl.SetNinja ();
					break;
				case 1:
                    playerCtrl.SetProfessor ();
					break;
				default:
					break;
				}
			}
            lobbyPanCtrl.UpdateHunterSkins(Hunters[ActivePanel].Skins[ActiveSkin].SkinSprite);
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().AcceptBeginning();
        }
	}

	public void UpdateSparksManager ()
	{
		for (int i = 0; i < Hunters.GetLength(0); i++) 
		{
            Hunters[i].Unlocked = GameSparksManager.playerData.CharUnlocks[Hunters[i].index] == false ? false : true;

            for (int a = 0; a < Hunters[i].Skins.GetLength(0); a++)
            {
                Hunters[i].Skins[a].Unlocked = GameSparksManager.playerData.SkinUnlocks[Hunters[i].Skins[a].index] == false ? false : true;
            }

            for (int a = 0; a < Hunters[i].TrailColors.GetLength(0); a++) 
			{
                Hunters[i].TrailColors[a].Unlocked = GameSparksManager.playerData.TrailUnlocks[Hunters[i].TrailColors[a].index] == false ? false : true;
			}
		}
	}

    IEnumerator Switch() {
        yield return new WaitForSeconds(1);
        LoadPanel();
    }

    IEnumerator Transition() {
        yield return new WaitForSeconds(2);
        CanClick = true;
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
