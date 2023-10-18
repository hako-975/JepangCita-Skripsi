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

    float realSecondCounter;
    float realSecondsPerGameMinute;
    int gameHour;
    int gameMinute;
    int gameDay;
    int gameMonth;
    int gameYear;

    // Start is called before the first frame update
    void Start()
    {
        gameYear = PlayerPrefsController.instance.GetDateYear();
        gameMonth = PlayerPrefsController.instance.GetDateMonth();
        gameDay = PlayerPrefsController.instance.GetDateDay();

        UpdateDateText(gameYear, gameMonth, gameDay);

        gameHour = PlayerPrefsController.instance.GetHour();
        gameMinute = PlayerPrefsController.instance.GetMinute();
        realSecondsPerGameMinute = 1f;
        realSecondCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update time based on real-time
        realSecondCounter += Time.deltaTime;

        // Check if it's time to add one game minute
        if (realSecondCounter >= realSecondsPerGameMinute)
        {
            realSecondCounter -= realSecondsPerGameMinute;

            // Add one game minute
            gameMinute++;
            PlayerPrefsController.instance.SetMinute(gameMinute);
            if (gameMinute >= 60)
            {
                gameMinute = 0;
                gameHour++;
                PlayerPrefsController.instance.SetHour(gameHour);
                if (gameHour > 23) //23
                {
                    gameHour = 0;
                    gameDay++;

                    PlayerPrefsController.instance.SetDateDay(gameDay);

                    // Check if the day exceeds 31
                    if (gameDay > DateTime.DaysInMonth(gameYear, gameMonth))
                    {
                        gameDay = 1;
                        gameMonth++;
                        PlayerPrefsController.instance.SetDateMonth(gameMonth);
                        // Check if the month exceeds 12
                        if (gameMonth > 12)
                        {
                            gameMonth = 1;
                            gameYear++;
                            PlayerPrefsController.instance.SetDateYear(gameYear);
                        }
                    }

                    UpdateDateText(gameYear, gameMonth, gameDay);
                }
            }
        }

        // Display the time in text
        timeText.text = string.Format("{0:D2}:{1:D2}", gameHour, gameMinute);
    }

    // Update the date text in your UI
    private void UpdateDateText(int gameYear, int gameMonth, int gameDay)
    {
        DateTime specificDate = new DateTime(gameYear, gameMonth, gameDay);
        CultureInfo cultureInfo = new CultureInfo(PlayerPrefsController.instance.localeName);
        dateDayText.text = specificDate.ToString("dd dddd", cultureInfo);
    }
}
