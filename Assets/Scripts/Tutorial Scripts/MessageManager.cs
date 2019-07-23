using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public struct Message
{
    public bool NoStop;
    public string message;
}

public class MessageManager : MonoBehaviour
{
    public TutorialManager TutMan;
    public int RoomIndex;
    public Message[] Messages;
    public Text Notification;
    public int index;
    public TutorialPlayerController TutPlayerCtrl;
    public Button acceptButton;

    public void StartRoom()
    {
        index = 0;
        Notification.text = Messages[index].message;
        Notification.gameObject.transform.parent.gameObject.SetActive(true);
        TutPlayerCtrl.CanMove = false;
    }

    private void Start()
    {
        acceptButton.onClick.AddListener(AcceptMessage);
    }

    public void Next()
    {
        index ++;
        if (index < Messages.GetLength(0))
        {
            Notification.text = Messages[index].message;
            Notification.gameObject.transform.parent.gameObject.SetActive(true);
            TutPlayerCtrl.CanMove = false;
        }
        else
        {
            TutMan.OpenGate(RoomIndex);
            Notification.gameObject.transform.parent.gameObject.SetActive(false);
            TutPlayerCtrl.CanMove = true;
            gameObject.GetComponent<MessageManager>().enabled = false;
            acceptButton.onClick.RemoveListener(AcceptMessage);
        }
    }

    public void AcceptMessage()
    {
        if (Messages[index].NoStop == true)
        {
            Next();
        }
        else
        {
            Notification.gameObject.transform.parent.gameObject.SetActive(false);
            TutPlayerCtrl.CanMove = true;
        }
    }

    /*void Update()
    {
        if(CrossPlatformInputManager.GetButtonDown("AcceptMessage"))
        {
            if(Messages[index].NoStop == true)
            {
                Next();
            }
            else
            {
                Notification.gameObject.transform.parent.gameObject.SetActive(false);
                TutPlayerCtrl.CanMove = true;
            }
        }
    }*/
}
