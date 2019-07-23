using UnityEngine;
using System.Collections;

public class TutorialOverwatchController : MonoBehaviour
{
    public bool Activated;
    public GameObject Player;

	void Update ()
    {
	    if(Activated)
        {
            Vector2 Direction = new Vector2(Player.transform.position.x - gameObject.transform.position.x, Player.transform.position.y - gameObject.transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    if (hit.collider.gameObject.GetComponent<TutorialPlayerController>().Invisible == false)
                    {
                        hit.collider.gameObject.transform.position = new Vector3(153.8f, -20.07f, 0);
                    }
                }
            }
        }
	}
}
