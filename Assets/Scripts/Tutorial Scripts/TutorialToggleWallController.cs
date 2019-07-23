using UnityEngine;
using System.Collections;

public class TutorialToggleWallController : MonoBehaviour
{
    public Room8Controller Room8Ctrl;
    float Timer;
    bool Active;

    public void Activate()
    {
        if (Active == false)
        {
            Timer = 3.5f;
            Active = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Room8Ctrl.ActivateOne();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<TutorialPlayerController>().ToggleWall = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<TutorialPlayerController>().ToggleWall = null;
        }
    }

    void Update()
    {
        if (Active == true)
        {
            Timer = Timer - Time.deltaTime;
            if (Timer < 0)
            {
                Active = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
