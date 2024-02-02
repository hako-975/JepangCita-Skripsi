using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class DateTimeText : MonoBehaviour
{
    private TextMeshProUGUI dateTimeText;

    // Start is called before the first frame update
    void Start()
    {
        dateTimeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        dateTimeText.text = string.Format("{0:D2}:{1:D2}", PlayerPrefsController.instance.GetHour(), PlayerPrefsController.instance.GetMinute()) + "\n" + UpdateDateText(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay());
    }

    private string UpdateDateText(int gameYear, int gameMonth, int gameDay)
    {
        DateTime specificDate = new DateTime(gameYear, gameMonth, gameDay);
        CultureInfo cultureInfo = new CultureInfo(PlayerPrefsController.instance.localeName);
        return specificDate.ToString("dd/MM/yyyy", cultureInfo);
    }
}
