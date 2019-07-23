using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public struct Gate
{
    public GameObject YellowLight1;
    public GameObject YellowLight2;
    public GameObject BlueLight1;
    public GameObject BlueLight2;
    public GameObject YellowGate;
}

public class TutorialManager : MonoBehaviour
{
    public Gate[] Gates;
    public Room1Manager Room1;
    public Room5Controller Room5;
    public Room5SecondaryController Room5Sec;
    public Room6Controller Room6;
    public Room6SecondaryController Room6Sec;
    public Room7Controller Room7;
    public Room8Controller Room8;

    public MessageManager Room1MsgMan;

    public GameObject ActivateWall;
    public GameObject Ability2;
    public TutorialPlayerController tutPlayCtrl;

    void Start()
    {
        Room1.enabled = false;
        Room5.enabled = false;
        Room5Sec.enabled = false;
        Room6.enabled = false;
        Room6Sec.enabled = false;
        Room7.enabled = false;
        Room8.enabled = false;
        Room1MsgMan.StartRoom();
    }

    public void OpenGate(int index)
    {
        Gates[index].YellowLight1.SetActive(false);
        Gates[index].YellowLight2.SetActive(false);
        Gates[index].BlueLight1.SetActive(true);
        Gates[index].BlueLight2.SetActive(true);
        Gates[index].YellowGate.SetActive(false);
        switch (index)
        {
            case 2:
                Room1.enabled = true;
                Room1.StartRoom();
                break;
            case 3:
                Room1.enabled = false;
                Room5.enabled = true;
                Room5Sec.enabled = true;
                Room5.StartRoom();
                break;
            case 4:
                Room5.enabled = false;
                Room5Sec.enabled = false;
                Room6.enabled = true;
                Room6Sec.enabled = true;
                Ability2.SetActive(true);
                tutPlayCtrl.canUseAbility1 = true;
                break;
            case 5:
                Room6.enabled = false;
                Room6Sec.enabled = false;
                Room7.enabled = true;
                Room7.StartRoom();
                break;
            case 6:
                Room7.enabled = false;
                Room8.enabled = true;
                ActivateWall.SetActive(true);
                tutPlayCtrl.canActivate = true;
                break;
            default:
                break;
        }
    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("Test Scene");
    }
}
