using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MateriPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    public ScrollRect scrollViewContent;

    [Header("Panels")]
    public GameObject materiPanel;
    public GameObject content;
    public GameObject contentHuruf;
    public GameObject contentHiragana;
    public GameObject contentKatakana;
    public GameObject contentAngka;
    public GameObject contentWaktu;

    [Header("Panels Kata")]
    public GameObject contentKataGanti;
    public GameObject contentKataBenda;
    public GameObject contentKataKerja;
    public GameObject contentKataSifat;
    public GameObject contentKataKeterangan;
    public GameObject contentKataTanya;
    public GameObject contentKataHubung;
    public GameObject contentKataSeru;
    public GameObject contentPerkenalanDiri;

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

    [Header("Heading and Close Button")]
    public TextMeshProUGUI headingText;
    public Button closeButton;

    [HideInInspector]
    public GameObject previewHurufPanelInstantiate;

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
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(delegate { BackToMateriPanel(true); });
    }

    void Update()
    {
        InteractableButton();
    }

    void InteractableButton()
    {
        if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Huruf Hiragana")
        {
            btnHuruf.interactable = false;
            btnAngka.interactable = false;
            btnWaktu.interactable = false;
            btnKataGanti.interactable = false;
            btnKataBenda.interactable = false;
            btnKataKerja.interactable = false;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Huruf Hiragana Lanjutan" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Hiragana" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Huruf Katakana" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Huruf Katakana Lanjutan" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Katakana")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = false;
            btnWaktu.interactable = false;
            btnKataGanti.interactable = false;
            btnKataBenda.interactable = false;
            btnKataKerja.interactable = false;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Angka" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Angka Lanjutan" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Angka")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = false;
            btnKataGanti.interactable = false;
            btnKataBenda.interactable = false;
            btnKataKerja.interactable = false;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Waktu" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Waktu Lanjutan" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Waktu")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = false;
            btnKataBenda.interactable = false;
            btnKataKerja.interactable = false;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Ganti" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Ganti")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = false;
            btnKataKerja.interactable = false;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Benda" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Benda")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = false;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Kerja" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Kerja")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = false;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Sifat" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Sifat")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = true;
            btnKataKeterangan.interactable = false;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Keterangan" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Keterangan")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = true;
            btnKataKeterangan.interactable = true;
            btnKataTanya.interactable = false;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Tanya" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Tanya")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = true;
            btnKataKeterangan.interactable = true;
            btnKataTanya.interactable = true;
            btnKataHubung.interactable = false;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Hubung" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Hubung")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = true;
            btnKataKeterangan.interactable = true;
            btnKataTanya.interactable = true;
            btnKataHubung.interactable = true;
            btnKataSeru.interactable = false;
            btnPerkenalanDiri.interactable = false;
        }
        else if (
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Kata Seru" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Seru")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = true;
            btnKataKeterangan.interactable = true;
            btnKataTanya.interactable = true;
            btnKataHubung.interactable = true;
            btnKataSeru.interactable = true;
            btnPerkenalanDiri.interactable = false;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Perkenalan Diri" ||
            PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Perkenalan Diri")
        {
            btnHuruf.interactable = true;
            btnAngka.interactable = true;
            btnWaktu.interactable = true;
            btnKataGanti.interactable = true;
            btnKataBenda.interactable = true;
            btnKataKerja.interactable = true;
            btnKataSifat.interactable = true;
            btnKataKeterangan.interactable = true;
            btnKataTanya.interactable = true;
            btnKataHubung.interactable = true;
            btnKataSeru.interactable = true;
            btnPerkenalanDiri.interactable = true;
        }
    }

    private void OpenPanel(GameObject panel)
    {
        soundController.PositiveButtonSound(gameObject);
        BackToMateriPanel();
        materiPanel.SetActive(false);
        headingText.text = InsertSpaces(panel.name);
        closeButton.gameObject.SetActive(true);
        panel.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(panel.GetComponent<RectTransform>());
    }

    string InsertSpaces(string input)
    {
        input = input.Replace("Content ", "");

        string result = "Materi - ";

        for (int i = 0; i < input.Length; i++)
        {
            if (i > 0 && char.IsUpper(input[i]))
            {
                result += " ";
            }
            result += input[i];
        }

        return result.Trim();
    }

    public void BackToMateriPanel(bool isSound = false)
    {
        if (isSound)
        {
            soundController.NegativeButtonSound(gameObject);
        }

        contentHuruf.SetActive(false);
        contentHiragana.SetActive(false);
        contentKatakana.SetActive(false);
        contentAngka.SetActive(false);
        contentWaktu.SetActive(false);
        contentKataGanti.SetActive(false);
        contentKataBenda.SetActive(false);
        contentKataKerja.SetActive(false);
        contentKataSifat.SetActive(false);
        contentKataKeterangan.SetActive(false);
        contentKataTanya.SetActive(false);
        contentKataHubung.SetActive(false);
        contentKataSeru.SetActive(false);
        contentPerkenalanDiri.SetActive(false);
        scrollViewContent.normalizedPosition = new Vector2(0, 1);

        headingText.text = "Materi";
        closeButton.gameObject.SetActive(false);

        materiPanel.SetActive(true);
    }
}
