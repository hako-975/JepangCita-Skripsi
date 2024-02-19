using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

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

    [Header("Alert Panels")]
    public GameObject succeedPanel;
    public TextMeshProUGUI messageSucceed;
    public GameObject failedPanel;
    public TextMeshProUGUI messageFailed;

    [Header("Message")]
    [SerializeField]
    private GameObject contentMessage;

    [SerializeField]
    private GameObject detailMessagePrefabs;

    [SerializeField]
    private GameObject noMessagePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InboxNavButton();

        writePanel.GetComponent<Animator>().SetTrigger("Hide");

        writeNavButton.onClick.AddListener(WriteNavButton);
        inboxNavButton.onClick.AddListener(InboxNavButton);
        sentNavButton.onClick.AddListener(SentNavButton);
        draftNavButton.onClick.AddListener(DraftNavButton);
        trashNavButton.onClick.AddListener(TrashNavButton);
    }

    private void WriteNavButton()
    {
        soundController.PositiveButtonSound(gameObject);

        writePanel.GetComponent<Animator>().SetTrigger("Show");
    }

    private void InboxNavButton()
    {
        soundController.PositiveButtonSound(gameObject);

        DisabledAllPanel();

        inboxNavButton.GetComponent<Image>().color = Color.white;
        
        sentNavButton.GetComponent<Image>().color = Color.grey;
        draftNavButton.GetComponent<Image>().color = Color.grey;
        trashNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(inboxPanel, PlayerPrefsController.instance.GetCountInboxMail(), i => PlayerPrefsController.instance.GetInboxMail(i));
    }

    public void SentNavButton()
    {
        soundController.PositiveButtonSound(gameObject);

        DisabledAllPanel();

        sentNavButton.GetComponent<Image>().color = Color.white;

        inboxNavButton.GetComponent<Image>().color = Color.grey;
        draftNavButton.GetComponent<Image>().color = Color.grey;
        trashNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(sentPanel, PlayerPrefsController.instance.GetCountSentMail(), i => PlayerPrefsController.instance.GetSentMail(i));
    }

    public void DraftNavButton()
    {
        soundController.PositiveButtonSound(gameObject);

        DisabledAllPanel();

        draftNavButton.GetComponent<Image>().color = Color.white;

        inboxNavButton.GetComponent<Image>().color = Color.grey;
        sentNavButton.GetComponent<Image>().color = Color.grey;
        trashNavButton.GetComponent<Image>().color = Color.grey;

        ShowMessage(draftPanel, PlayerPrefsController.instance.GetCountDraftMail(), i => PlayerPrefsController.instance.GetDraftMail(i));
    }

    private void TrashNavButton()
    {
        soundController.PositiveButtonSound(gameObject);

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

        StartCoroutine(CheckChildCountNextFrame(panel));
    }

    IEnumerator CheckChildCountNextFrame(GameObject panel)
    {
        // Wait for the end of the frame
        yield return new WaitForEndOfFrame();
        if (panel.transform.childCount < 1)
        {
            Instantiate(noMessagePrefabs, panel.transform);
        }
    }

    private void OpenMail(int id, GameObject panel, string sender, string title, string message, string date, string isChangedPasswordString)
    {
        soundController.PositiveButtonSound(gameObject);

        detailMessagePanel.SetActive(true);

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

        detailMessage.changePasswordButton.gameObject.SetActive(false);
        if (isChangedPassword)
        {
            detailMessage.changePasswordButton.gameObject.SetActive(true);
            detailMessage.changePasswordButton.onClick.AddListener(ChangePassword);
        }
        
        detailMessage.editButton.gameObject.SetActive(false);
        if (panel == draftPanel)
        {
            detailMessage.editButton.gameObject.SetActive(true);
            detailMessage.editButton.onClick.AddListener(delegate { EditDraft(id, sender, title, message); });
        }

        detailMessage.deleteButton.onClick.AddListener(delegate { DeleteMail(id, panel, sender, title, message); });
    }

    private void ChangePassword()
    {
        soundController.PositiveButtonSound(gameObject);

        PlayerPrefsController.instance.SetCredentialJepangCita(2);
        deskPanel.OpenBrowserButton(false);
        browserPanel.GetComponent<BrowserPanel>().OnChangePasswordButtonClick();
    }

    private void EditDraft(int id, string sender, string title, string message)
    {
        soundController.PositiveButtonSound(gameObject);

        PlayerPrefsController.instance.DeleteDraftMail(id);
        writePanel.GetComponent<Animator>().SetTrigger("Show");
        WritePanel writePanelScript = writePanel.GetComponent<WritePanel>();
        writePanelScript.toInput.text = sender;
        writePanelScript.subjectInput.text = title;
        writePanelScript.messageInput.text = message;
    }

    private void DeleteMail(int id, GameObject panel, string sender, string title, string message)
    {
        soundController.TrashButtonSound(gameObject);

        if (panel == inboxPanel)
        {
            PlayerPrefsController.instance.DeleteInboxMail(id);
            TrashMail(sender, title, message);
            InboxNavButton();
        }
        else if (panel == sentPanel)
        {
            PlayerPrefsController.instance.DeleteSentMail(id);
            TrashMail(sender, title, message);
            SentNavButton();
        }
        else if (panel == draftPanel)
        {
            PlayerPrefsController.instance.DeleteDraftMail(id);
            TrashMail(sender, title, message);
            DraftNavButton();
        }
        else if (panel == trashPanel)
        {
            PlayerPrefsController.instance.DeleteTrashMail(id);
            TrashNavButton();
        }
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
        detailMessagePanel.SetActive(false);
        inboxPanel.SetActive(false);
        sentPanel.SetActive(false);
        draftPanel.SetActive(false);
        trashPanel.SetActive(false);
        
        foreach (Transform child in contentMessage.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public IEnumerator ShowAndHidePanelCoroutine(GameObject writePanel, GameObject panel, bool isSent)
    {
        panel.SetActive(true);

        writePanel.GetComponent<Animator>().SetTrigger("Hide");

        panel.GetComponent<Animator>().SetTrigger("Show");

        if (panel == succeedPanel)
        {
            if (isSent)
            {
                SentNavButton();
            }
            else
            {
                DraftNavButton();
            }
        }

        yield return new WaitForSeconds(2f);
        panel.GetComponent<Animator>().SetTrigger("Hide");
    }
}
