using System;
using UnityEngine;

public class AttendanceHomeController : MonoBehaviour
{
    private int gameDay;
    private int gameMonth;
    private int gameYear;
    private string formattedDate;

    // Update is called once per frame
    void Update()
    {
        gameDay = PlayerPrefsController.instance.GetDateDay(); 
        gameMonth = PlayerPrefsController.instance.GetDateMonth();
        gameYear = PlayerPrefsController.instance.GetDateYear();

        DateTime date = new DateTime(gameYear, gameMonth, gameDay);

        string dayName = date.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));

        formattedDate = date.ToString("dd/MM/yyyy");
        
        if (PlayerPrefsController.instance.GetAttendance(formattedDate) == "-?>?-?>?-")
        {
            // senin dan rabu, jam 9 - jam 12
            if (dayName == "Senin" || dayName == "Rabu")
            {
                if (PlayerPrefsController.instance.GetHour() >= 12)
                {
                    // Bolos
                    PlayerPrefsController.instance.SetAttendance(dayName, formattedDate, "Bolos");
                }
            }

            // jumat, jam 13 - jam 16
            if (dayName == "Jumat")
            {
                if (PlayerPrefsController.instance.GetHour() >= 16)
                {
                    // Bolos
                    PlayerPrefsController.instance.SetAttendance(dayName, formattedDate, "Bolos");
                }
            }
        }
        
    }
}
