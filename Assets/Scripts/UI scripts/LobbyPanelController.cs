using UnityEngine;
using Photon;
using UnityEngine.UI;
using System.Collections;

public class LobbyPanelController : Photon.MonoBehaviour 
{
	public GameObject HunterLoadoutButton;
	public GameObject PlayerLoadoutButton;

	public GameObject ReadyButton;

	public Image Hunter;
	public Image Player;
	public Text HunterName;
	public Text PlayerName;

	public GameObject HunterPanel;
	public GameObject PlayerPanel;

	public NetworkManager NetworkMan;

	[PunRPC]
	void SetHunterText(string name)
	{
		HunterName.text = name;
	}

	[PunRPC]
	void SetPlayerText(string name)
	{
		PlayerName.text = name;
	}

    public void Setup()
    {
        if (NetworkMan.IsHunter == true)
        {
            PlayerLoadoutButton.SetActive(false);
            HunterLoadoutButton.SetActive(true);
        }
        else
        {
            PlayerLoadoutButton.SetActive(true);
            HunterLoadoutButton.SetActive(false);
        }
    }

	public void SetName()
	{
        string Name = GameObject.Find("GameSparksManager").GetComponent<GameSparksManager>().playerData.Name;
        string RPCName = PhotonNetwork.isMasterClient ? "SetHunterText" : "SetPlayerText";
        photonView.RPC(RPCName, PhotonTargets.All, Name);
    }

    public void PressedLoadoutButton(bool isHunter)
    {
        
        PlayerPanel.SetActive(false);
        HunterPanel.SetActive(true);
        if (isHunter)
        {
            HunterPanelController hunterPanelCtrl = HunterPanel.GetComponent<HunterPanelController>();
            hunterPanelCtrl.UpdateSparksManager();
            hunterPanelCtrl.setup();
            hunterPanelCtrl.LoadPanel();
            PlayerPanel.SetActive(false);
            HunterPanel.SetActive(true);
        }
        else
        {
            HunterPanelController playerPanelCtrl = PlayerPanel.GetComponent<HunterPanelController>();
            playerPanelCtrl.UpdateSparksManager();
            playerPanelCtrl.setup();
            playerPanelCtrl.LoadPanel();
            PlayerPanel.SetActive(true);
            HunterPanel.SetActive(false);
        }
        gameObject.SetActive(false);
    }

	public void UpdateHunterSkins(Sprite skin)
	{
		Hunter.sprite = skin;
	}

	public void UpdatePlayerSkins(Sprite skin)
	{
		Player.sprite = skin;
	}

	public void ReadyUp()
	{
        if (NetworkMan.IsHunter)
		{
			GameObject.FindGameObjectWithTag ("Hunter").GetComponent<PhotonView> ().RPC ("ReadyUp", PhotonTargets.All, null);
        }
		else
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PhotonView> ().RPC ("ReadyUp", PhotonTargets.All, null);
		}
        NetworkMan.gameObject.GetComponent<PhotonView>().RPC("SetReady", PhotonTargets.All, true);
        ReadyButton.SetActive(false);
    }
}
