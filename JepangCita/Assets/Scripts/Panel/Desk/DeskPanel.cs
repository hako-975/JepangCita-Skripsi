using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeskPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject openedAppPrefabs;

    [SerializeField]
    private GameObject leftTaskbar;

    [Header("Calendars")]
    [SerializeField]
    private Button calendarButton;

    [SerializeField]
    private GameObject calendarPanel;

    [SerializeField]
    private Sprite calendarIcon;

    private bool calendarOpened;
    
    private GameObject calendarApps;


    [Header("Mails")]
    [SerializeField]
    private Button mailButton;
    
    [SerializeField]
    private GameObject mailPanel;

    [SerializeField]
    private Sprite mailIcon;

    private bool mailOpened;
    
    private GameObject mailApps;

    [Header("Browsers")]
    [SerializeField]
    private Button browserButton;

    [SerializeField]
    private GameObject browserPanel;

    [SerializeField]
    private Sprite browserIcon;

    private bool browserOpened;

    private GameObject browserApps;

    [Header("Musics")]
    [SerializeField]
    private Button musicButton;

    [SerializeField]
    private GameObject musicPanel;

    [SerializeField]
    private Sprite musicIcon;

    private bool musicOpened;

    private GameObject musicApps;

    // Start is called before the first frame update
    void Start()
    {
        calendarPanel.SetActive(false);
        mailPanel.SetActive(false);
        browserPanel.SetActive(false);
        musicPanel.SetActive(false);

        calendarPanel.GetComponent<Button>().onClick.AddListener(delegate { OpenCalendarButton(true); });
        mailPanel.GetComponent<Button>().onClick.AddListener(delegate { OpenMailButton(true); });
        browserPanel.GetComponent<Button>().onClick.AddListener(delegate { OpenBrowserButton(true); });
        musicPanel.GetComponent<Button>().onClick.AddListener(delegate { OpenMusicButton(true); });

        calendarButton.onClick.AddListener(delegate { OpenCalendarButton(false); });
        mailButton.onClick.AddListener(delegate { OpenMailButton(false); });
        browserButton.onClick.AddListener(delegate { OpenBrowserButton(false); });
        musicButton.onClick.AddListener(delegate { OpenMusicButton(false); });
    }

    private void OpenCalendarButton(bool alreadyOpen = false)
    {
        if (!alreadyOpen)
        {
            StartCoroutine(AnimationOpenCalendar());
        }

        if (!calendarOpened)
        {
            calendarApps = Instantiate(openedAppPrefabs, leftTaskbar.transform);
            calendarApps.GetComponent<OpenedApp>().icon.sprite = calendarIcon;
        }

        int lastIndex = calendarPanel.transform.parent.childCount - 1;
        calendarPanel.transform.SetSiblingIndex(lastIndex);
        calendarApps.GetComponent<OpenedApp>().GetComponent<Button>().onClick.AddListener(delegate { OpenCalendarButton(false); });
        ToggleAllOffTaskbar();
        calendarApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 0.4902f);

        calendarOpened = true;
    }

    private IEnumerator AnimationOpenCalendar()
    {
        calendarPanel.SetActive(true);
        calendarPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    public void MinimizeCalendarButton()
    {
        StartCoroutine(AnimationCloseCalendar());
        ToggleAllOffTaskbar();
    }

    public void CloseCalendarButton()
    {
        StartCoroutine(AnimationCloseCalendar());
        Destroy(calendarApps);
        calendarOpened = false;
    }

    private IEnumerator AnimationCloseCalendar()
    {
        calendarPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void OpenMailButton(bool alreadyOpen = false)
    {
        if (!alreadyOpen)
        {
            StartCoroutine(AnimationOpenMail());
        }

        if (!mailOpened)
        {
            mailApps = Instantiate(openedAppPrefabs, leftTaskbar.transform);
            mailApps.GetComponent<OpenedApp>().icon.sprite = mailIcon;
        }

        int lastIndex = mailPanel.transform.parent.childCount - 1;
        mailPanel.transform.SetSiblingIndex(lastIndex);
        mailApps.GetComponent<OpenedApp>().GetComponent<Button>().onClick.AddListener(delegate { OpenMailButton(false); });
        ToggleAllOffTaskbar();
        mailApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 0.4902f);
        mailOpened = true;
    }

    private IEnumerator AnimationOpenMail()
    {
        mailPanel.SetActive(true);
        mailPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    public void MinimizeMailButton()
    {
        StartCoroutine(AnimationCloseMail());
        ToggleAllOffTaskbar();
    }

    public void CloseMailButton()
    {
        StartCoroutine(AnimationCloseMail());
        Destroy(mailApps);
        mailOpened = false;
    }

    private IEnumerator AnimationCloseMail()
    {
        mailPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    public void OpenBrowserButton(bool alreadyOpen = false)
    {
        if (!alreadyOpen)
        {
            StartCoroutine(AnimationOpenBrowser());
        }

        if (!browserOpened)
        {
            browserApps = Instantiate(openedAppPrefabs, leftTaskbar.transform);
            browserApps.GetComponent<OpenedApp>().icon.sprite = browserIcon;
        }

        int lastIndex = browserPanel.transform.parent.childCount - 1;
        browserPanel.transform.SetSiblingIndex(lastIndex);
        browserApps.GetComponent<OpenedApp>().GetComponent<Button>().onClick.AddListener(delegate { OpenBrowserButton(false); });
        ToggleAllOffTaskbar();
        browserApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 0.4902f);
        browserOpened = true;
    }

    private IEnumerator AnimationOpenBrowser()
    {
        browserPanel.SetActive(true);
        browserPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    public void MinimizeBrowserButton()
    {
        StartCoroutine(AnimationCloseBrowser());
        ToggleAllOffTaskbar();
    }

    public void CloseBrowserButton()
    {
        StartCoroutine(AnimationCloseBrowser());
        Destroy(browserApps);
        browserOpened = false;
    }

    private IEnumerator AnimationCloseBrowser()
    {
        browserPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void OpenMusicButton(bool alreadyOpen = false)
    {
        if (!alreadyOpen)
        {
            StartCoroutine(AnimationOpenMusic());
        }

        if (!musicOpened)
        {
            musicApps = Instantiate(openedAppPrefabs, leftTaskbar.transform);
            musicApps.GetComponent<OpenedApp>().icon.sprite = musicIcon;
        }

        int lastIndex = musicPanel.transform.parent.childCount - 1;
        musicPanel.transform.SetSiblingIndex(lastIndex);
        musicApps.GetComponent<OpenedApp>().GetComponent<Button>().onClick.AddListener(delegate { OpenMusicButton(false); });
        ToggleAllOffTaskbar();
        musicApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 0.4902f);

        musicOpened = true;
    }

    private IEnumerator AnimationOpenMusic()
    {
        musicPanel.SetActive(true);
        musicPanel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }

    public void MinimizeMusicButton()
    {
        StartCoroutine(AnimationCloseMusic());
        ToggleAllOffTaskbar();
    }

    public void CloseMusicButton()
    {
        StartCoroutine(AnimationCloseMusic());
        Destroy(musicApps);
        musicOpened = false;
    }

    private IEnumerator AnimationCloseMusic()
    {
        musicPanel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }

    private void ToggleAllOffTaskbar()
    {
        if (calendarApps)
        {
            calendarApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }

        if (mailApps)
        {
            mailApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }

        if (browserApps)
        {
            browserApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }

        if (musicApps)
        {
            musicApps.GetComponent<OpenedApp>().GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
