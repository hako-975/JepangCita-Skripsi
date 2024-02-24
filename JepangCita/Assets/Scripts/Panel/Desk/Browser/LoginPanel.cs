using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LoginPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private BrowserPanel browserPanel;

    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField emailInput;
    [SerializeField]
    private TMP_InputField passwordInput;

    [Header("Buttons")]
    [SerializeField]
    private Button loginButton;

    [Header("Panels")]
    [SerializeField]
    private GameObject failedPanel;
    [SerializeField]
    private TextMeshProUGUI messageFailed;
    [SerializeField]
    private GameObject succeedPanel;
    [SerializeField]
    private TextMeshProUGUI messageSucceed;



    // Start is called before the first frame update
    void Start()
    {
        failedPanel.SetActive(false);
        succeedPanel.SetActive(false);

        loginButton.onClick.AddListener(LoginAccount);
    }

    private void LoginAccount()
    {
        soundController.PositiveButtonSound(gameObject);

        if (PlayerPrefsController.instance.GetEmailJepangCita() != emailInput.text)
        {
            messageFailed.text = "Email atau Password salah!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel));
            return;
        }
        else if (PlayerPrefsController.instance.GetPasswordJepangCita() != passwordInput.text)
        {
            messageFailed.text = "Email atau Password salah!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel));
            return;
        }
        else
        {
            // jika misi pertama 0 index pertama
            PlayerPrefsController.instance.SetMission(0, soundController);

            messageSucceed.text = "Berhasil Login!";
            PlayerPrefsController.instance.SetCredentialJepangCita(1);
            StartCoroutine(ShowAndHidePanelCoroutine(succeedPanel));
            return;
        }
    }

    IEnumerator ShowAndHidePanelCoroutine(GameObject panel)
    {
        panel.SetActive(true);

        panel.GetComponent<Animator>().SetTrigger("Show");

        yield return new WaitForSeconds(2f);

        panel.GetComponent<Animator>().SetTrigger("Hide");

        if (panel == succeedPanel)
        {
            browserPanel.OnDashboardNavButtonClick();
        }
    }
}
