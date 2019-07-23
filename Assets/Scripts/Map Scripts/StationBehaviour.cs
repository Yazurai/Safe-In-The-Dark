using UnityEngine;
using Photon;
using System.Collections;

public class StationBehaviour : Photon.MonoBehaviour
{
    public GameObject Player;
    public GameObject Hunter;
    public GameObject Track1;
    public GameObject Track2;
    public SpriteRenderer Glow;
    public SpriteRenderer sprite;
    public SpriteRenderer Track1Sprite;
    public SpriteRenderer Track2Sprite;
    public bool IsFake;
    public bool IsShown;
    public bool TotallyShown;

    public float Speed;

    [PunRPC]
    public void SetFake()
    {
        IsFake = true;
    }

    [PunRPC]
    public void GetHacked()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void DestroyObjectHunterVer()
    {
        Hunter.GetComponent<HunterController>().Track(new Vector2(transform.position.x, transform.position.y), false);
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void DestroyObjectHunterVerStealth()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    void Start()
    {
        Hunter = GameObject.FindGameObjectWithTag("Hunter");
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("UpdateBehaviour");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().Station = gameObject;
        }
        if (coll.tag == "Hunter")
        {
            coll.GetComponent<HunterController>().ActiveStation = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().Station = null;
        }
        if (coll.tag == "Hunter")
        {
            coll.GetComponent<HunterController>().ActiveStation = null;
        }
    }

    private void SetColor(float a)
    {
        sprite.color = new Color(1, 1, 1, a);
        Track1Sprite.color = new Color(1, 1, 1, a);
        Track2Sprite.color = new Color(1, 1, 1, a);
    }

    IEnumerator UpdateBehaviour()
    {
        while (true)
        {
            if (IsShown == true)
            {
                if (TotallyShown == false)
                {
                    float alpha = 1 - Vector2.Distance(Hunter.transform.position, transform.position) / 50;
                    if (alpha < 0)
                        alpha = 0;
                    SetColor(alpha);
                }
                else
                {
                    SetColor(1);
                }
            }
            else
            {
                SetColor(0);
            }
            Track1.transform.Rotate(new Vector3(0, 0, Speed * Time.deltaTime * 90));
            Track2.transform.Rotate(new Vector3(0, 0, -Speed * Time.deltaTime * 90));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
