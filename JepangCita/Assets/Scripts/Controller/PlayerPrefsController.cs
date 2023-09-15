using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerPrefsController : MonoBehaviour
{
    #region Singleton
    public static PlayerPrefsController instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer soundMixer;

    CinemachineFreeLook cinemachineFreeLook;
    CinemachineCoreInput touchSensitivity;
    
    // Start is called before the first frame update
    void Start()
    {
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        touchSensitivity = FindObjectOfType<CinemachineCoreInput>();
        
        float calValueMusic = -50 + GetMusicVolume() / 2;
        musicMixer.SetFloat("volume", calValueMusic);
        
        float calValueSound = -50 + GetSoundVolume() / 2;
        soundMixer.SetFloat("volume", calValueSound);

        if (cinemachineFreeLook)
        {
            cinemachineFreeLook.m_CommonLens = true;
            cinemachineFreeLook.m_Lens.FieldOfView = GetZoomCamera();
        }

        if (touchSensitivity)
        {
            touchSensitivity.touchSensitivity = GetSensitivityCamera();
        }
    }

    #region Scene
    public string GetNextScene()
    {
        return PlayerPrefs.GetString("NextScene", "MainMenu");
    }

    public void SetNextScene(string nameScene)
    {
        Time.timeScale = 1;

        PlayerPrefs.SetString("NextScene", nameScene);
        SceneManager.LoadScene("Loading");
    }
    #endregion

    #region Settings
    public int GetMusicVolume()
    {
        return PlayerPrefs.GetInt("MusicVolume", 100);
    }

    public int SetMusicVolume(int musicVolume)
    {
        PlayerPrefs.SetInt("MusicVolume", musicVolume);
        return GetMusicVolume();
    }

    public int GetSoundVolume()
    {
        return PlayerPrefs.GetInt("SoundVolume", 100);
    }

    public int SetSoundVolume(int soundEffectVolume)
    {
        PlayerPrefs.SetInt("SoundVolume", soundEffectVolume);
        return GetSoundVolume();
    }

    public int GetSensitivityCamera()
    {
        return PlayerPrefs.GetInt("SensitivityCamera", 50);
    }

    public int SetSensitivityCamera(int sensitivityCamera)
    {
        PlayerPrefs.SetInt("SensitivityCamera", sensitivityCamera);
        return GetSensitivityCamera();
    }

    public int GetZoomCamera()
    {
        return PlayerPrefs.GetInt("ZoomCamera", 50);
    }

    public int SetZoomCamera(int zoomCamera)
    {
        PlayerPrefs.SetInt("ZoomCamera", zoomCamera);
        return GetZoomCamera();
    }
    #endregion

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}
