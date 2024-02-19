using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RegisterPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private BrowserPanel browserPanel;

    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField fullnameInput;
    [SerializeField]
    private TMP_InputField emailInput;
    [SerializeField]
    private TMP_InputField passwordInput;
    [SerializeField]
    private TMP_InputField confirmPasswordInput;

    [Header("Buttons")]
    [SerializeField]
    private Button registerButton;

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
        
        fullnameInput.text = PlayerPrefsController.instance.GetCharacterName();
        registerButton.onClick.AddListener(RegisterAccount);
    }

    private void RegisterAccount()
    {
        soundController.PositiveButtonSound(gameObject);

        if (PlayerPrefsController.instance.GetEmailJepangCita() != "member@jepangcita.com")
        {
            messageFailed.text = "Anda sudah pernah mendaftar sebelumnya! Dengan email: " + PlayerPrefsController.instance.GetEmailJepangCita();
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel));
            return;
        }
        else if (passwordInput.text != confirmPasswordInput.text)
        {
            messageFailed.text = "Password dan Confirm Password harus sama!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel));
            return;
        } 
        else if (emailInput.text == PlayerPrefsController.instance.GetEmailJepangCita())
        {
            messageFailed.text = "Email sudah terdaftar! Silahkan Login!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel));
            return;
        }
        else
        {
            PlayerPrefsController.instance.SetFullnameJepangCita(fullnameInput.text);
            PlayerPrefsController.instance.SetEmailJepangCita(emailInput.text);
            PlayerPrefsController.instance.SetPasswordJepangCita(passwordInput.text);
            messageSucceed.text = "Akun berhasil didaftarkan! Silahkan Login!";
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
            browserPanel.OnLoginNavButtonClick();
        }
    }
}
