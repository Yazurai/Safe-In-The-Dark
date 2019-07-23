using UnityEngine;
using System.Collections;

public class TutorialTeleporterController : MonoBehaviour
{
    public GameObject LinkedTeleporter;
    public GameObject[] Parts;
    public GameObject particleEffect;
    public int Mode;
    public bool Teleported;

    float Timer;
    bool Active;

    public void DisableTeleporter()
    {
        for (int i = 0; i < 4; i++)
        {
            Color temp = Parts[i].GetComponent<SpriteRenderer>().color;
            temp.a = 0.4f;
            Parts[i].GetComponent<SpriteRenderer>().color = temp;
        }
        particleEffect.SetActive(false);
        Timer = 10;
        Active = false;
    }

    public void EnableTeleporter()
    {
        for (int i = 0; i < 4; i++)
        {
            Color temp = Parts[i].GetComponent<SpriteRenderer>().color;
            temp.a = 1;
            Parts[i].GetComponent<SpriteRenderer>().color = temp;
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
                    LinkedTeleporter.GetComponent<TutorialTeleporterController>().Teleported = true;
                    LinkedTeleporter.GetComponent<TutorialTeleporterController>().DisableTeleporter();
                    Vector3 newPosition = LinkedTeleporter.transform.position;
                    newPosition.z = -1;
                    coll.gameObject.transform.position = newPosition;
                    DisableTeleporter();
                }
            }
            if (Mode == 2)
            {
                if (coll.tag == "Player")
                {
                    LinkedTeleporter.GetComponent<TutorialTeleporterController>().Teleported = true;
                    LinkedTeleporter.GetComponent<TutorialTeleporterController>().DisableTeleporter();
                    Vector3 newPosition = LinkedTeleporter.transform.position;
                    newPosition.z = -1;
                    coll.gameObject.transform.position = newPosition;
                    DisableTeleporter();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
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
                EnableTeleporter();
            }
        }
    }
}
