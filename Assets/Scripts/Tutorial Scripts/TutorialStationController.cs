using UnityEngine;
using System.Collections;

public class TutorialStationController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Track1;
    public GameObject Track2;
    public SpriteRenderer Glow;

    public Room1Manager Room1Man;

    public float Speed;

    public void GetHacked()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Track1.SetActive(false);
        Track2.SetActive(false);
        Glow.gameObject.SetActive(false);
        Destroy(gameObject, 2);
        Room1Man.StationActivated();
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Speed = 1;
        Glow.color = new Color(1, 1, 1, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<TutorialPlayerController>().Station = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<TutorialPlayerController>().Station = null;
        }
    }

    //TODO: make it an animation
    void Update()
    {
        Track1.transform.Rotate(new Vector3(0, 0, Speed * Time.deltaTime * 90));
        Track2.transform.Rotate(new Vector3(0, 0, -Speed * Time.deltaTime * 90));
    }
}
