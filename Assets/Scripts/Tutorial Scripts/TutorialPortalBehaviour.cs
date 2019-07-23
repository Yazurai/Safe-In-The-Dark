using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialPortalBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    { 
        if(coll.tag == "Player")
        {
            SceneManager.LoadScene("Test Scene");
        }
	}
}
