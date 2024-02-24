using System;
using UnityEngine;
using UnityEngine.UI;

public class JadwalPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject tableHead;

    [SerializeField]
    private GameObject kehadiranPrefabs;

    [SerializeField]
    private GameObject tabelKehadiran;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Sprite checkIcon;
    [SerializeField]
    private Sprite crossIcon;
    [SerializeField]
    private Sprite transparentIcon;

    string formattedDate;
    int gameDay;
    int gameMonth;
    int gameYear;

    int tempDay;

    // Start is called before the first frame update
    void Start()
    {
        gameDay = PlayerPrefsController.instance.GetDateDay();
        gameMonth = PlayerPrefsController.instance.GetDateMonth();
        gameYear = PlayerPrefsController.instance.GetDateYear();
        
        tempDay = PlayerPrefsController.instance.GetDateDay();

        RefreshTable();
    }

    private void Update()
    {
        if (tempDay != PlayerPrefsController.instance.GetDateDay())
        {
            RefreshTable();
            tempDay = PlayerPrefsController.instance.GetDateDay();
        }
    }

    void RefreshTable()
    {
        // Clear existing table rows
        foreach (Transform child in tabelKehadiran.transform)
        {
            if (child.gameObject != tableHead)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < PlayerPrefsController.instance.GetDateDay(); i++)
        {
            DateTime date = new DateTime(gameYear, gameMonth, gameDay);
            string dayName = date.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));

            // Check if it's Monday, Tuesday, Wednesday, or Thursday
            if (dayName == "Senin" || dayName == "Selasa" || dayName == "Rabu" || dayName == "Kamis")
            {
                formattedDate = date.ToString("dd/MM/yyyy");
                PlayerPrefsController.instance.SetAttendance(dayName, formattedDate, "-");

                // Instantiate row
                GameObject rowKehadiranInstantiate = Instantiate(kehadiranPrefabs, tabelKehadiran.transform);
                Kehadiran rowKehadiran = rowKehadiranInstantiate.GetComponent<Kehadiran>();
                string[] separatedData = PlayerPrefsController.instance.GetAttendance(formattedDate).Split(new string[] { "?>?" }, StringSplitOptions.None);

                rowKehadiran.hariText.text = separatedData[0];
                rowKehadiran.tanggalText.text = separatedData[1];
                if (separatedData[2] == "hadir")
                {
                    rowKehadiran.kehadiranImage.sprite = checkIcon;
                    rowKehadiran.kehadiranImage.color = new Color(0.5882352941176471f, 0.788235294117647f, 0.06274509803921569f);
                }
                else if (separatedData[2] == "bolos")
                {
                    rowKehadiran.kehadiranImage.sprite = crossIcon;
                    rowKehadiran.kehadiranImage.color = new Color(0.9607843137254902f, 0.2627450980392157f, 0.3254901960784314f);
                }
                else
                {
                    rowKehadiran.kehadiranImage.sprite = transparentIcon;
                }

                rowKehadiran.keteranganText.text = separatedData[2];
            }

            IncreaseDay();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }


    private void IncreaseDay()
    {
        gameDay++;

        if (gameDay > DateTime.DaysInMonth(gameYear, gameMonth))
        {
            gameDay = 1;
            gameMonth++;
            // Check if the month exceeds 12
            if (gameMonth > 12)
            {
                gameMonth = 1;
                gameYear++;
            }
        }
    }
}
