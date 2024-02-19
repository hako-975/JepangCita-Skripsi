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
    
    [SerializeField]
    private Light directionalLight;

    public float realSecondCounter;
    public float realSecondsPerGameMinute;
    int gameHour;
    int gameMinute;
    int gameDay;
    int gameMonth;
    int gameYear;

    float gameHourStartSun;
    float gameMinuteStartSun;

    // Start is called before the first frame update
    void Start()
    {
        gameYear = PlayerPrefsController.instance.GetDateYear();
        gameMonth = PlayerPrefsController.instance.GetDateMonth();
        gameDay = PlayerPrefsController.instance.GetDateDay();

        UpdateDateText(gameYear, gameMonth, gameDay);

        gameHour = PlayerPrefsController.instance.GetHour();
        gameHourStartSun = gameHour;
        gameMinute = PlayerPrefsController.instance.GetMinute();
        gameMinuteStartSun = gameMinute;
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
            if (gameMinute > 59)
            {
                gameMinute = 0;
                gameHour++;
                if (gameHour > 23) 
                {
                    gameHour = 0;
                    gameDay++;


                    // Check if the day exceeds 31
                    if (gameDay > DateTime.DaysInMonth(gameYear, gameMonth))
                    {
                        gameDay = 1;
                        gameMonth++;
                        // Check if the month exceeds 12
                        if (gameMonth > 12)
                        {
                            gameMonth = 1;
                            gameYear++;
                            PlayerPrefsController.instance.SetDateYear(gameYear);
                        }

                        PlayerPrefsController.instance.SetDateMonth(gameMonth);
                    }

                    PlayerPrefsController.instance.SetDateDay(gameDay);

                    UpdateDateText(gameYear, gameMonth, gameDay);
                }

                PlayerPrefsController.instance.SetHour(gameHour);
            }

            PlayerPrefsController.instance.SetMinute(gameMinute);
        }

        // Display the time in text
        timeText.text = string.Format("{0:D2}:{1:D2}", gameHour, gameMinute);
        
        UpdateSunRotation();
    }

    // Update the date text in your UI
    private void UpdateDateText(int gameYear, int gameMonth, int gameDay)
    {
        DateTime specificDate = new DateTime(gameYear, gameMonth, gameDay);
        CultureInfo cultureInfo = new CultureInfo(PlayerPrefsController.instance.localeName);
        dateDayText.text = specificDate.ToString("dd dddd", cultureInfo);
    }

    private void UpdateSunRotation()
    {
        // Calculate total elapsed time in seconds
        float startTime = gameMinuteStartSun + (gameHourStartSun * 60f);
        float totalSeconds = Time.time + startTime;
        // Calculate rotation angle based on current time
        float zenithHour = 6f;
        float rotationAngle = (totalSeconds - zenithHour * 60f) / (24f * 60f) * 360f;
        // Apply rotation to directional light
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(rotationAngle, 90f, 0f));
    }

}