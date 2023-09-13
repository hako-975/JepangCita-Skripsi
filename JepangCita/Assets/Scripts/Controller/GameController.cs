using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
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

    private bool windowPause = false;
    private bool windowSettings = false;
    private bool windowMainMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(PauseButton);
        resumeButton.onClick.AddListener(ResumeButton);
        settingsButton.onClick.AddListener(SettingsButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
        goToMainMenuButton.onClick.AddListener(GoToMainMenuButton);
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
                PauseButton();
            }
        }
    }

    private void PauseButton()
    {
        StartCoroutine(AnimationPause());
    }

    private IEnumerator AnimationPause()
    {
        Time.timeScale = 0f;
        windowPause = true;
        pausePanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    private void ResumeButton()
    {
        StartCoroutine(AnimationResume());
    }

    private IEnumerator AnimationResume()
    {
        Time.timeScale = 1f;
        windowPause = false;
        pausePanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void SettingsButton()
    {
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
        PlayerPrefsController.instance.SetNextScene("MainMenu");
    }
}
