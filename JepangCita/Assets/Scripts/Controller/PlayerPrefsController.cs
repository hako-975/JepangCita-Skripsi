using Cinemachine;
using System.Globalization;
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

    [Header("Mission List")]
    public Mission[] missionList;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        float calValueMusic = 0.5f * GetMusicVolume() - 45f;
        musicMixer.SetFloat("volume", calValueMusic);

        if (GetMusicVolume() == 0)
        {
            musicMixer.SetFloat("volume", -100f);
        }

        float calValueSound = 0.5f * GetMusicVolume() - 45f;
        soundMixer.SetFloat("volume", calValueSound);

        if (GetSoundVolume() == 0)
        {
            soundMixer.SetFloat("volume", -100f);
        }

        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        touchSensitivity = FindObjectOfType<CinemachineCoreInput>();

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

    #region Mission
    public int GetMission()
    {
        return PlayerPrefs.GetInt("Mission", 0);
    }

    public void SetMission(int missionTo, SoundController soundController)
    {
        if (GetMission() == missionTo)
        {
            // ubah misi saat ini menjadi misi
            PlayerPrefs.SetInt("Mission", missionTo+1);
            SetUpdateMission(1);
            soundController.MissionSucceedSound(gameObject);
        }
    }

    public int GetUpdateMission()
    {
        return PlayerPrefs.GetInt("UpdateMission");
    }
    
    public int SetUpdateMission(int boolean)
    {
        PlayerPrefs.SetInt("UpdateMission", boolean);
        return GetMission();
    }


    #endregion

    #region Scene
    public string GetLastScene()
    {
        return PlayerPrefs.GetString("LastScene", "Home");
    }

    public string SetLastScene(string lastScene)
    {
        PlayerPrefs.SetString("LastScene", lastScene);
        return GetLastScene();
    }

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

    public void SetPositionRotationCharacter(Vector3 position, Quaternion rotation)
    {
        CultureInfo culture = CultureInfo.InvariantCulture;
        PlayerPrefs.SetString("PositionRotationCharacter", 
            position.x.ToString(culture) + ", " + position.y.ToString(culture) + ", " + position.z.ToString(culture) + "?>?" +
            rotation.x.ToString(culture) + ", " + rotation.y.ToString(culture) + ", " + rotation.z.ToString(culture) + ", " + rotation.w.ToString(culture));
    }

    public string GetPositionRotationCharacter()
    {
        return PlayerPrefs.GetString("PositionRotationCharacter", "0.0, 0.0, 0.0?>?0.0, 0.0, 0.0, 1.0");
    }

    #endregion

    #region Settings
    public int GetMusicVolume()
    {
        return PlayerPrefs.GetInt("MusicVolume", 80);
    }

    public int SetMusicVolume(int musicVolume)
    {
        PlayerPrefs.SetInt("MusicVolume", musicVolume);
        return GetMusicVolume();
    }

    public int GetSoundVolume()
    {
        return PlayerPrefs.GetInt("SoundVolume", 80);
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

    public int GetBoolIsRepeatMusic()
    {
        return PlayerPrefs.GetInt("IsRepeatMusic", 0);
    }

    public int SetBoolIsRepeatMusic(int boolean)
    {
        PlayerPrefs.SetInt("IsRepeatMusic", boolean);
        return GetBoolIsRepeatMusic();
    }
    #endregion

    #region Calendar
    public string localeName = "id-ID";

    public int GetTotalDay()
    {
        return PlayerPrefs.GetInt("TotalDay", 1);
    }

    public int SetTotalDay(int day)
    {
        PlayerPrefs.SetInt("TotalDay", day);
        return GetTotalDay();
    }

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
    public void SetHiraganaScore(int number)
    {
        PlayerPrefs.SetInt("HiraganaScore", number);
    }

    public int GetHiraganaScore()
    {
        return PlayerPrefs.GetInt("HiraganaScore", 0);
    }

    public void SetKatakanaScore(int number)
    {
        PlayerPrefs.SetInt("KatakanaScore", number);
    }

    public int GetKatakanaScore()
    {
        return PlayerPrefs.GetInt("KatakanaScore", 0);
    }

    public void SetAngkaScore(int number)
    {
        PlayerPrefs.SetInt("AngkaScore", number);
    }

    public int GetAngkaScore()
    {
        return PlayerPrefs.GetInt("AngkaScore", 0);
    }

    public void SetWaktuScore(int number)
    {
        PlayerPrefs.SetInt("WaktuScore", number);
    }

    public int GetWaktuScore()
    {
        return PlayerPrefs.GetInt("WaktuScore", 0);
    }

    public void SetKataGantiScore(int number)
    {
        PlayerPrefs.SetInt("KataGantiScore", number);
    }

    public int GetKataGantiScore()
    {
        return PlayerPrefs.GetInt("KataGantiScore", 0);
    }

    public void SetKataBendaScore(int number)
    {
        PlayerPrefs.SetInt("KataBendaScore", number);
    }

    public int GetKataBendaScore()
    {
        return PlayerPrefs.GetInt("KataBendaScore", 0);
    }

    public void SetKataKerjaScore(int number)
    {
        PlayerPrefs.SetInt("KataKerjaScore", number);
    }

    public int GetKataKerjaScore()
    {
        return PlayerPrefs.GetInt("KataKerjaScore", 0);
    }

    public void SetKataSifatScore(int number)
    {
        PlayerPrefs.SetInt("KataSifatScore", number);
    }

    public int GetKataSifatScore()
    {
        return PlayerPrefs.GetInt("KataSifatScore", 0);
    }

    public void SetKataKeteranganScore(int number)
    {
        PlayerPrefs.SetInt("KataKeteranganScore", number);
    }

    public int GetKataKeteranganScore()
    {
        return PlayerPrefs.GetInt("KataKeteranganScore", 0);
    }

    public void SetKataTanyaScore(int number)
    {
        PlayerPrefs.SetInt("KataTanyaScore", number);
    }

    public int GetKataTanyaScore()
    {
        return PlayerPrefs.GetInt("KataTanyaScore", 0);
    }

    public void SetKataHubungScore(int number)
    {
        PlayerPrefs.SetInt("KataHubungScore", number);
    }

    public int GetKataHubungScore()
    {
        return PlayerPrefs.GetInt("KataHubungScore", 0);
    }

    public void SetKataSeruScore(int number)
    {
        PlayerPrefs.SetInt("KataSeruScore", number);
    }

    public int GetKataSeruScore()
    {
        return PlayerPrefs.GetInt("KataSeruScore", 0);
    }

    public void SetPerkenalanDiriScore(int number)
    {
        PlayerPrefs.SetInt("PerkenalanDiriScore", number);
    }

    public int GetPerkenalanDiriScore()
    {
        return PlayerPrefs.GetInt("PerkenalanDiriScore", 0);
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

    #region Attendance
    public void SetAttendance(string day, string date, string attendance)
    {
        // attendance = hadir, Bolos
        PlayerPrefs.SetString("Attendance" + date, day + "?>?" + date + "?>?" + attendance);
    }

    public string GetAttendance(string date)
    {
        return PlayerPrefs.GetString("Attendance" + date, "-" + "?>?" + "-" + "?>?" + "-");
    }
    #endregion

    #region MateriClassroom
    [HideInInspector]
    public string[] listMaterials = new string[] 
    { 
        "Huruf Hiragana",
        "Huruf Hiragana Lanjutan",
        "Ujian - Hiragana",
        "Huruf Katakana",
        "Huruf Katakana Lanjutan",
        "Ujian - Katakana",
        "Angka",
        "Angka Lanjutan",
        "Ujian - Angka",
        "Waktu",
        "Waktu Lanjutan",
        "Ujian - Waktu",
        "Kata Ganti",
        "Kata Ganti Lanjutan",
        "Ujian - Kata Ganti",
        "Kata Benda",
        "Kata Benda Lanjutan",
        "Ujian - Kata Benda",
        "Kata Kerja",
        "Kata Kerja Lanjutan",
        "Ujian - Kata Kerja",
        "Kata Sifat",
        "Kata Sifat Lanjutan",
        "Ujian - Kata Sifat",
        "Kata Keterangan",
        "Ujian - Kata Keterangan",
        "Kata Tanya",
        "Ujian - Kata Tanya",
        "Kata Hubung",
        "Ujian - Kata Hubung",
        "Kata Seru",
        "Ujian - Kata Seru",
        "Perkenalan Diri",
        "Ujian - Perkenalan Diri"
    };

    public void SetCurrentMateri(int indexMateri)
    {
        PlayerPrefs.SetInt("CurrentMateri", indexMateri);
    }

    public int GetCurrentMateri()
    {
        return PlayerPrefs.GetInt("CurrentMateri", 0);
    }
    #endregion

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}
