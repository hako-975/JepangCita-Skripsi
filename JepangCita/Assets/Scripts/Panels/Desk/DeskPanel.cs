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

    // Start is called before the first frame update
    void Start()
    {
        calendarPanel.SetActive(false);
        mailPanel.SetActive(false);

        calendarPanel.GetComponent<Button>().onClick.AddListener(delegate { OpenCalendarButton(true); });
        mailPanel.GetComponent<Button>().onClick.AddListener(delegate { OpenMailButton(true); });

        calendarButton.onClick.AddListener(delegate { OpenCalendarButton(false); });
        mailButton.onClick.AddListener(delegate { OpenMailButton(false); });
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
    }
}
