using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class WritePanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [Header("Buttons")]
    [SerializeField]
    private Button sentButton;
    [SerializeField]
    private Button closeButton;

    [Header("Inputs")]
    public TMP_InputField toInput;
    public TMP_InputField subjectInput;
    public TMP_InputField messageInput;

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
        soundController.PositiveButtonSound(gameObject);

        if (toInput.text == "")
        {
            mailPanel.messageFailed.text = "Input Kepada harus diisi!";
            StartCoroutine(mailPanel.ShowAndHidePanelCoroutine(gameObject, mailPanel.failedPanel, true));
            return;
        }

        if (subjectInput.text == "")
        {
            mailPanel.messageFailed.text = "Input Subjek harus diisi!";
            StartCoroutine(mailPanel.ShowAndHidePanelCoroutine(gameObject, mailPanel.failedPanel, true));
            return;
        }

        if (messageInput.text == "")
        {
            mailPanel.messageFailed.text = "Input Pesan harus diisi!";
            StartCoroutine(mailPanel.ShowAndHidePanelCoroutine(gameObject, mailPanel.failedPanel, true));
            return;
        }

        DateTime currentDateTime = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay(), PlayerPrefsController.instance.GetHour(), PlayerPrefsController.instance.GetMinute(), 0);
        string dateSent = currentDateTime.ToString("dddd, dd MMMM yyyy, HH:mm", new CultureInfo("id-ID"));

        // kepada, subjek, pesan, tanggal
        PlayerPrefsController.instance.SetSentMail(toInput.text, subjectInput.text, messageInput.text, dateSent);
        mailPanel.messageSucceed.text = "Pesan berhasil terikirim!";

        EmptyInput();

        StartCoroutine(mailPanel.ShowAndHidePanelCoroutine(gameObject, mailPanel.succeedPanel, true));
    }

    private void EmptyInput()
    {
        toInput.text = "";
        subjectInput.text = "";
        messageInput.text = "";
    }

    private void CloseButton()
    {
        soundController.NegativeButtonSound(gameObject);

        if (toInput.text != "" || subjectInput.text != "" || messageInput.text != "")
        {
            DateTime currentDateTime = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay(), PlayerPrefsController.instance.GetHour(), PlayerPrefsController.instance.GetMinute(), 0);
            string dateSent = currentDateTime.ToString("dddd, dd MMMM yyyy, HH:mm", new CultureInfo("id-ID"));

            // kepada, subjek, pesan, tanggal
            PlayerPrefsController.instance.SetDraftMail(toInput.text, subjectInput.text, messageInput.text, dateSent);
            mailPanel.messageSucceed.text = "Pesan tersimpan di draf!";

            EmptyInput();

            StartCoroutine(mailPanel.ShowAndHidePanelCoroutine(gameObject, mailPanel.succeedPanel, false));
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger("Hide");
        }
    }
}
