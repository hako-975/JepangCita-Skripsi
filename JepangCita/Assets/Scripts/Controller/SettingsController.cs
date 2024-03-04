using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

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

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        sensitivityCameraSlider.onValueChanged.AddListener(SetSensitivityCamera);
        zoomCameraSlider.onValueChanged.AddListener(SetZoomCamera);
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

    private void SetMusicVolume(float musicVolume)
    {
        soundController.PositiveButtonSound(gameObject);

        float calValue = 0.5f * musicVolume - 45f;
        musicMixer.SetFloat("volume", calValue);
        PlayerPrefsController.instance.SetMusicVolume((int)musicVolume);

        if (musicVolume == 0)
        {
            musicMixer.SetFloat("volume", -100f);
        }
    }

    private void SetSoundVolume(float soundVolume)
    {
        soundController.PositiveButtonSound(gameObject);

        float calValue = 0.5f * soundVolume - 45f;
        soundMixer.SetFloat("volume", calValue);
        PlayerPrefsController.instance.SetSoundVolume((int)soundVolume);
        if (soundVolume == 0)
        {
            soundMixer.SetFloat("volume", -100f);
        }
    }

    private void SetSensitivityCamera(float sensitivityCamera)
    {
        soundController.PositiveButtonSound(gameObject);

        touchSensitivity.touchSensitivity = sensitivityCamera;
        PlayerPrefsController.instance.SetSensitivityCamera((int)sensitivityCamera);
    }

    private void SetZoomCamera(float zoomCamera)
    {
        soundController.PositiveButtonSound(gameObject);

        cinemachineFreeLook.m_CommonLens = true;
        cinemachineFreeLook.m_Lens.FieldOfView = zoomCamera;
        PlayerPrefsController.instance.SetZoomCamera((int)zoomCamera);
    }

    private void ResetButton()
    {
        soundController.ResetButtonSound(gameObject);

        PlayerPrefsController.instance.DeleteKey("MusicVolume");
        PlayerPrefsController.instance.DeleteKey("SoundVolume");
        PlayerPrefsController.instance.DeleteKey("SensitivityCamera");
        PlayerPrefsController.instance.DeleteKey("ZoomCamera");
    }
}
