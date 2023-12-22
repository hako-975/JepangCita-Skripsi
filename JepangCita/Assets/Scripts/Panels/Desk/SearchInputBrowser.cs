using UnityEngine;
using TMPro;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SearchInputBrowser : MonoBehaviour
{
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
    private GameObject jepangCitaDashboardPanel;

    [SerializeField]
    private GameObject notFoundPanel;

    [SerializeField]
    private Button goBackwardButton;

    [SerializeField]
    private Button goForwardButton;

    private GameObject currentPanel;
    private GameObject nextPanel;

    void Start()
    {
        currentPanel = mainPanel;

        goBackwardButton.onClick.AddListener(Backward);
        goForwardButton.onClick.AddListener(UpdatePanel);

        urlInput.onEndEdit.AddListener(OnInputEndEdit);
        searchInput.onEndEdit.AddListener(OnInputEndEdit);
    }

    private void OnInputEndEdit(string input)
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            GameObject panelToShow;

            if (IsJepangCitaInput(input))
            {
                urlInput.text = "https://jepangcita.com/";
                panelToShow = jepangCitaPanel;
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

            // logic
            nextPanel = panelToShow;
            UpdatePanel();
        }
    }

    private bool IsJepangCitaInput(string input)
    {
        string lowercaseInput = Regex.Replace(input, "[^a-zA-Z0-9]", "").Trim().ToLower();
        return lowercaseInput.Contains("jepangcita") || lowercaseInput.Contains("jepang cita") || lowercaseInput.Contains("jepangcitacom") || lowercaseInput.Contains("httpsjepangcitacom");
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
        if (nextPanel == jepangCitaPanel)
        {
            urlInput.text = "https://jepangcita.com/";
        }
        else if (nextPanel == mainPanel)
        {
            urlInput.text = "https://temukan.com/";
        }

        StartCoroutine(AnimationCloseWeb(currentPanel));
        StartCoroutine(AnimationOpenWeb(nextPanel));
    }

    private void Backward()
    {
        if (currentPanel == jepangCitaPanel)
        {
            urlInput.text = "https://jepangcita.com/";
        }
        else if (currentPanel == mainPanel)
        {
            urlInput.text = "https://temukan.com/";
        }

        StartCoroutine(AnimationCloseWeb(nextPanel));
        StartCoroutine(AnimationOpenWeb(currentPanel));
    }
}
