using UnityEngine;
using System.Collections;

public class GateAfterController : MonoBehaviour
{
    public MessageManager PrevMsgMan;
    public MessageManager CurrMsgMan; 

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            PrevMsgMan.enabled = false;
            CurrMsgMan.enabled = true;
            CurrMsgMan.StartRoom();
            gameObject.SetActive(false);
        }
    }
}
