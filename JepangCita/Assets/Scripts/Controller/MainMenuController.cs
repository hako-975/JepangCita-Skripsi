using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private Button quitButton;

    [Header("Panels")]
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject creditsPanel;

    [SerializeField]
    private GameObject quitPanel;

    private bool windowOn = false;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayButton);
        settingsButton.onClick.AddListener(SettingsButton);
        creditsButton.onClick.AddListener(HintButton);
        quitButton.onClick.AddListener(QuitButton);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && windowOn)
        {
            CloseButton();
        }
    }

    private void PlayButton()
    {
        PlayerPrefsController.instance.SetNextScene("Gameplay");
    }

    private void SettingsButton()
    {
        StartCoroutine(AnimationSettings());
    }

    private IEnumerator AnimationSettings()
    {
        windowOn = true;

        DisabledAllPanel();
        panel.GetComponent<Animator>().SetTrigger("Show");
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private void HintButton()
    {
        StartCoroutine(AnimationHint());
    }

    private IEnumerator AnimationHint()
    {
        windowOn = true;

        DisabledAllPanel();
        panel.GetComponent<Animator>().SetTrigger("Show");
        creditsPanel.SetActive(true);
        yield return null;
    }

    private void QuitButton()
    {
        StartCoroutine(AnimationQuit());
    }

    private IEnumerator AnimationQuit()
    {
        windowOn = true;

        DisabledAllPanel();
        panel.GetComponent<Animator>().SetTrigger("Show");
        quitPanel.SetActive(true);
        yield return null;
    }

    public void CloseButton()
    {
        StartCoroutine(AnimationClose());
    }

    private IEnumerator AnimationClose()
    {
        windowOn = false;

        panel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }
    
    private void DisabledAllPanel()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void QuitButtonYes()
    {
        Debug.Log("Keluar Permainan");
        Application.Quit();
    }
}
