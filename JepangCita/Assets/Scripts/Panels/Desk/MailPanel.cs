using System;
using System.Collections;
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
    private GameObject prefabMessageButton;

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
        StartCoroutine(AnimationOpenWeb(writePanel));
    }

    private void InboxNavButton()
    {
        DisabledAllPanel();
        inboxPanel.SetActive(true);
        scrollRect.content = inboxPanel.GetComponent<RectTransform>();
    }

    public void SentNavButton()
    {
        DisabledAllPanel();
        sentPanel.SetActive(true);

        scrollRect.content = sentPanel.GetComponent<RectTransform>();

        foreach (Transform child in sentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i <= PlayerPrefsController.instance.GetCountSentMail(); i++)
        {
            GameObject messageButton = Instantiate(prefabMessageButton, sentPanel.transform);
            MessageButton messageButtonComponent = messageButton.GetComponent<MessageButton>();
            string dataSentMail = PlayerPrefsController.instance.GetSentMail(i);

            string[] separatedData = dataSentMail.Split(new string[] { "?>?" }, StringSplitOptions.None);

            messageButtonComponent.senderMail.text = separatedData[0];
            messageButtonComponent.titleMail.text = separatedData[1];
            messageButtonComponent.dateMail.text = separatedData[3].Split(',')[1].Trim();
        }
    }


    public void DraftNavButton()
    {
        DisabledAllPanel();
        draftPanel.SetActive(true);
        scrollRect.content = draftPanel.GetComponent<RectTransform>();
        foreach (Transform child in draftPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i <= PlayerPrefsController.instance.GetCountDraftMail(); i++)
        {
            GameObject messageButton = Instantiate(prefabMessageButton, draftPanel.transform);
            MessageButton messageButtonComponent = messageButton.GetComponent<MessageButton>();
            string dataDraftMail = PlayerPrefsController.instance.GetDraftMail(i);

            string[] separatedData = dataDraftMail.Split(new string[] { "?>?" }, StringSplitOptions.None);

            messageButtonComponent.senderMail.text = separatedData[0];
            messageButtonComponent.titleMail.text = separatedData[1];
            messageButtonComponent.dateMail.text = separatedData[3].Split(',')[1].Trim();
        }
    }

    private void TrashNavButton()
    {
        DisabledAllPanel();
        trashPanel.SetActive(true);
        scrollRect.content = trashPanel.GetComponent<RectTransform>();
    }

    private void DisabledAllPanel()
    {
        inboxPanel.SetActive(false);
        sentPanel.SetActive(false);
        draftPanel.SetActive(false);
        trashPanel.SetActive(false);
    }

    private IEnumerator AnimationOpenWeb(GameObject panel)
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }
}
