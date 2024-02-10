using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MailPanel : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [Header("Buttons")]
    [SerializeField]
    private Button writeNavButton;
    [SerializeField]
    private Button inboxNavButton;
    [SerializeField]
    private Button sentNavButton;
    [SerializeField]
    private Button draftNavButton;
    [SerializeField]
    private Button trashNavButton;

    [SerializeField]
    private GameObject messageButtonPrefabs;

    [Header("Panels")]
    [SerializeField]
    private GameObject writePanel;
    [SerializeField]
    private GameObject inboxPanel;
    [SerializeField]
    private GameObject sentPanel;
    [SerializeField]
    private GameObject draftPanel;
    [SerializeField]
    private GameObject trashPanel;
    [SerializeField]
    private GameObject browserPanel;
    [SerializeField]
    private DeskPanel deskPanel;
    [SerializeField]
    private GameObject detailMessagePanel;

    [Header("Message")]
    [SerializeField]
    private GameObject contentMessage;

    [SerializeField]
    private GameObject detailMessagePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InboxNavButton();

        writePanel.SetActive(false);

        writeNavButton.onClick.AddListener(WriteNavButton);
        inboxNavButton.onClick.AddListener(InboxNavButton);
        sentNavButton.onClick.AddListener(SentNavButton);
        draftNavButton.onClick.AddListener(DraftNavButton);
        trashNavButton.onClick.AddListener(TrashNavButton);
    }

    private void WriteNavButton()
    {
        StartCoroutine(AnimationOpenMail(writePanel));
    }

    private void InboxNavButton()
    {
        DisabledAllPanel();

        inboxNavButton.GetComponent<Image>().color = Color.white;
        
        sentNavButton.GetComponent<Image>().color = Color.grey;
        draftNavButton.GetComponent<Image>().color = Color.grey;
        trashNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(inboxPanel, PlayerPrefsController.instance.GetCountInboxMail(), i => PlayerPrefsController.instance.GetInboxMail(i));
    }

    public void SentNavButton()
    {
        DisabledAllPanel();

        sentNavButton.GetComponent<Image>().color = Color.white;

        inboxNavButton.GetComponent<Image>().color = Color.grey;
        draftNavButton.GetComponent<Image>().color = Color.grey;
        trashNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(sentPanel, PlayerPrefsController.instance.GetCountSentMail(), i => PlayerPrefsController.instance.GetSentMail(i));
    }

    public void DraftNavButton()
    {
        DisabledAllPanel();

        draftNavButton.GetComponent<Image>().color = Color.white;

        inboxNavButton.GetComponent<Image>().color = Color.grey;
        sentNavButton.GetComponent<Image>().color = Color.grey;
        trashNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(draftPanel, PlayerPrefsController.instance.GetCountDraftMail(), i => PlayerPrefsController.instance.GetDraftMail(i));
    }

    private void TrashNavButton()
    {
        DisabledAllPanel();

        trashNavButton.GetComponent<Image>().color = Color.white;

        inboxNavButton.GetComponent<Image>().color = Color.grey;
        sentNavButton.GetComponent<Image>().color = Color.grey;
        draftNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(trashPanel, PlayerPrefsController.instance.GetCountTrashMail(), i => PlayerPrefsController.instance.GetTrashMail(i));
    }

    private void ShowMessage(GameObject panel, int getCountMail, Func<int, string> getDataMail)
    {
        panel.SetActive(true);
        scrollRect.content = panel.GetComponent<RectTransform>();

        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i <= getCountMail; i++)
        {
            GameObject messageButton = Instantiate(messageButtonPrefabs, panel.transform);
            MessageButton messageButtonComponent = messageButton.GetComponent<MessageButton>();
            string dataMail = getDataMail(i);
            string[] separatedData = dataMail.Split(new string[] { "?>?" }, StringSplitOptions.None);

            if (separatedData[0] == "deleted" && separatedData[1] == "deleted" && separatedData[2] == "deleted" && separatedData[3] == "deleted")
            {
                Destroy(messageButton);
                continue;
            }

            messageButtonComponent.senderMail.text = separatedData[0];
            messageButtonComponent.titleMail.text = separatedData[1];
            messageButtonComponent.dateMail.text = separatedData[3].Split(',')[1].Trim();
            
            string isChangedPassword = "false";
            
            if (panel == inboxPanel)
            {
                isChangedPassword = separatedData[4];
            }

            int id = i;
            
            messageButtonComponent.GetComponent<Button>().onClick.AddListener(delegate { OpenMail(id, panel, separatedData[0], separatedData[1], separatedData[2], separatedData[3], isChangedPassword); });
        }
    }

    private void OpenMail(int id, GameObject panel, string sender, string title, string message, string date, string isChangedPasswordString)
    {
        StartCoroutine(AnimationOpenMail(detailMessagePanel));
        foreach (Transform child in contentMessage.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject detailMessageInstantiate = Instantiate(detailMessagePrefabs, contentMessage.transform);
        DetailMessage detailMessage = detailMessageInstantiate.GetComponent<DetailMessage>();
        detailMessage.senderMail.text = sender;
        detailMessage.titleMail.text = title;
        detailMessage.messageMail.text = message;
        detailMessage.dateMail.text = date;

        bool.TryParse(isChangedPasswordString, out bool isChangedPassword);

        if (isChangedPassword)
        {
            detailMessage.changePasswordPanel.SetActive(true);
            detailMessage.changePasswordButton.onClick.AddListener(ChangePassword);
        }

        detailMessage.deleteButton.onClick.AddListener(delegate { DeleteMail(id, panel, sender, title, message); });
    }

    private void ChangePassword()
    {
        PlayerPrefsController.instance.SetCredentialJepangCita(2);
        deskPanel.OpenBrowserButton(false);
        browserPanel.GetComponent<BrowserPanel>().OnChangePasswordButtonClick();
    }

    private void DeleteMail(int id, GameObject panel, string sender, string title, string message)
    {
        if (panel == inboxPanel)
        {
            PlayerPrefsController.instance.DeleteInboxMail(id);
            TrashMail(sender, title, message);
        }
        else if (panel == sentPanel)
        {
            PlayerPrefsController.instance.DeleteSentMail(id);
            TrashMail(sender, title, message);
        }
        else if (panel == draftPanel)
        {
            PlayerPrefsController.instance.DeleteDraftMail(id);
            TrashMail(sender, title, message);
        }
        else if (panel == trashPanel)
        {
            PlayerPrefsController.instance.DeleteTrashMail(id);
        }

        TrashNavButton();
    }

    private void TrashMail(string sender, string title, string message)
    {
        DateTime currentDateTime = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay(), PlayerPrefsController.instance.GetHour(), PlayerPrefsController.instance.GetMinute(), 0);
        string dateSent = currentDateTime.ToString("dddd, dd MMMM yyyy, HH:mm", new CultureInfo("id-ID"));

        // kepada, subjek, pesan, tanggal
        PlayerPrefsController.instance.SetTrashMail(sender, title, message, dateSent);
    }

    private void DisabledAllPanel()
    {
        StartCoroutine(AnimationCloseMail(detailMessagePanel));
        inboxPanel.SetActive(false);
        sentPanel.SetActive(false);
        draftPanel.SetActive(false);
        trashPanel.SetActive(false);
    }

    private IEnumerator AnimationOpenMail(GameObject panel)
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().SetTrigger("Show");;
        yield return null;
    }

    private IEnumerator AnimationCloseMail(GameObject panel)
    {
        foreach (Transform child in contentMessage.transform)
        {
            Destroy(child.gameObject);
        }
        panel.GetComponent<Animator>().SetTrigger("Hide");
        yield return null;
    }
}
