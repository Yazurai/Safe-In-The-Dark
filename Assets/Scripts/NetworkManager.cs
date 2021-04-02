using Photon;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : PunBehaviour {
    public GameObject SpawnPoints;
    public GameObject RotatingPlatformSpawnPoint;
    public GameObject HorizontalPlatformSpawnPoint;
    public GameObject ToggleWallSpawnPoint;
    public GameObject TeleporterSpawnPoint;
    public Text Notification;
    public GameObject PlayButton;
    public GameObject AbsoluteBlackScreen;
    public GameObject LobbyPanel;
    public GameObject HunterSelectionMenu;
    public GameObject PlayerSelectionMenu;
    public GameObject XPPanel;
    public GameObject InventoryPanel;
    public GameObject ShopPanel;
    public GameObject StatsPanel;
    public GameObject MenuCamera;
    public GameObject[] Labyrinths;

    public GameObject[] SpawnLocations = new GameObject[35];
    public GameObject[] RotPlatSpawnPoints = new GameObject[15];
    public GameObject[] HorPlatSpawnPoints = new GameObject[10];
    public GameObject[] TogWallSpawnPoints = new GameObject[15];
    public GameObject[] TeleporterSpawnPoints = new GameObject[5];
    public float Timer;
    public bool IsHunter;
    public GameObject Player;
    int ReadyCount;

    public TransitionController TransCtrl;

    private GameManager gm;

    [PunRPC]
    void SetSide(bool isHunter) {
        IsHunter = isHunter;
        if (isHunter) {
            Player = PhotonNetwork.Instantiate("Hunter Prefab", SpawnPoints.GetComponent<StationSpawnerController>().GetSpawn().transform.position, Quaternion.identity, 0);
            Player.transform.GetChild(0).gameObject.SetActive(true);
            Player.GetComponent<HunterController>().Awaking();
            Player.GetComponent<HunterController>().SetShadowHunter();
        } else {
            Player = PhotonNetwork.Instantiate("Player Prefab", SpawnPoints.GetComponent<StationSpawnerController>().GetSpawn().transform.position, Quaternion.identity, 0);
            Player.transform.GetChild(0).gameObject.SetActive(true);
            Player.GetComponent<PlayerController>().Awaking();
            Player.GetComponent<PlayerController>().SetNinja();
        }
        Notification.gameObject.transform.parent.gameObject.SetActive(false);
        LobbyPanel.gameObject.SetActive(true);
        LobbyPanel.GetComponent<LobbyPanelController>().NetworkMan = this;
        LobbyPanel.GetComponent<LobbyPanelController>().Setup();
    }

    [PunRPC]
    void SetLabyrinth(int index) {
        Labyrinths[index].SetActive(true);
    }

    [PunRPC]
    void SetReady(bool isReady) {
        ReadyCount += isReady ? 1 : -1;
        if (ReadyCount == 2) {
            if (PhotonNetwork.isMasterClient) {
                gm = PhotonNetwork.Instantiate("Game Manager", Vector3.zero, Quaternion.identity, 0).GetComponent<GameManager>();

                SpawnLocations = SpawnPoints.GetComponent<StationSpawnerController>().GetSpawns();
                for (int i = 0; i < 35; i++) {
                    PhotonNetwork.Instantiate("Station", SpawnLocations[i].transform.position, Quaternion.identity, 0);
                }
                RotPlatSpawnPoints = RotatingPlatformSpawnPoint.GetComponent<StationSpawnerController>().GetSpawns();
                for (int i = 0; i < 15; i++) {
                    GameObject RotPlatform = PhotonNetwork.Instantiate("Rotating Platform", RotPlatSpawnPoints[i].transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))), 0);
                    if (Random.Range(0, 2) == 0)
                        RotPlatform.GetComponent<PhotonView>().RPC("TurnDirection", PhotonTargets.AllBuffered, true);
                    else
                        RotPlatform.GetComponent<PhotonView>().RPC("TurnDirection", PhotonTargets.AllBuffered, false);
                }
                HorPlatSpawnPoints = HorizontalPlatformSpawnPoint.GetComponent<StationSpawnerController>().GetSpawns();
                for (int i = 0; i < 10; i++) {
                    GameObject HorPlatform = PhotonNetwork.Instantiate("Horizontal Moving Platform", HorPlatSpawnPoints[i].transform.position, Quaternion.identity, 0);
                    if (Random.Range(0, 2) == 0)
                        HorPlatform.GetComponent<PhotonView>().RPC("SetDirection", PhotonTargets.AllBuffered, true);

                    else
                        HorPlatform.GetComponent<PhotonView>().RPC("SetDirection", PhotonTargets.AllBuffered, false);
                }
                TogWallSpawnPoints = ToggleWallSpawnPoint.GetComponent<StationSpawnerController>().GetSpawns();
                for (int i = 0; i < 15; i++) {
                    GameObject ToggleWall = PhotonNetwork.Instantiate("ToggleWall", new Vector3(0, 0, 0), Quaternion.identity, 0);
                    ToggleWall.GetComponent<PhotonView>().RPC("SetTransform", PhotonTargets.All, TogWallSpawnPoints[i].gameObject.transform.position, TogWallSpawnPoints[i].gameObject.transform.rotation.eulerAngles, TogWallSpawnPoints[i].gameObject.transform.localScale);
                }
                TeleporterSpawnPoints = TeleporterSpawnPoint.GetComponent<StationSpawnerController>().GetSpawns();
                for (int i = 0; i < 5; i++) {
                    GameObject Teleporter = PhotonNetwork.Instantiate("Teleporter", TeleporterSpawnPoints[i].transform.GetChild(0).transform.position, Quaternion.identity, 0);
                    GameObject Teleporter2 = PhotonNetwork.Instantiate("Teleporter", TeleporterSpawnPoints[i].transform.GetChild(1).transform.position, Quaternion.identity, 0);
                    int TeleporterRandom = Random.Range(0, 11);
                    if (TeleporterRandom > 0 && TeleporterRandom < 10) {
                        Teleporter.GetComponent<PhotonView>().RPC("SetNeutral", PhotonTargets.All, null);
                        Teleporter2.GetComponent<PhotonView>().RPC("SetNeutral", PhotonTargets.All, null);
                    }
                    if (TeleporterRandom == 0) {
                        Teleporter.GetComponent<PhotonView>().RPC("SetHunterOnly", PhotonTargets.All, null);
                        Teleporter2.GetComponent<PhotonView>().RPC("SetHunterOnly", PhotonTargets.All, null);
                    }
                    if (TeleporterRandom == 10) {
                        Teleporter.GetComponent<PhotonView>().RPC("SetPlayerOnly", PhotonTargets.All, null);
                        Teleporter2.GetComponent<PhotonView>().RPC("SetPlayerOnly", PhotonTargets.All, null);
                    }
                    int ID1 = Teleporter.GetComponent<PhotonView>().viewID;
                    int ID2 = Teleporter2.GetComponent<PhotonView>().viewID;
                    Teleporter.GetComponent<PhotonView>().RPC("SetLinkedTeleporter", PhotonTargets.All, ID2);
                    Teleporter2.GetComponent<PhotonView>().RPC("SetLinkedTeleporter", PhotonTargets.All, ID1);
                }
                int LabyrinthIndex = Random.Range(0, Labyrinths.GetLength(0));
                photonView.RPC("SetLabyrinth", PhotonTargets.All, LabyrinthIndex);
            }

            if (IsHunter) {
                GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterController>().Player = GameObject.FindGameObjectWithTag("Player");
                GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterController>().GameStarted = true;
            } else {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Hunter = GameObject.FindGameObjectWithTag("Hunter");
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameStarted = true;
            }
            DisableAllUI();
            StartCoroutine("Countdown");
        }
    }

    public void AcceptBeginning() {
        TransCtrl.SetHold(true);
        TransCtrl.Transition();
        StartCoroutine("AcceptEnding");
    }

    IEnumerator AcceptEnding() {
        yield return new WaitForSeconds(1);
        HunterSelectionMenu.SetActive(false);
        PlayerSelectionMenu.SetActive(false);
        LobbyPanel.SetActive(true);
        LobbyPanel.GetComponent<LobbyPanelController>().Hunter.sprite = GameObject.FindGameObjectWithTag("Hunter").GetComponent<SpriteRenderer>().sprite;
        LobbyPanel.GetComponent<LobbyPanelController>().Player.sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite;
        TransCtrl.SetHold(false);
    }

    void Start() {
        if (PhotonNetwork.connected == false) {
            PhotonNetwork.ConnectUsingSettings("1");
        }
        PhotonNetwork.JoinLobby();
        Timer = 5;
        IsHunter = false;
        Notification.text = "";
        HunterSelectionMenu.gameObject.SetActive(false);
        LobbyPanel.gameObject.SetActive(false);
        XPPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        ShopPanel.SetActive(false);
        StatsPanel.SetActive(false);
    }

    public void OpenInventory() {
        InventoryPanel.SetActive(true);
        InventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        InventoryPanel.GetComponent<InventoryManager>().LoadSideChoosePanel();
    }

    public void OpenShop() {
        ShopPanel.SetActive(true);
        ShopPanel.GetComponent<ShopManager>().LoadTokens();
    }

    public void OpenStatsPanel() {
        StatsPanel.SetActive(true);
        StatsPanel.GetComponent<PlayerStatPanelController>().UpdateData();
    }

    void DisableAllUI() {
        HunterSelectionMenu.SetActive(false);
        PlayerSelectionMenu.SetActive(false);
        LobbyPanel.SetActive(false);
        PlayButton.SetActive(false);
        InventoryPanel.SetActive(false);
        ShopPanel.SetActive(false);
        XPPanel.SetActive(false);
    }

    public void PlayPushed() {
        PlayButton.SetActive(false);
        PhotonNetwork.JoinRandomRoom();
    }

    IEnumerator Countdown() {
        Notification.transform.parent.gameObject.SetActive(true);
        for (int i = 5; i >= 0; i--) {
            Notification.text = "Starting game in: " + i.ToString() + "s";
            yield return new WaitForSeconds(1);
        }
        Notification.transform.parent.gameObject.SetActive(false);
        DisableAllUI();
        TransCtrl.gameObject.SetActive(false);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
        if (PhotonNetwork.room.PlayerCount == 2) {
            MenuCamera.SetActive(false);
            if (PhotonNetwork.isMasterClient) {
                PhotonNetwork.room.IsOpen = false;
                PhotonNetwork.room.IsVisible = false;
                photonView.RPC("SetSide", PhotonTargets.MasterClient, true);
                photonView.RPC("SetSide", PhotonTargets.Others, false);
                /*if (Random.Range(0f, 1f) > 0.5f) {
                    photonView.RPC("SetSide", PhotonTargets.MasterClient, true);
                    photonView.RPC("SetSide", PhotonTargets.Others, false);
                } else {
                    photonView.RPC("SetSide", PhotonTargets.MasterClient, false);
                    photonView.RPC("SetSide", PhotonTargets.Others, true);
                }*/
            }
        }
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        PlayButton.SetActive(true);
    }

    void OnPhotonRandomJoinFailed() {
        RoomOptions roomOptions = new RoomOptions {
            IsVisible = true,
            MaxPlayers = 2
        };
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        if (!PhotonNetwork.isMasterClient) {
            MenuCamera.SetActive(false);
        }
    }

    public override void OnLeftRoom() {
        base.OnLeftRoom();
        SceneManager.LoadScene("Test Scene");
    }

    public override void OnCreatedRoom() {
        base.OnCreatedRoom();
        Notification.text = "Waiting For A Player To Join";
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        if (gm != null)
        {
            if (!gm.GameEnded)
            {
                if (PhotonNetwork.isMasterClient == true)
                {
                    GameObject.Find("Game Manager").GetComponent<GameManager>().HunterWin();
                }
                else
                {
                    GameObject.Find("Game Manager").GetComponent<GameManager>().PlayerWin();
                }
            }
        }
    }
}
