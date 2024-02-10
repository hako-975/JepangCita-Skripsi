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

    #region Character
    public void SetCharacterName(string name)
    {
        PlayerPrefs.SetString("CharacterName", name);
    }

    public string GetCharacterName()
    {
        return PlayerPrefs.GetString("CharacterName", "Nama");
    }

    public void SetCharacterSelection(string character)
    {
        PlayerPrefs.SetString("CharacterSelection", character);
    }

    public string GetCharacterSelection()
    {
        return PlayerPrefs.GetString("CharacterSelection");
    }

    public bool IsHasCharacterSelection()
    {
        return PlayerPrefs.HasKey("CharacterSelection");
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

    #region Calendar
    public string localeName = "id-ID";

    public int GetDateDay()
    {
        return PlayerPrefs.GetInt("DateDay", 1);
    }

    public int SetDateDay(int date)
    {
        PlayerPrefs.SetInt("DateDay", date);
        return GetDateDay();
    }

    public int GetDateMonth()
    {
        return PlayerPrefs.GetInt("DateMonth", 1);
    }

    public int SetDateMonth(int date)
    {
        PlayerPrefs.SetInt("DateMonth", date);
        return GetDateMonth();
    }

    public int SetDateYear(int date)
    {
        PlayerPrefs.SetInt("DateYear", date);
        return GetDateYear();
    }

    public int GetDateYear()
    {
        return PlayerPrefs.GetInt("DateYear", 2023);
    }

   
    public int GetHour()
    {
        return PlayerPrefs.GetInt("Hour", 6);
    }

    public int SetHour(int hour)
    {
        PlayerPrefs.SetInt("Hour", hour);
        return GetHour();
    }

    public int GetMinute()
    {
        return PlayerPrefs.GetInt("Minute", 0);
    }

    public int SetMinute(int minute)
    {
        PlayerPrefs.SetInt("Minute", minute);
        return GetMinute();
    }
    #endregion

    #region JepangCitaAccount
    public void SetCredentialJepangCita(int boolean)
    {
        PlayerPrefs.SetInt("Credential", boolean);
    }

    public int GetCredentialJepangCita()
    {
        return PlayerPrefs.GetInt("Credential", 0);
    }

    public void SetFullnameJepangCita(string name)
    {
        PlayerPrefs.SetString("Fullname", name);
    }

    public string GetFullnameJepangCita()
    {
        return PlayerPrefs.GetString("Fullname", "Nama");
    }

    public void SetEmailJepangCita(string email)
    {
        PlayerPrefs.SetString("Email", email);
    }

    public string GetEmailJepangCita()
    {
        return PlayerPrefs.GetString("Email", "member@jepangcita.com");
    }

    public void SetPasswordJepangCita(string password)
    {
        PlayerPrefs.SetString("Password", password);
    }

    public string GetPasswordJepangCita()
    {
        return PlayerPrefs.GetString("Password", "123456");
    }
    #endregion

    #region JepangCitaDashboard
    public void SetMateriNumberLearned(int number)
    {
        PlayerPrefs.SetInt("MateriNumberLearned", number);
    }

    public int GetMateriNumberLearned()
    {
        return PlayerPrefs.GetInt("MateriNumberLearned", 0);
    }

    public void SetLatestScore(int number)
    {
        PlayerPrefs.SetInt("LatestScore", number);
    }

    public int GetLatestScore()
    {
        return PlayerPrefs.GetInt("LatestScore", 0);
    }
    #endregion

    #region SentMail
    public int IdMailGenerator()
    {
        PlayerPrefs.SetInt("IdMail", PlayerPrefs.GetInt("IdMail", 0) + 1);
        return PlayerPrefs.GetInt("IdMail");
    }

    public int GetCountSentMail()
    {
        return PlayerPrefs.GetInt("IdMail");
    }

    public void SetSentMail(string to, string subject, string message, string date)
    {
        // kepada, subjek, pesan, tanggal
        PlayerPrefs.SetString("SentMail" + IdMailGenerator(), to + "?>?" + subject + "?>?" + message + "?>?" + date);
    }

    public string GetSentMail(int id)
    {
        return PlayerPrefs.GetString("SentMail" + id);
    }

    public void DeleteSentMail(int id)
    {
        PlayerPrefs.SetString("SentMail" + id, "deleted" + "?>?" + "deleted" + "?>?" + "deleted" + "?>?" + "deleted");
    }
    #endregion

    #region InboxMail
    public int IdInboxMailGenerator()
    {
        PlayerPrefs.SetInt("IdInboxMail", PlayerPrefs.GetInt("IdInboxMail", 0) + 1);
        return PlayerPrefs.GetInt("IdInboxMail");
    }

    public int GetCountInboxMail()
    {
        return PlayerPrefs.GetInt("IdInboxMail");
    }

    public void SetInboxMail(string to, string subject, string message, string date, bool isChangePassword)
    {
        PlayerPrefs.SetString("InboxMail" + IdInboxMailGenerator(), to + "?>?" + subject + "?>?" + message + "?>?" + date + "?>?" + isChangePassword);
    }

    public string GetInboxMail(int id)
    {
        return PlayerPrefs.GetString("InboxMail" + id);
    }

    public void DeleteInboxMail(int id)
    {
        PlayerPrefs.SetString("InboxMail" + id, "deleted" + "?>?" + "deleted" + "?>?" + "deleted" + "?>?" + "deleted");
    }
    #endregion

    #region DraftMail
    public int IdDraftMailGenerator()
    {
        PlayerPrefs.SetInt("idDraftMail", PlayerPrefs.GetInt("idDraftMail", 0) + 1);
        return PlayerPrefs.GetInt("idDraftMail");
    }

    public int GetCountDraftMail()
    {
        return PlayerPrefs.GetInt("idDraftMail");
    }

    public void SetDraftMail(string to, string subject, string message, string date)
    {
        // kepada, subjek, pesan, tanggal
        PlayerPrefs.SetString("DraftMail" + IdDraftMailGenerator(), to + "?>?" + subject + "?>?" + message + "?>?" + date);
    }

    public string GetDraftMail(int id)
    {
        return PlayerPrefs.GetString("DraftMail" + id);
    }

    public void DeleteDraftMail(int id)
    {
        PlayerPrefs.SetString("DraftMail" + id, "deleted" + "?>?" + "deleted" + "?>?" + "deleted" + "?>?" + "deleted");
    }
    #endregion

    #region TrashMail
    public int IdTrashMailGenerator()
    {
        PlayerPrefs.SetInt("idTrashMail", PlayerPrefs.GetInt("idTrashMail", 0) + 1);
        return PlayerPrefs.GetInt("idTrashMail");
    }

    public int GetCountTrashMail()
    {
        return PlayerPrefs.GetInt("idTrashMail");
    }

    public void SetTrashMail(string to, string subject, string message, string date)
    {
        // kepada, subjek, pesan, tanggal
        PlayerPrefs.SetString("TrashMail" + IdTrashMailGenerator(), to + "?>?" + subject + "?>?" + message + "?>?" + date);
    }

    public string GetTrashMail(int id)
    {
        return PlayerPrefs.GetString("TrashMail" + id);
    }

    public void DeleteTrashMail(int id)
    {
        PlayerPrefs.SetString("TrashMail" + id, "deleted" + "?>?" + "deleted" + "?>?" + "deleted" + "?>?" + "deleted");
    }
    #endregion



    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}
