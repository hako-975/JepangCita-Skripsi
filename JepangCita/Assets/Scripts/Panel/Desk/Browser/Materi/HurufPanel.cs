using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;

public class HurufPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private AudioMixerGroup soundMixer;
    [SerializeField]
    private AudioClip clipPositive;

    [SerializeField]
    private GameObject previewHurufPanelPrefabs;

    private MateriPanel materiPanel;

    [Header("Hiragana")]
    [SerializeField]
    private Button[] hurufHiraganaButtons;

    private string[] hurufHiraganaStrings;

    [SerializeField]
    private VideoClip[] hurufHiraganaVideoClip;

    [SerializeField]
    private Button btnMenuHiragana;

    [Header("Katakana")]
    [SerializeField]
    private Button[] hurufKatakanaButtons;

    private string[] hurufKatakanaStrings;

    [SerializeField]
    private VideoClip[] hurufKatakanaVideoClip;

    [SerializeField]
    private Button btnMenuKatakana;

    private Vector2 scrollPosition;
    
    private PreviewHurufPanel previewHurufPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        materiPanel = GetComponent<MateriPanel>();

        hurufHiraganaStrings = new string[]
        {
            "Huruf Hiragana - (a) - あ", "Huruf Hiragana - (i) - い", "Huruf Hiragana - (u) - う", "Huruf Hiragana - (e) - え", "Huruf Hiragana - (o) - お", "Huruf Hiragana - (ka) - か", "Huruf Hiragana - (ki) - き", "Huruf Hiragana - (ku) - く", "Huruf Hiragana - (ke) - け", "Huruf Hiragana - (ko) - こ", "Huruf Hiragana - (sa) - さ", "Huruf Hiragana - (shi) - し", "Huruf Hiragana - (su) - す", "Huruf Hiragana - (se) - せ", "Huruf Hiragana - (so) - そ", "Huruf Hiragana - (ta) - た", "Huruf Hiragana - (chi) - ち", "Huruf Hiragana - (tsu) - つ", "Huruf Hiragana - (te) - て", "Huruf Hiragana - (to) - と", "Huruf Hiragana - (na) - な", "Huruf Hiragana - (ni) - に", "Huruf Hiragana - (nu) - ぬ", "Huruf Hiragana - (ne) - ね", "Huruf Hiragana - (no) - の", "Huruf Hiragana - (ha) - は", "Huruf Hiragana - (hi) - ひ", "Huruf Hiragana - (fu) - ふ", "Huruf Hiragana - (he) - へ", "Huruf Hiragana - (ho) - ほ", "Huruf Hiragana - (ma) - ま", "Huruf Hiragana - (mi) - み", "Huruf Hiragana - (mu) - む", "Huruf Hiragana - (me) - め", "Huruf Hiragana - (mo) - も", "Huruf Hiragana - (ya) - や", "Huruf Hiragana - (yu) - ゆ", "Huruf Hiragana - (yo) - よ", "Huruf Hiragana - (ra) - ら", "Huruf Hiragana - (ri) - り", "Huruf Hiragana - (ru) - る", "Huruf Hiragana - (re) - れ", "Huruf Hiragana - (ro) - ろ", "Huruf Hiragana - (wa) - わ", "Huruf Hiragana - (wo) - を", "Huruf Hiragana - (n) - ん"
        };

        hurufKatakanaStrings = new string[]
        {
            "Huruf Katakana - (a) - ア", "Huruf Katakana - (i) - イ", "Huruf Katakana - (u) - ウ", "Huruf Katakana - (e) - エ", "Huruf Katakana - (o) - オ", "Huruf Katakana - (ka) - カ", "Huruf Katakana - (ki) - キ", "Huruf Katakana - (ku) - ク", "Huruf Katakana - (ke) - ケ", "Huruf Katakana - (ko) - コ", "Huruf Katakana - (sa) - サ", "Huruf Katakana - (shi) - シ", "Huruf Katakana - (su) - ス", "Huruf Katakana - (se) - セ", "Huruf Katakana - (so) - ソ", "Huruf Katakana - (ta) - タ", "Huruf Katakana - (chi) - チ", "Huruf Katakana - (tsu) - ツ", "Huruf Katakana - (te) - テ", "Huruf Katakana - (to) - ト", "Huruf Katakana - (na) - ナ", "Huruf Katakana - (ni) - ニ", "Huruf Katakana - (nu) - ヌ", "Huruf Katakana - (ne) - ネ", "Huruf Katakana - (no) - ノ", "Huruf Katakana - (ha) - ハ", "Huruf Katakana - (hi) - ヒ", "Huruf Katakana - (fu) - フ", "Huruf Katakana - (he) - ヘ", "Huruf Katakana - (ho) - ホ", "Huruf Katakana - (ma) - マ", "Huruf Katakana - (mi) - ミ", "Huruf Katakana - (mu) - ム", "Huruf Katakana - (me) - メ", "Huruf Katakana - (mo) - モ", "Huruf Katakana - (ya) - ヤ", "Huruf Katakana - (yu) - ユ", "Huruf Katakana - (yo) - ヨ", "Huruf Katakana - (ra) - ラ", "Huruf Katakana - (ri) - リ", "Huruf Katakana - (ru) - ル", "Huruf Katakana - (re) - レ", "Huruf Katakana - (ro) - ロ", "Huruf Katakana - (wa) - ワ", "Huruf Katakana - (wo) - ヲ", "Huruf Katakana - (n) - ン"
        };

        for (int i = 0; i < hurufHiraganaButtons.Length; i++)
        {
            int j = i;
            hurufHiraganaButtons[i].onClick.AddListener(delegate { OpenPreviewHiraganaButton(hurufHiraganaStrings[j], hurufHiraganaVideoClip[j]); });
        }

        for (int i = 0; i < hurufKatakanaButtons.Length; i++)
        {
            int j = i;
            hurufKatakanaButtons[i].onClick.AddListener(delegate { OpenPreviewKatakanaButton(hurufKatakanaStrings[j], hurufKatakanaVideoClip[j]); });
        }

        btnMenuHiragana.onClick.AddListener(OpenMenuHiragana);
        btnMenuKatakana.onClick.AddListener(OpenMenuKatakana);

    }

    private void OpenPreviewHiraganaButton(string paragraph, VideoClip videoClip)
    {
        soundController.PositiveButtonSound(gameObject);

        materiPanel.headingText.text = paragraph;
        materiPanel.previewHurufPanelInstantiate = Instantiate(previewHurufPanelPrefabs, materiPanel.content.transform);
        previewHurufPanel = materiPanel.previewHurufPanelInstantiate.GetComponent<PreviewHurufPanel>();
        previewHurufPanel.videoPlayer.clip = videoClip;
        materiPanel.closeButton.onClick.RemoveAllListeners();
        materiPanel.closeButton.onClick.AddListener(delegate { CloseContentHiraganaButton(previewHurufPanel.gameObject); });
        scrollPosition = materiPanel.scrollViewContent.normalizedPosition;
        CloseAllContent();
    }

    private void OpenPreviewKatakanaButton(string paragraph, VideoClip videoClip)
    {
        soundController.PositiveButtonSound(gameObject);

        materiPanel.headingText.text = paragraph;
        materiPanel.previewHurufPanelInstantiate = Instantiate(previewHurufPanelPrefabs, materiPanel.content.transform);
        previewHurufPanel = materiPanel.previewHurufPanelInstantiate.GetComponent<PreviewHurufPanel>();
        previewHurufPanel.videoPlayer.clip = videoClip;
        materiPanel.closeButton.onClick.RemoveAllListeners();
        materiPanel.closeButton.onClick.AddListener(delegate { CloseContentKatakanaButton(previewHurufPanel.gameObject); });
        scrollPosition = materiPanel.scrollViewContent.normalizedPosition;
        CloseAllContent();
    }

    private void CloseAllContent()
    {
        materiPanel.contentHuruf.SetActive(false);
        materiPanel.contentHiragana.SetActive(false);
        materiPanel.contentKatakana.SetActive(false);
        materiPanel.contentAngka.SetActive(false);
        materiPanel.contentWaktu.SetActive(false);
        materiPanel.contentKataGanti.SetActive(false);
        materiPanel.contentKataBenda.SetActive(false);
        materiPanel.contentKataKerja.SetActive(false);
        materiPanel.contentKataSifat.SetActive(false);
        materiPanel.contentKataKeterangan.SetActive(false);
        materiPanel.contentKataTanya.SetActive(false);
        materiPanel.contentKataHubung.SetActive(false);
        materiPanel.contentKataSeru.SetActive(false);
        materiPanel.contentPerkenalanDiri.SetActive(false);
    }

    private void CloseContentHiraganaButton(GameObject previewHurufPanel)
    {
        CloseAllContent();
        Destroy(previewHurufPanel);
        materiPanel.headingText.text = "Materi - Huruf Hiragana";
        materiPanel.contentHiragana.SetActive(true);
        StartCoroutine(RestoreScroll());
    }

    private void CloseContentKatakanaButton(GameObject previewHurufPanel)
    {
        CloseAllContent();
        Destroy(previewHurufPanel);
        materiPanel.headingText.text = "Materi - Huruf Katakana";
        materiPanel.contentKatakana.SetActive(true);
        StartCoroutine(RestoreScroll());
    }

    private void OpenMenuHiragana()
    {
        soundController.PositiveButtonSound(gameObject);

        CloseAllContent();
        materiPanel.headingText.text = "Materi - Huruf Hiragana";
        materiPanel.contentHiragana.SetActive(true);
    }

    private void OpenMenuKatakana()
    {
        soundController.PositiveButtonSound(gameObject);

        CloseAllContent();
        materiPanel.headingText.text = "Materi - Huruf Katakana";
        materiPanel.contentKatakana.SetActive(true);
    }

    private IEnumerator RestoreScroll()
    {
        materiPanel.closeButton.onClick.RemoveAllListeners();
        materiPanel.closeButton.onClick.AddListener(delegate { materiPanel.BackToMateriPanel(true); });

        yield return null;

        materiPanel.scrollViewContent.normalizedPosition = scrollPosition;
    }

}
