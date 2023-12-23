using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashboardPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI statusText;
    [SerializeField]
    private TextMeshProUGUI materiNumberText;
    [SerializeField]
    private TextMeshProUGUI latestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int materiNumber = PlayerPrefsController.instance.GetMateriNumberLearned();
        materiNumberText.text = materiNumber.ToString();

        int latestScore = PlayerPrefsController.instance.GetLatestScore();
        latestScoreText.text = latestScore + "%";


        if (materiNumber < 5)
        {
            statusText.text = "Pemula";
        }
        if (materiNumber >= 5)
        {
            statusText.text = "Pedagang";
        }
        if (materiNumber >= 10 && latestScore >= 70)
        {
            statusText.text = "Pengrajin";
        }
        if (materiNumber >= 15 && latestScore >= 75)
        {
            statusText.text = "Ninja";
        }
        if (materiNumber >= 20 && latestScore >= 80)
        {
            statusText.text = "Samurai";
        }
        if (materiNumber >= 25 && latestScore >= 85)
        {
            statusText.text = "Daimyo";
        }
        if (materiNumber >= 30 && latestScore >= 90)
        {
            statusText.text = "Shogun";
        }
    }
}
