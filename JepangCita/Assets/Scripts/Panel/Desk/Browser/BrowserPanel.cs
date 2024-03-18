using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class BrowserPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField urlInput;

    [SerializeField]
    private TMP_InputField searchInput;

    [Header("Panels")]
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject jepangCitaPanel;

    [SerializeField]
    private GameObject jepangCitaRegisterPanel;

    [SerializeField]
    private GameObject jepangCitaLoginPanel;

    [SerializeField]
    private GameObject jepangCitaResetPasswordPanel;

    [SerializeField]
    private GameObject jepangCitaChangePasswordPanel;

    // Dashboard
    [SerializeField]
    private GameObject jepangCitaDashboardPanel;
    
    [SerializeField]
    private GameObject jepangCitaJadwalPanel;

    [SerializeField]
    private GameObject jepangCitaMateriPanel;


    [SerializeField]
    private GameObject notFoundPanel;


    [Header("Buttons")]
    [SerializeField]
    private Button goBackwardButton;

    [SerializeField]
    private Button goForwardButton;

    private GameObject currentPanel;
    private GameObject nextPanel;

    private readonly Stack<GameObject> panelHistory = new Stack<GameObject>();
    private readonly Stack<GameObject> panelFuture = new Stack<GameObject>();

    private MateriPanel materiPanel;

    void Start()
    {
        materiPanel = jepangCitaMateriPanel.GetComponent<MateriPanel>();

        goBackwardButton.interactable = false;
        goForwardButton.interactable = false;

        currentPanel = mainPanel;
        panelHistory.Push(currentPanel);

        goBackwardButton.onClick.AddListener(Backward);
        goForwardButton.onClick.AddListener(Forward);

        urlInput.onEndEdit.AddListener(OnInputEndEdit);
        searchInput.onEndEdit.AddListener(OnInputEndEdit);
    }

    private void OnInputEndEdit(string input)
    {
        soundController.PositiveButtonSound(gameObject);

        GameObject panelToShow;

        if (materiPanel.previewHurufPanelInstantiate != null)
        {
            Destroy(materiPanel.previewHurufPanelInstantiate);
            materiPanel.closeButton.onClick.RemoveAllListeners();
            materiPanel.closeButton.onClick.AddListener(delegate { materiPanel.BackToMateriPanel(true); });
        }

        if (IsJepangCitaInput(input))
        {
            panelToShow = jepangCitaPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                panelToShow = jepangCitaDashboardPanel;
            }
        }
        else if (IsJepangCitaDashboardInput(input))
        {
            panelToShow = jepangCitaPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                panelToShow = jepangCitaDashboardPanel;
            } 
        }
        else if (IsJepangCitaJadwalInput(input))
        {
            panelToShow = jepangCitaPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                panelToShow = jepangCitaJadwalPanel;
                // misi kedua
                PlayerPrefsController.instance.SetMission(1, soundController);
            }
        }
        else if (IsJepangCitaMateriInput(input))
        {
            panelToShow = jepangCitaPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                // misi kelima
                PlayerPrefsController.instance.SetMission(4, soundController);
                panelToShow = jepangCitaMateriPanel;
                materiPanel.BackToMateriPanel();
            }
        }
        else if (IsJepangCitaRegisterInput(input))
        {
            panelToShow = jepangCitaRegisterPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                panelToShow = jepangCitaDashboardPanel;
            }
        }
        else if (IsJepangCitaLoginInput(input))
        {
            panelToShow = jepangCitaLoginPanel;
            
            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                panelToShow = jepangCitaDashboardPanel;
            }
        }
        else if (IsJepangCitaResetPasswordInput(input))
        {
            panelToShow = jepangCitaResetPasswordPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 1)
            {
                panelToShow = jepangCitaDashboardPanel;
            }
        }
        else if (IsJepangCitaChangePasswordInput(input))
        {
            panelToShow = jepangCitaPanel;

            if (PlayerPrefsController.instance.GetCredentialJepangCita() == 2)
            {
                panelToShow = jepangCitaChangePasswordPanel;
            }
        }
        else if (IsTemukanInput(input))
        {
            panelToShow = mainPanel;
        }
        else
        {
            panelToShow = notFoundPanel;
        }

        if (panelToShow != currentPanel)
        {
            nextPanel = panelToShow;
            UpdatePanel();
        }
    }

    private bool IsJepangCitaInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        if (lowercaseInput.Contains("httpsjepangcitacomdashboard") || lowercaseInput.Contains("jepangcitacomdashboard"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacomjadwal") || lowercaseInput.Contains("jepangcitacomjadwal"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacommateri") || lowercaseInput.Contains("jepangcitacommateri"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacomlogin") || lowercaseInput.Contains("jepangcitacomlogin"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacomresetpassword") || lowercaseInput.Contains("jepangcitacomresetpassword"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacomchangepassword") || lowercaseInput.Contains("jepangcitacomchangepassword"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacomregister") || lowercaseInput.Contains("jepangcitacomregister"))
        {
            return false;
        }
        return lowercaseInput.Contains("jepangcita") || lowercaseInput.Contains("jepang cita") || lowercaseInput.Contains("jepangcitacom") || lowercaseInput.Contains("httpsjepangcitacom");
    }

    private bool IsJepangCitaDashboardInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitadashboard") || lowercaseInput.Contains("jepang cita dashboard") || lowercaseInput.Contains("jepangcitacomdashboard") || lowercaseInput.Contains("httpsjepangcitacomdashboard");
    }

    private bool IsJepangCitaJadwalInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitajadwal") || lowercaseInput.Contains("jepang cita jadwal") || lowercaseInput.Contains("jepangcitacomjadwal") || lowercaseInput.Contains("httpsjepangcitacomjadwal");
    }

    private bool IsJepangCitaMateriInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitamateri") || lowercaseInput.Contains("jepang cita materi") || lowercaseInput.Contains("jepangcitacommateri") || lowercaseInput.Contains("httpsjepangcitacommateri");
    }

    private bool IsJepangCitaRegisterInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitaregister") || lowercaseInput.Contains("jepang cita register") || lowercaseInput.Contains("jepangcitacomregister") || lowercaseInput.Contains("httpsjepangcitacomregister");
    }

    private bool IsJepangCitaLoginInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitalogin") || lowercaseInput.Contains("jepang cita login") || lowercaseInput.Contains("jepangcitacomlogin") || lowercaseInput.Contains("httpsjepangcitacomlogin");
    }

    private bool IsJepangCitaResetPasswordInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitaresetpassword") || lowercaseInput.Contains("jepang cita resetpassword") || lowercaseInput.Contains("jepangcitacomresetpassword") || lowercaseInput.Contains("httpsjepangcitacomresetpassword");
    }

    private bool IsJepangCitaChangePasswordInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcitachangepassword") || lowercaseInput.Contains("jepang cita changepassword") || lowercaseInput.Contains("jepangcitacomchangepassword") || lowercaseInput.Contains("httpsjepangcitacomchangepassword");
    }

    private bool IsTemukanInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("temukan") || lowercaseInput.Contains("temukancom") || lowercaseInput.Contains("httpstemukancom");
    }

    private IEnumerator AnimationOpenWeb(GameObject panel)
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private IEnumerator AnimationCloseWeb(GameObject panel)
    {
        panel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void UpdatePanel()
    {
        goBackwardButton.interactable = panelHistory.Count > 0;

        if (nextPanel != null)
        {
            panelHistory.Push(currentPanel);

            if (currentPanel != null)
            {
                StartCoroutine(AnimationCloseWeb(currentPanel));
            }

            currentPanel = nextPanel;
            StartCoroutine(AnimationOpenWeb(nextPanel));
            goForwardButton.interactable = false;
            panelFuture.Clear(); 
        }
        else
        {
            goForwardButton.interactable = panelFuture.Count > 0;
        }

        UpdateUrlInputText();
    }

    private void Backward()
    {
        soundController.PositiveButtonSound(gameObject);

        if (panelHistory.Count > 1)
        {
            panelFuture.Push(currentPanel);

            nextPanel = panelHistory.Pop();
            StartCoroutine(AnimationCloseWeb(currentPanel));
            StartCoroutine(AnimationOpenWeb(nextPanel));

            currentPanel = nextPanel;
            goForwardButton.interactable = true; 
        }

        goBackwardButton.interactable = panelHistory.Count > 1 || currentPanel != mainPanel;
        UpdateUrlInputText();
    }


    private void Forward()
    {
        soundController.PositiveButtonSound(gameObject);

        if (panelFuture.Count > 0)
        {
            panelHistory.Push(currentPanel);

            nextPanel = panelFuture.Pop();
            StartCoroutine(AnimationCloseWeb(currentPanel));
            StartCoroutine(AnimationOpenWeb(nextPanel));

            currentPanel = nextPanel;
            goBackwardButton.interactable = true;
        }

        goForwardButton.interactable = panelFuture.Count > 0;
        UpdateUrlInputText();
    }

    private void UpdateUrlInputText()
    {
        if (currentPanel == jepangCitaPanel)
        {
            urlInput.text = "https://jepangcita.com/";
        }
        else if (currentPanel == jepangCitaDashboardPanel)
        {
            urlInput.text = "https://jepangcita.com/dashboard";
        }
        else if (currentPanel == jepangCitaJadwalPanel)
        {
            urlInput.text = "https://jepangcita.com/jadwal";
        }
        else if (currentPanel == jepangCitaMateriPanel)
        {
            urlInput.text = "https://jepangcita.com/materi";
        }
        else if (currentPanel == jepangCitaLoginPanel)
        {
            urlInput.text = "https://jepangcita.com/login";
        }
        else if (currentPanel == jepangCitaResetPasswordPanel)
        {
            urlInput.text = "https://jepangcita.com/resetpassword";
        }
        else if (currentPanel == jepangCitaChangePasswordPanel)
        {
            urlInput.text = "https://jepangcita.com/changepassword";
        }
        else if (currentPanel == jepangCitaRegisterPanel)
        {
            urlInput.text = "https://jepangcita.com/register";
        }
        else if (currentPanel == mainPanel)
        {
            urlInput.text = "https://temukan.com/";
        }
    }

    public void OnLogoNavButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com");
    }

    public void OnDashboardNavButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/dashboard");
    }

    public void OnRegisterNavButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/register");
    }

    public void OnLoginNavButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/login");
    }

    public void OnJadwalNavButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/jadwal");
    }

    public void OnMateriNavButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/materi");
    }

    public void OnResetPasswordButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/resetpassword");
    }

    public void OnChangePasswordButtonClick()
    {
        OnInputEndEdit("https://jepangcita.com/changepassword");
    }

    public void OnLogoutNavButtonClick()
    {
        PlayerPrefsController.instance.SetCredentialJepangCita(0);
        OnInputEndEdit("https://jepangcita.com/login");
    }
}
