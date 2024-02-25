using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ChangePassword : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private BrowserPanel browserPanel;

    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField passwordInput;
    [SerializeField]
    private TMP_InputField confirmPasswordInput;

    [Header("Buttons")]
    [SerializeField]
    private Button changePasswordButton;

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

        changePasswordButton.onClick.AddListener(ChangePasswordJepangCita);
    }

    private void ChangePasswordJepangCita()
    {
        soundController.PositiveButtonSound(gameObject);

        if (passwordInput.text != confirmPasswordInput.text)
        {
            messageFailed.text = "Password dan Confirm Password harus sama!";

            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel));
            return;
        }
        else
        {
            PlayerPrefsController.instance.SetPasswordJepangCita(passwordInput.text);
            messageSucceed.text = "Password berhasil di reset!";
            
            PlayerPrefsController.instance.SetCredentialJepangCita(0);
            
            StartCoroutine(ShowAndHidePanelCoroutine(succeedPanel));
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
            browserPanel.OnLoginNavButtonClick();
        }
    }
}
