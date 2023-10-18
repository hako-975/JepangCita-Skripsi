using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarPanel : MonoBehaviour
{
    [Header("Month Year")]
    [SerializeField]
    TextMeshProUGUI monthYearText;
    [SerializeField]
    Button monthYearLeftButton;
    [SerializeField]
    Button monthYearRightButton;

    [SerializeField]
    Button[] calendarDayButtons;

    int gameMonth;
    int gameYear;
    int daysInCalendar;
    int startDay;
    int dayNumber;
    int lastActiveButtonIndex;

    // Start is called before the first frame update
    void Start()
    {
        daysInCalendar = calendarDayButtons.Length;
        gameYear = PlayerPrefsController.instance.GetDateYear();
        gameMonth = PlayerPrefsController.instance.GetDateMonth();
        DateTime firstDayAtMonthAndYear = new DateTime(gameYear, gameMonth, 1);
        startDay = (int)firstDayAtMonthAndYear.DayOfWeek;
        dayNumber = 1;
        lastActiveButtonIndex = 0;

        monthYearLeftButton.onClick.AddListener(MonthYearLeftButton);
        monthYearRightButton.onClick.AddListener(MonthYearRightButton);

        UpdateCalendar();
    }

    private void MonthYearLeftButton()
    {
        gameMonth--;
        if (gameMonth < 1)
        {
            gameMonth = 12;
            gameYear--;
        }
        UpdateCalendar();
        
    }

    private void MonthYearRightButton()
    {
        gameMonth++;
        if (gameMonth > 12)
        {
            gameMonth = 1;
            gameYear++;
        }

        UpdateCalendar();
    }

    private void UpdateCalendar()
    {
        if (gameYear > 2023 || (gameYear == 2023 && gameMonth > 1))
        {
            monthYearLeftButton.interactable = true;
        }
        else
        {
            monthYearLeftButton.interactable = false;
        }

        monthYearText.text = new DateTime(gameYear, gameMonth, 1).ToString("MMMM yyyy");
        
        DateTime firstDayAtMonthAndYear = new DateTime(gameYear, gameMonth, 1);
        startDay = (int)firstDayAtMonthAndYear.DayOfWeek;
        dayNumber = 1;

        // Populate first buttons to interactable false
        for (int i = 0; i < startDay; i++)
        {
            int dayInPreviousMonth = 0;
            if (gameMonth - 1 < 1)
            {
                dayInPreviousMonth = DateTime.DaysInMonth(gameYear - 1, 12);
            }
            else
            {
                dayInPreviousMonth = DateTime.DaysInMonth(gameYear, gameMonth - 1);
            }

            calendarDayButtons[i].GetComponent<CalendarDayButton>().tanggalText.text = (dayInPreviousMonth - startDay + i + 1).ToString();
            calendarDayButtons[i].interactable = false;
        }

        // main date
        for (int day = startDay; day < daysInCalendar; day++)
        {
            calendarDayButtons[day].GetComponent<CalendarDayButton>().tanggalText.text = dayNumber.ToString();
            calendarDayButtons[day].interactable = true;

            dayNumber++;

            if (dayNumber > DateTime.DaysInMonth(gameYear, gameMonth))
            {
                dayNumber = 1;
                lastActiveButtonIndex = day;
            }
        }

        // last interactable false
        for (int i = lastActiveButtonIndex + 1; i < lastActiveButtonIndex + 32; i++)
        {
            if (i < daysInCalendar)
            {
                calendarDayButtons[i].interactable = false;
            }
        }

        // for clicked main button
        for (int buttonIndex = 0; buttonIndex < daysInCalendar; buttonIndex++)
        {
            int dayIndex = buttonIndex;
            calendarDayButtons[buttonIndex].onClick.AddListener(() =>
            {
                Debug.Log("Button " + (dayIndex + 1) + " clicked.");
            });
        }
    }
}
