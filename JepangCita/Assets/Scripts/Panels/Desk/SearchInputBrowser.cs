using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SearchInputBrowser : MonoBehaviour
{
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
    private GameObject notFoundPanel;

    [Header("Buttons")]
    [SerializeField]
    private Button goBackwardButton;

    [SerializeField]
    private Button goForwardButton;

    [Header("Navbars")]
    [SerializeField]
    private Button logoButton;

    [SerializeField]
    private Button registerButton;

    [SerializeField]
    private Button loginButton;

    private GameObject currentPanel;
    private GameObject nextPanel;

    private readonly Stack<GameObject> panelHistory = new Stack<GameObject>();
    private readonly Stack<GameObject> panelFuture = new Stack<GameObject>();

    void Start()
    {
        goBackwardButton.interactable = false;
        goForwardButton.interactable = false;

        currentPanel = mainPanel;
        panelHistory.Push(currentPanel);

        goBackwardButton.onClick.AddListener(Backward);
        goForwardButton.onClick.AddListener(Forward);

        logoButton.onClick.AddListener(delegate { OnInputEndEdit("https://jepangcita.com"); });
        registerButton.onClick.AddListener(delegate { OnInputEndEdit("https://jepangcita.com/register"); });
        loginButton.onClick.AddListener(delegate { OnInputEndEdit("https://jepangcita.com/login"); });

        urlInput.onEndEdit.AddListener(OnInputEndEdit);
        searchInput.onEndEdit.AddListener(OnInputEndEdit);
    }

    private void OnInputEndEdit(string input)
    {
        GameObject panelToShow;

        if (IsJepangCitaInput(input))
        {
            urlInput.text = "https://jepangcita.com/";
            panelToShow = jepangCitaPanel;
        }
        else if (IsJepangCitaRegisterInput(input))
        {
            urlInput.text = "https://jepangcita.com/register";
            panelToShow = jepangCitaRegisterPanel;
        }
        else if (IsJepangCitaLoginInput(input))
        {
            urlInput.text = "https://jepangcita.com/login";
            panelToShow = jepangCitaLoginPanel;
        }
        else if (IsTemukanInput(input))
        {
            urlInput.text = "https://temukan.com/";
            panelToShow = mainPanel;
        }
        else
        {
            panelToShow = notFoundPanel;
        }

        nextPanel = panelToShow;
        UpdatePanel();
    }

    private bool IsJepangCitaInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        if (lowercaseInput.Contains("httpsjepangcitacomlogin") || lowercaseInput.Contains("jepangcitacomlogin"))
        {
            return false;
        }
        else if (lowercaseInput.Contains("httpsjepangcitacomregister") || lowercaseInput.Contains("jepangcitacomregister"))
        {
            return false;
        }
        return lowercaseInput.Contains("jepangcita") || lowercaseInput.Contains("jepang cita") || lowercaseInput.Contains("jepangcitacom") || lowercaseInput.Contains("httpsjepangcitacom");
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
        else if (currentPanel == jepangCitaLoginPanel)
        {
            urlInput.text = "https://jepangcita.com/login";
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
}
