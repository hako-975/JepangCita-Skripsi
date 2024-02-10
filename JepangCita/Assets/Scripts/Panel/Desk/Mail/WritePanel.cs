using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;
using System.Collections;

public class WritePanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button sentButton;
    [SerializeField]
    private Button closeButton;

    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField toInput;
    [SerializeField]
    private TMP_InputField subjectInput;
    [SerializeField]
    private TMP_InputField messageInput;

    [Header("Panels")]
    [SerializeField]
    private GameObject succeedPanel;
    [SerializeField]
    private TextMeshProUGUI messageSucceed;
    [SerializeField]
    private GameObject failedPanel;
    [SerializeField]
    private TextMeshProUGUI messageFailed;

    [SerializeField]
    private MailPanel mailPanel;

    // Start is called before the first frame update
    void Start()
    {
        sentButton.onClick.AddListener(SentButton);
        closeButton.onClick.AddListener(CloseButton);
    }

    private void SentButton()
    {
        if (toInput.text == "")
        {
            messageFailed.text = "Input Kepada harus diisi!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel, true));
            return;
        }

        if (subjectInput.text == "")
        {
            messageFailed.text = "Input Subjek harus diisi!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel, true));
            return;
        }

        if (messageInput.text == "")
        {
            messageFailed.text = "Input Pesan harus diisi!";
            StartCoroutine(ShowAndHidePanelCoroutine(failedPanel, true));
            return;
        }

        DateTime currentDateTime = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay(), PlayerPrefsController.instance.GetHour(), PlayerPrefsController.instance.GetMinute(), 0);
        string dateSent = currentDateTime.ToString("dddd, dd MMMM yyyy, HH:mm", new CultureInfo("id-ID"));

        // kepada, subjek, pesan, tanggal
        PlayerPrefsController.instance.SetSentMail(toInput.text, subjectInput.text, messageInput.text, dateSent);
        messageSucceed.text = "Pesan berhasil terikirim!";
        StartCoroutine(ShowAndHidePanelCoroutine(succeedPanel, true));
    }

    private void CloseButton()
    {
        if (toInput.text != "" || subjectInput.text != "" || messageInput.text != "")
        {
            DateTime currentDateTime = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay(), PlayerPrefsController.instance.GetHour(), PlayerPrefsController.instance.GetMinute(), 0);
            string dateSent = currentDateTime.ToString("dddd, dd MMMM yyyy, HH:mm", new CultureInfo("id-ID"));

            // kepada, subjek, pesan, tanggal
            PlayerPrefsController.instance.SetDraftMail(toInput.text, subjectInput.text, messageInput.text, dateSent);
            messageSucceed.text = "Pesan tersimpan di draf!";
            StartCoroutine(ShowAndHidePanelCoroutine(succeedPanel, false));
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Hide");
        }
    }

    IEnumerator ShowAndHidePanelCoroutine(GameObject panel, bool isSent)
    {
        panel.SetActive(true);

        panel.GetComponent<Animator>().SetTrigger("Show");;

        if (panel == succeedPanel)
        {
            GetComponent<Animator>().SetTrigger("Hide");

            if (isSent)
            {
                mailPanel.SentNavButton();
            }
            else
            {
                mailPanel.DraftNavButton();
            }
        }

        yield return new WaitForSeconds(2f);

        panel.GetComponent<Animator>().SetTrigger("Hide");
    }
}
