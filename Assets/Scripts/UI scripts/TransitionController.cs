using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour {
    public Animator anim;

    public void Transition() {
        anim.SetTrigger("Fade");
    }

    public void SetHold(bool val) {
        anim.SetBool("Hold", val);
    }
}
