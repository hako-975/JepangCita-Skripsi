using UnityEngine;
using UnityEngine.UI;

public class MateriPanel : MonoBehaviour
{
    [Header("Panels")]
    public GameObject materiPanel;
    [SerializeField]
    private GameObject contentHuruf;
    [SerializeField]
    private GameObject contentHiragana;
    [SerializeField]
    private GameObject contentKatakana;
    [SerializeField]
    private GameObject contentAngka;
    [SerializeField]
    private GameObject contentWaktu;
    [Header("Panels Kata")]
    [SerializeField]
    private GameObject contentKataGanti;
    [SerializeField]
    private GameObject contentKataBenda;
    [SerializeField]
    private GameObject contentKataKerja;
    [SerializeField]
    private GameObject contentKataSifat;
    [SerializeField]
    private GameObject contentKataKeterangan;
    [SerializeField]
    private GameObject contentKataTanya;
    [SerializeField]
    private GameObject contentKataHubung;
    [SerializeField]
    private GameObject contentKataSeru;
    [SerializeField]
    private GameObject contentPerkenalanDiri;

    [Header("Buttons")]
    [SerializeField]
    private Button btnHuruf;
    [SerializeField]
    private Button btnAngka;
    [SerializeField]
    private Button btnWaktu;
    [Header("Buttons Kata")]
    [SerializeField]
    private Button btnKataGanti;
    [SerializeField]
    private Button btnKataBenda;
    [SerializeField]
    private Button btnKataKerja;
    [SerializeField]
    private Button btnKataSifat;
    [SerializeField]
    private Button btnKataKeterangan;
    [SerializeField]
    private Button btnKataTanya;
    [SerializeField]
    private Button btnKataHubung;
    [SerializeField]
    private Button btnKataSeru;
    [SerializeField]
    private Button btnPerkenalanDiri;

    // Start is called before the first frame update
    void Start()
    {
        BackToMateriPanel();
        btnHuruf.onClick.AddListener(delegate { OpenPanel(contentHuruf); });
        btnAngka.onClick.AddListener(delegate { OpenPanel(contentAngka); });
        btnWaktu.onClick.AddListener(delegate { OpenPanel(contentWaktu); });
        btnKataGanti.onClick.AddListener(delegate { OpenPanel(contentKataGanti); });
        btnKataBenda.onClick.AddListener(delegate { OpenPanel(contentKataBenda); });
        btnKataKerja.onClick.AddListener(delegate { OpenPanel(contentKataKerja); });
        btnKataSifat.onClick.AddListener(delegate { OpenPanel(contentKataSifat); });
        btnKataKeterangan.onClick.AddListener(delegate { OpenPanel(contentKataKeterangan); });
        btnKataTanya.onClick.AddListener(delegate { OpenPanel(contentKataTanya); });
        btnKataHubung.onClick.AddListener(delegate { OpenPanel(contentKataHubung); });
        btnKataSeru.onClick.AddListener(delegate { OpenPanel(contentKataSeru); });
        btnPerkenalanDiri.onClick.AddListener(delegate { OpenPanel(contentPerkenalanDiri); });
    }

    private void OpenPanel(GameObject panel)
    {
        BackToMateriPanel();
        materiPanel.SetActive(false);
        panel.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(panel.GetComponent<RectTransform>());
    }

    public void BackToMateriPanel()
    {
        contentHuruf.SetActive(false);
        contentHiragana.SetActive(false);
        contentKatakana.SetActive(false);
        contentAngka.SetActive(false);
        contentWaktu.SetActive(false);
        materiPanel.SetActive(true);
    }
}
