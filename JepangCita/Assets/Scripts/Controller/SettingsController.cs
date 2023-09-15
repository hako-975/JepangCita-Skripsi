using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Button resetButton;

    [SerializeField]
    private AudioMixer musicMixer;
    
    [SerializeField]
    private AudioMixer soundMixer;
    
    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI textValueMusic;
    [SerializeField]
    private TextMeshProUGUI textValueSound;
    [SerializeField]
    private TextMeshProUGUI textValueSensitivity;
    [SerializeField]
    private TextMeshProUGUI textValueZoom;
    
    [Header("Sliders")]
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Slider sensitivityCameraSlider;
    [SerializeField]
    private Slider zoomCameraSlider;

    CinemachineFreeLook cinemachineFreeLook;

    CinemachineCoreInput touchSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        touchSensitivity = FindObjectOfType<CinemachineCoreInput>();

        resetButton.onClick.AddListener(ResetButton);
    }

    // Update is called once per frame
    void Update()
    {
        musicSlider.value = PlayerPrefsController.instance.GetMusicVolume();
        textValueMusic.SetText(PlayerPrefsController.instance.GetMusicVolume() + "%");

        soundSlider.value = PlayerPrefsController.instance.GetSoundVolume();
        textValueSound.SetText(PlayerPrefsController.instance.GetSoundVolume() + "%");

        float sensitivityValue = PlayerPrefsController.instance.GetSensitivityCamera();
        float minSensitivitySliderValue = 1;
        float maxSensitivitySliderValue = 150;
        // Calculate the percentage within the 0 to 100 range
        float percentageSensitivity = Mathf.Clamp(((sensitivityValue - minSensitivitySliderValue) / (maxSensitivitySliderValue - minSensitivitySliderValue)) * 100, 0, 100);
        // Display the percentage in a text element
        string percentageSensitivityText = percentageSensitivity.ToString("0") + "%";
        sensitivityCameraSlider.value = sensitivityValue;
        textValueSensitivity.SetText(percentageSensitivityText);

        float zoomValue = PlayerPrefsController.instance.GetZoomCamera();
        float minZoomSliderValue = 20;
        float maxZoomSliderValue = 100;
        // Calculate the percentage within the 0 to 100 range
        float percentageZoom = Mathf.Clamp(((zoomValue - minZoomSliderValue) / (maxZoomSliderValue - minZoomSliderValue)) * 100, 0, 100);
        // Display the percentage in a text element
        string percentageZoomText = percentageZoom.ToString("0") + "%";
        zoomCameraSlider.value = zoomValue;
        textValueZoom.SetText(percentageZoomText);
    }

    public void SetMusicVolume(float musicVolume)
    {
        float calValue = -50 + musicVolume / 2;
        musicMixer.SetFloat("volume", calValue);
        PlayerPrefsController.instance.SetMusicVolume((int)musicVolume);
    }

    public void SetSoundVolume(float soundVolume)
    {
        float calValue = -50 + soundVolume / 2;
        soundMixer.SetFloat("volume", calValue);
        PlayerPrefsController.instance.SetSoundVolume((int)soundVolume);
    }

    public void SetSensitivityCamera(float sensitivityCamera)
    {
        touchSensitivity.touchSensitivity = sensitivityCamera;
        PlayerPrefsController.instance.SetSensitivityCamera((int)sensitivityCamera);
    }

    public void SetZoomCamera(float zoomCamera)
    {
        cinemachineFreeLook.m_CommonLens = true;
        cinemachineFreeLook.m_Lens.FieldOfView = zoomCamera;
        PlayerPrefsController.instance.SetZoomCamera((int)zoomCamera);
    }

    private void ResetButton()
    {
        PlayerPrefsController.instance.DeleteKey("MusicVolume");
        PlayerPrefsController.instance.DeleteKey("SoundVolume");
        PlayerPrefsController.instance.DeleteKey("SensitivityCamera");
        PlayerPrefsController.instance.DeleteKey("ZoomCamera");
    }

}
