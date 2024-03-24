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

    // Start is called before the first frame update
    void Start()
    {
        RefreshTable();
    }

    private void RefreshTable()
    {
        // Clear existing table rows
        foreach (Transform child in tabelKehadiran.transform)
        {
            if (child.gameObject != tableHead)
            {
                Destroy(child.gameObject);
            }
        }

        DateTime startDate = new DateTime(2023, 1, 1);
        
        DateTime playerDate = new DateTime(PlayerPrefsController.instance.GetDateYear(), PlayerPrefsController.instance.GetDateMonth(), PlayerPrefsController.instance.GetDateDay());

        TimeSpan difference = playerDate - startDate;
        
        for (int i = 0; i < difference.Days+1; i++)
        {
            DateTime date = startDate.AddDays(i);
            string dayName = date.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));

            // Check if it's Monday, Wednesday, or Friday
            if (dayName == "Senin" || dayName == "Rabu" || dayName == "Jumat")
            {
                string formattedDate = date.ToString("dd/MM/yyyy");
                // Instantiate row
                GameObject rowKehadiranInstantiate = Instantiate(kehadiranPrefabs, tabelKehadiran.transform);
                Kehadiran rowKehadiran = rowKehadiranInstantiate.GetComponent<Kehadiran>();
                string[] separatedData = PlayerPrefsController.instance.GetAttendance(formattedDate).Split(new string[] { "?>?" }, StringSplitOptions.None);

                rowKehadiran.hariText.text = separatedData[0];
                rowKehadiran.tanggalText.text = separatedData[1];
                if (separatedData[2] == "Hadir")
                {
                    rowKehadiran.kehadiranImage.sprite = checkIcon;
                    rowKehadiran.kehadiranImage.color = new Color(0.5882352941176471f, 0.788235294117647f, 0.06274509803921569f);
                }
                else if (separatedData[2] == "Bolos")
                {
                    rowKehadiran.kehadiranImage.sprite = crossIcon;
                    rowKehadiran.kehadiranImage.color = new Color(0.9607843137254902f, 0.2627450980392157f, 0.3254901960784314f);
                }
                else if (separatedData[2] == "Terlambat")
                {
                    rowKehadiran.kehadiranImage.sprite = checkIcon;
                    rowKehadiran.kehadiranImage.color = new Color(0.9607843137254902f, 0.2627450980392157f, 0.3254901960784314f);
                }
                else
                {
                    rowKehadiran.kehadiranImage.sprite = transparentIcon;
                }

                rowKehadiran.keteranganText.text = separatedData[2];
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }
}
