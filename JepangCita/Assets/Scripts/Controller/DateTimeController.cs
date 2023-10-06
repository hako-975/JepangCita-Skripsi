using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class DateTimeController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateDayText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    
    private readonly string localeName = "id-ID";
    float realSecondCounter;
    float realSecondsPerGameMinute;
    int gameHour;
    int gameMinute;
    int displayHour;
    string dateDay;

    // Start is called before the first frame update
    void Start()
    {
        DateTime specificDate = new DateTime(2023, PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay());
        CultureInfo cultureInfo = new CultureInfo(localeName);
        
        dateDay = specificDate.ToString("dd dddd", cultureInfo);
        dateDayText.text = dateDay;

        gameHour = 6; // Jam awal dalam game (misalnya, pukul 12 PM)
        gameMinute = 0; // Menit awal dalam game
        realSecondsPerGameMinute = 1f; // 1 menit dunia nyata = 1 jam dunia game
        realSecondCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Perbarui perhitungan waktu berdasarkan waktu nyata
        realSecondCounter += Time.deltaTime;

        // Cek apakah sudah waktunya menambahkan satu menit game
        if (realSecondCounter >= realSecondsPerGameMinute)
        {
            realSecondCounter -= realSecondsPerGameMinute;

            // Tambahkan satu menit dalam game
            gameMinute++;
            if (gameMinute >= 60)
            {
                gameMinute = 0;
                gameHour++;
                if (gameHour > 23)
                {
                    gameHour = 0;
                    
                    PlayerPrefsController.instance.SetDateDay(PlayerPrefsController.instance.GetDateDay() + 1);
                        
                    DateTime specificDate = new DateTime(2023, PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay());
                    CultureInfo cultureInfo = new CultureInfo(localeName);

                    dateDay = specificDate.ToString("dd dddd", cultureInfo);
                    dateDayText.text = dateDay;
                }
            }
        }

        // Tampilkan waktu dalam teks
        timeText.text = string.Format("{0:D2}:{1:D2}", gameHour, gameMinute);
    }
}
