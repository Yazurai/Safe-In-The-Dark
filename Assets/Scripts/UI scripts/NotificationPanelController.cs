using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanelController : MonoBehaviour {


    public void show() {
        gameObject.SetActive(true);
        StartCoroutine("display");
    }

    private IEnumerator display() {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
