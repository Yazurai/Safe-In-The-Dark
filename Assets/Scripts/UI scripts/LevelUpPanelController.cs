using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanelController : MonoBehaviour {
    public Text PlayerLeveledUpText;
    public Text NewLevelText;
    public Text Awards;

    void Start() {
        NewLevelText.color = new Color(1, 1, 1, 0);
        Awards.color = new Color(1, 1, 1, 0);
    }

    //TODO: MAKE THIS INTO AN ANIMATION AND CONTROL THE ANIMATOR FROM THE SCRIPT
    /*void Update () 
	{
		if (IsShown == true) 
		{
			timer = timer - Time.deltaTime;
			if (timer < 0) 
			{
				switch (turn) 
				{
				    case 0:
					    NewLevelText.CrossFadeAlpha (1, 1.5f, true);
                        turn++;
					    timer = 1.5f;
					    break;
				    case 1:
					    Awards.CrossFadeAlpha (1, 1.5f, true);
                        turn++;
					    timer = 3;
					    break;
				    case 2:
                        IsShown = false;
					    gameObject.transform.parent.GetComponent<XPScreenController> ().XpActive = true;
                        gameObject.SetActive(false);
                        break;
				    default:
					    break;
				}
			}
		}
	}*/
}
