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

    [Header("Buttons")]
    [SerializeField]
    private Button btnHuruf;
    [SerializeField]
    private Button btnAngka;
    [SerializeField]
    private Button btnKataBenda;
    [SerializeField]
    private Button btnKataKerja;
    [SerializeField]
    private Button btnKataSifat;
    [SerializeField]
    private Button btnPerkenalanDiri;

    // Start is called before the first frame update
    void Start()
    {
        BackToMateriPanel();
        btnHuruf.onClick.AddListener(delegate { OpenPanel(contentHuruf); });
        btnAngka.onClick.AddListener(delegate { OpenPanel(contentAngka); });
    }

    private void OpenPanel(GameObject panel)
    {
        BackToMateriPanel();
        materiPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void BackToMateriPanel()
    {
        contentHuruf.SetActive(false);
        contentHiragana.SetActive(false);
        contentKatakana.SetActive(false);
        contentAngka.SetActive(false);
        materiPanel.SetActive(true);
    }
}
