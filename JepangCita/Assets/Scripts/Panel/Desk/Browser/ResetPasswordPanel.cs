using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class ResetPasswordPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField emailInput;

    [Header("Buttons")]
    [SerializeField]
    private Button resetButton;

    [Header("Panels")]
    [SerializeField]
    private GameObject succeedPanel;
    [SerializeField]
    private TextMeshProUGUI messageSucceed;



    // Start is called before the first frame update
    void Start()
    {
        succeedPanel.SetActive(false);

        resetButton.onClick.AddListener(ResetPassword);
    }

    private void ResetPassword()
    {
        soundController.PositiveButtonSound(gameObject);

        StartCoroutine(ShowAndHidePanelCoroutine(succeedPanel));
    }

    IEnumerator ShowAndHidePanelCoroutine(GameObject panel)
    {
        messageSucceed.text = "Cek e-mail untuk melakukan reset password!";
        string message = "Konnichiwa " + PlayerPrefsController.instance.GetFullnameJepangCita() + "," +
            "\n\n" + "Anda telah meminta untuk reset password Anda. Klik tombol di bawah untuk membuat password baru." +
            "\n" + "Jika Anda belum meminta untuk reset password, harap abaikan email ini. Anda tidak perlu melakukan apa pun untuk memastikan akun Anda aman." +
            "\n\n" + "Arigatou," +
            "\n" + "JepangCita Tim";

        DateTime currentDateTime = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay());
        string dateSent = currentDateTime.ToString("dddd, dd MMMM yyyy, HH:mm", new CultureInfo("id-ID"));

        PlayerPrefsController.instance.SetInboxMail("noreply@jepangcita.com", "Jepang Cita Reset Password", message, dateSent, true);

        panel.SetActive(true);

        panel.GetComponent<Animator>().SetTrigger("Show");

        yield return new WaitForSeconds(3f);

        panel.GetComponent<Animator>().SetTrigger("Hide");
    }
}
