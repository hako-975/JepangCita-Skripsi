using TMPro;
using UnityEngine;

public class DashboardPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI statusText;
    [SerializeField]
    private TextMeshProUGUI materiNumberText;

    [Header("Score Text")]
    [SerializeField]
    private TextMeshProUGUI averageScoreText;
    [SerializeField]
    private TextMeshProUGUI hiraganaScoreText;
    [SerializeField]
    private TextMeshProUGUI katakanaScoreText;
    [SerializeField]
    private TextMeshProUGUI angkaScoreText;
    [SerializeField]
    private TextMeshProUGUI waktuScoreText;
    [SerializeField]
    private TextMeshProUGUI kataGantiScoreText;
    [SerializeField]
    private TextMeshProUGUI kataBendaScoreText;
    [SerializeField]
    private TextMeshProUGUI kataKerjaScoreText;
    [SerializeField]
    private TextMeshProUGUI kataSifatScoreText;
    [SerializeField]
    private TextMeshProUGUI kataKeteranganScoreText;
    [SerializeField]
    private TextMeshProUGUI kataTanyaScoreText;
    [SerializeField]
    private TextMeshProUGUI kataHubungScoreText;
    [SerializeField]
    private TextMeshProUGUI kataSeruScoreText;
    [SerializeField]
    private TextMeshProUGUI perkenalanDiriScoreText;

    int hiraganaScore = 0;
    int katakanaScore = 0;
    int angkaScore = 0;
    int waktuScore = 0;
    int kataGantiScore = 0;
    int kataBendaScore = 0;
    int kataKerjaScore = 0;
    int kataSifatScore = 0;
    int kataKeteranganScore = 0;
    int kataTanyaScore = 0;
    int kataHubungScore = 0;
    int kataSeruScore = 0;
    int perkenalanDiriScore = 0;
    int averageScore = 0;

    int sum = 0;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        int materiNumber = PlayerPrefsController.instance.GetCurrentMateri();
        materiNumberText.text = materiNumber.ToString();

        hiraganaScore = PlayerPrefsController.instance.GetHiraganaScore();
        katakanaScore = PlayerPrefsController.instance.GetKatakanaScore();
        angkaScore = PlayerPrefsController.instance.GetAngkaScore();
        waktuScore = PlayerPrefsController.instance.GetWaktuScore();
        kataGantiScore = PlayerPrefsController.instance.GetKataGantiScore();
        kataBendaScore = PlayerPrefsController.instance.GetKataBendaScore();
        kataKerjaScore = PlayerPrefsController.instance.GetKataKerjaScore();
        kataSifatScore = PlayerPrefsController.instance.GetKataSifatScore();
        kataKeteranganScore = PlayerPrefsController.instance.GetKataKeteranganScore();
        kataTanyaScore = PlayerPrefsController.instance.GetKataTanyaScore();
        kataHubungScore = PlayerPrefsController.instance.GetKataHubungScore();
        kataSeruScore = PlayerPrefsController.instance.GetKataSeruScore();
        perkenalanDiriScore = PlayerPrefsController.instance.GetPerkenalanDiriScore();

        hiraganaScoreText.text = hiraganaScore.ToString();
        katakanaScoreText.text = katakanaScore.ToString();
        angkaScoreText.text = angkaScore.ToString();
        waktuScoreText.text = waktuScore.ToString();
        kataGantiScoreText.text = kataGantiScore.ToString();
        kataBendaScoreText.text = kataBendaScore.ToString();
        kataKerjaScoreText.text = kataKerjaScore.ToString();
        kataSifatScoreText.text = kataSifatScore.ToString();
        kataKeteranganScoreText.text = kataKeteranganScore.ToString();
        kataTanyaScoreText.text = kataTanyaScore.ToString();
        kataHubungScoreText.text = kataHubungScore.ToString();
        kataSeruScoreText.text = kataSeruScore.ToString();
        perkenalanDiriScoreText.text = perkenalanDiriScore.ToString();

        if (hiraganaScore != 0) { sum += hiraganaScore; count++; }
        if (katakanaScore != 0) { sum += katakanaScore; count++; }
        if (angkaScore != 0) { sum += angkaScore; count++; }
        if (waktuScore != 0) { sum += waktuScore; count++; }
        if (kataGantiScore != 0) { sum += kataGantiScore; count++; }
        if (kataBendaScore != 0) { sum += kataBendaScore; count++; }
        if (kataKerjaScore != 0) { sum += kataKerjaScore; count++; }
        if (kataSifatScore != 0) { sum += kataSifatScore; count++; }
        if (kataKeteranganScore != 0) { sum += kataKeteranganScore; count++; }
        if (kataTanyaScore != 0) { sum += kataTanyaScore; count++; }
        if (kataHubungScore != 0) { sum += kataHubungScore; count++; }
        if (kataSeruScore != 0) { sum += kataSeruScore; count++; }
        if (perkenalanDiriScore != 0) { sum += perkenalanDiriScore; count++; }
        
        averageScore = count > 0 ? sum / count : 0;
        averageScoreText.text = averageScore.ToString();

        if (materiNumber < 5)
        {
            statusText.text = "Pemula";
        }
        if (materiNumber >= 5)
        {
            statusText.text = "Pedagang";
        }
        if (materiNumber >= 10 && averageScore >= 70)
        {
            statusText.text = "Pengrajin";
        }
        if (materiNumber >= 15 && averageScore >= 75)
        {
            statusText.text = "Ninja";
        }
        if (materiNumber >= 20 && averageScore >= 80)
        {
            statusText.text = "Samurai";
        }
        if (materiNumber >= 25 && averageScore >= 85)
        {
            statusText.text = "Daimyo";
        }
        if (materiNumber >= 30 && averageScore >= 90)
        {
            statusText.text = "Shogun";
        }
    }
}
