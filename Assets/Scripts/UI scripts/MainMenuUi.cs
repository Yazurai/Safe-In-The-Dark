using UnityEngine;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour {
    public GameObject RegisteringPanel;
    public GameObject LogInPanel;
    public GameObject NotificationPanel;

    void Start() {
        RegisteringPanel.SetActive(false);
        LogInPanel.SetActive(true);
        NotificationPanel.SetActive(false);
    }

    public void RegisterPanel() {
        LogInPanel.SetActive(false);
        RegisteringPanel.SetActive(true);
    }

    public void BackButton() {
        LogInPanel.SetActive(true);
        RegisteringPanel.SetActive(false);
    }

    public void ToggleNotificationPanel(string message) {
        NotificationPanel.transform.GetChild(0).GetComponent<Text>().text = message;
        NotificationPanel.SetActive(true);
        NotificationPanel.GetComponent<NotificationPanelController>().Active = true;
    }
}
