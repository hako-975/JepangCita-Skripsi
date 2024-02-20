using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [Header("Buttons")]
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button mainMenuButton;

    [SerializeField]
    private Button closePauseButton;

    [SerializeField]
    private Button closeSettingsButton;

    [SerializeField]
    private Button closeMainMenuButton;

    [SerializeField]
    private Button goToMainMenuButton;

    [SerializeField]
    private Button cancelMainMenuButton;

    [Header("Panels")]
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private ActionController actionController;

    private bool windowPause = false;
    private bool windowSettings = false;
    private bool windowMainMenu = false;

    [SerializeField]
    private GameObject playerBoyPrefabs;

    [SerializeField]
    private GameObject playerGirlPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefsController.instance.GetCharacterSelection() == "Girl")
        {
            Instantiate(playerGirlPrefabs);
        }
        else
        {
            Instantiate(playerBoyPrefabs);
        }

        pauseButton.onClick.AddListener(PauseButton);
        resumeButton.onClick.AddListener(ResumeButton);
        settingsButton.onClick.AddListener(SettingsButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
        goToMainMenuButton.onClick.AddListener(GoToMainMenuButton);
        
        closePauseButton.onClick.AddListener(ResumeButton);
        closeSettingsButton.onClick.AddListener(CloseSettingsButton);
        closeMainMenuButton.onClick.AddListener(CloseMainMenuButton);
        cancelMainMenuButton.onClick.AddListener(CloseMainMenuButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (windowSettings)
            {
                CloseSettingsButton();
            }
            else if (windowMainMenu)
            {
                CloseMainMenuButton();
            }
            else if (windowPause)
            {
                ResumeButton();
            }
            else
            {
                if (!actionController.deskIsActive)
                {
                    PauseButton();
                }
            }
        }
    }

    private void PauseButton()
    {
        soundController.PositiveButtonSound(gameObject);
        StartCoroutine(AnimationPause());
    }

    private IEnumerator AnimationPause()
    {
        Time.timeScale = 0f;
        windowPause = true;
        pausePanel.GetComponent<Animator>().SetTrigger("Show");
        actionController.DeactiveCanvasAction();
        yield return null;
    }

    private void ResumeButton()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(AnimationResume());
    }

    private IEnumerator AnimationResume()
    {
        Time.timeScale = 1f;
        windowPause = false;
        pausePanel.GetComponent<Animator>().SetTrigger("Hide");
        actionController.ActiveCanvasAction();
        yield return null;
    }

    private void SettingsButton()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(AnimationSettings());
    }

    private IEnumerator AnimationSettings()
    {
        windowSettings = true;
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private void MainMenuButton()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(AnimationMainMenu());
    }

    private IEnumerator AnimationMainMenu()
    {
        windowMainMenu = true;
        mainMenuPanel.SetActive(true);
        mainMenuPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private void CloseSettingsButton()
    {
        soundController.NegativeButtonSound(gameObject);

        StartCoroutine(AnimationCloseSettings());
    }

    private IEnumerator AnimationCloseSettings()
    {
        windowSettings = false;
        settingsPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void CloseMainMenuButton()
    {
        soundController.NegativeButtonSound(gameObject);

        StartCoroutine(AnimationCloseMainMenu());
    }

    private IEnumerator AnimationCloseMainMenu()
    {
        windowMainMenu = false;
        mainMenuPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void GoToMainMenuButton()
    {
        soundController.PositiveButtonSound(gameObject);

        PlayerPrefsController.instance.SetNextScene("MainMenu");
    }
}
