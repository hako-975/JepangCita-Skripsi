using UnityEngine;
using UnityEngine.UI;

public class MateriPanel : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private GameObject materiPanel;
    [SerializeField]
    private GameObject hurufPanel;

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
        btnHuruf.onClick.AddListener(delegate { OpenPanel(hurufPanel); });
    }

    private void OpenPanel(GameObject panel)
    {
        CloseAllPanel();
        materiPanel.SetActive(false);
        panel.SetActive(true);
    }


    private void CloseAllPanel()
    {
        hurufPanel.SetActive(false);
        materiPanel.SetActive(true);
    }
}
