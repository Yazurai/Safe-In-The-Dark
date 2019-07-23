using System.Collections;
using UnityEngine;

public class PlayerDecoyController : Photon.MonoBehaviour {
    NetworkManager NetworkMan;

    void SetUp(NetworkManager networkMan) {
        NetworkMan = networkMan;
        StartCoroutine("Timer");
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(5);
        if (NetworkMan.IsHunter) {
            GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterController>().Track(new Vector2(transform.position.x, transform.position.y), false);
        }
        Destroy(gameObject);
    }
}
