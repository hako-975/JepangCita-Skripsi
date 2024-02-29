using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [Header("Buttons")]
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Button closeSettingsButton;

    [SerializeField]
    private Button closeCreditsButton;

    [SerializeField]
    private Button closeQuitButton;

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
        creditsButton.onClick.AddListener(CreditsButton);
        quitButton.onClick.AddListener(QuitButton);
        closeSettingsButton.onClick.AddListener(CloseButton);
        closeCreditsButton.onClick.AddListener(CloseButton);
        closeQuitButton.onClick.AddListener(CloseButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && windowOn)
        {
            CloseButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && windowOn == false)
        {
            QuitButton();
        }
    }

    private void PlayButton()
    {
        soundController.StartButtonSound(gameObject);

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);

        if (PlayerPrefsController.instance.IsHasCharacterSelection())
        {
            PlayerPrefsController.instance.SetNextScene(PlayerPrefsController.instance.GetLastScene());
        }
        else
        {
            PlayerPrefsController.instance.SetNextScene("CharacterSelection");
        }
    }

    private void SettingsButton()
    {
        soundController.PositiveButtonSound(gameObject);
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

    private void CreditsButton()
    {
        soundController.PositiveButtonSound(gameObject);
        StartCoroutine(AnimationCredits());
    }

    private IEnumerator AnimationCredits()
    {
        windowOn = true;

        DisabledAllPanel();
        panel.GetComponent<Animator>().SetTrigger("Show");
        creditsPanel.SetActive(true);
        yield return null;
    }

    private void QuitButton()
    {
        soundController.PositiveButtonSound(gameObject);
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

    private void CloseButton()
    {
        soundController.NegativeButtonSound(gameObject);

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
        soundController.PositiveButtonSound(gameObject);

        Debug.Log("Keluar Permainan");
        Application.Quit();
    }
}
