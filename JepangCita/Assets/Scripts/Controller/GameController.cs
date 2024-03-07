using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [Header("Buttons")]
    [SerializeField]
    private Button missionButton;

    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button mainMenuButton;

    [SerializeField]
    private Button closeMissionButton;

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
    private Animator transitionPanel;

    [SerializeField]
    private GameObject missionPanel;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private ActionController actionController;

    private bool windowMission = false;
    private bool windowPause = false;
    private bool windowSettings = false;
    private bool windowMainMenu = false;
    
    [Header("Prefabs")]
    [SerializeField]
    private GameObject playerBoyPrefabs;

    [SerializeField]
    private GameObject playerGirlPrefabs;

    [Header("Mission")]
    [SerializeField]
    private RectTransform[] rectTransformMissionContent;
    [SerializeField]
    private TextMeshProUGUI[] titleMissionText;
    [SerializeField]
    private TextMeshProUGUI[] detailMissionText;

    string titleMission;
    string detailMission;

    void Awake()
    {
        if (PlayerPrefsController.instance.GetCharacterSelection() == "Girl")
        {
            Instantiate(playerGirlPrefabs);
        }
        else
        {
            Instantiate(playerBoyPrefabs);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Mission();

        PlayerPrefsController.instance.SetLastScene(SceneManager.GetActiveScene().name);

        StartCoroutine(StartTransition());

        missionButton.onClick.AddListener(MissionButton);
        pauseButton.onClick.AddListener(PauseButton);
        resumeButton.onClick.AddListener(ResumeButton);
        settingsButton.onClick.AddListener(SettingsButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
        goToMainMenuButton.onClick.AddListener(GoToMainMenuButton);
        
        closeMissionButton.onClick.AddListener(ResumeMissionButton);
        closePauseButton.onClick.AddListener(ResumeButton);
        closeSettingsButton.onClick.AddListener(CloseSettingsButton);
        closeMainMenuButton.onClick.AddListener(CloseMainMenuButton);
        cancelMainMenuButton.onClick.AddListener(CloseMainMenuButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefsController.instance.GetUpdateMission() == 1)
        {
            Mission();
            PlayerPrefsController.instance.SetUpdateMission(0);
        }

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
            else if (windowMission)
            {
                ResumeMissionButton();
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

    IEnumerator StartTransition()
    {
        transitionPanel.gameObject.SetActive(true);
        transitionPanel.SetTrigger("Open");
        yield return new WaitForSeconds(3f);
        transitionPanel.SetTrigger("Idle");
    }

    private void Mission()
    {
        titleMission = "Tidak ada Misi";
        detailMission = "Tidak ada Misi";
        for (int i = 0; i < PlayerPrefsController.instance.missionList.Length; i++)
        {
            if (PlayerPrefsController.instance.GetMission() == i)
            {
                titleMission = PlayerPrefsController.instance.missionList[i].titleMission;
                detailMission = PlayerPrefsController.instance.missionList[i].detailMission;
                break;
            }
        }

        for (int i = 0; i < titleMissionText.Length; i++)
        {
            titleMissionText[i].text = titleMission;
            detailMissionText[i].text = detailMission;
        }
    }

    private void MissionButton()
    {
        soundController.PositiveButtonSound(gameObject);
        StartCoroutine(AnimationMission());
    }

    private void PauseButton()
    {
        soundController.PositiveButtonSound(gameObject);
        StartCoroutine(AnimationPause());
    }

    private IEnumerator AnimationMission()
    {
        missionPanel.SetActive(true);

        Time.timeScale = 0f;
        windowMission = true;
        missionPanel.GetComponent<Animator>().SetTrigger("Show");
        actionController.DeactiveCanvasAction();
        for (int i = 0; i < rectTransformMissionContent.Length; i++)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransformMissionContent[i]);
        }
        yield return null;
    }

    private IEnumerator AnimationPause()
    {
        pausePanel.SetActive(true);

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
    private void ResumeMissionButton()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(AnimationResumeMission());
    }

    private IEnumerator AnimationResumeMission()
    {
        Time.timeScale = 1f;
        windowMission = false;
        missionPanel.GetComponent<Animator>().SetTrigger("Hide");
        actionController.ActiveCanvasAction();
        yield return null;
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
