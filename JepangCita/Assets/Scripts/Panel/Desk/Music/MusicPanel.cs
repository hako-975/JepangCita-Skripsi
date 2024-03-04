using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private GameObject contentListMusic;

    [SerializeField]
    private GameObject buttonMusicPrefabs;

    [SerializeField]
    private MusicController musicController;

    [SerializeField]
    private Sprite playIcon;

    [SerializeField]
    private Sprite pauseIcon;

    private AudioSource audioSource;

    [Header("Buttons")]
    [SerializeField]
    private Button backwardStepButton;
    [SerializeField]
    private Button playPauseButton;
    [SerializeField]
    private Button forwardStepButton;
    [SerializeField]
    private Button repeatButton;
    [SerializeField]
    private Button shuffleButton;

    [Header("Controls")]
    [SerializeField]
    private TextMeshProUGUI titleMusic;
    [SerializeField]
    private Slider sliderMusic;
    [SerializeField]
    private TextMeshProUGUI currentTime;
    [SerializeField]
    private TextMeshProUGUI lengthTime;
    [SerializeField]
    private Slider volumeMusic;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = musicController.GetComponent<AudioSource>();

        isPlaying = audioSource.isPlaying;
        UpdateUI();

        ShowListMusic();

        backwardStepButton.onClick.AddListener(PreviousMusic);
        playPauseButton.onClick.AddListener(PlayPauseMusic);
        forwardStepButton.onClick.AddListener(NextMusic);
        repeatButton.onClick.AddListener(ToggleRepeat);
        shuffleButton.onClick.AddListener(ToggleShuffle);

        sliderMusic.onValueChanged.AddListener(OnSliderMusicChanged);
        volumeMusic.onValueChanged.AddListener(SetMusicVolume);
    }

    void Update()
    {
        volumeMusic.value = PlayerPrefsController.instance.GetMusicVolume();
        sliderMusic.value = audioSource.time;
        currentTime.text = FormatTime(audioSource.time);

        if (musicController.updateUI)
        {
            UpdateUI();
            musicController.updateUI = false;
        }
    }

    private void ShowListMusic()
    {
        foreach (Transform child in contentListMusic.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < musicController.listMusic.Length; i++)
        {
            GameObject musicInstantiate = Instantiate(buttonMusicPrefabs, contentListMusic.transform);
            int index = i;
            musicInstantiate.GetComponent<Button>().onClick.AddListener(delegate { PlayMusic(musicController.listMusic[index], index); });
            musicInstantiate.GetComponent<ButtonMusic>().titleMusic.text = musicController.listMusic[index].name;
        }
    }

    private void OnSliderMusicChanged(float value)
    {
        audioSource.time = value;
    }

    private void PlayMusic(AudioClip music, int currentIndex)
    {
        musicController.musicIndex = currentIndex;
        audioSource.time = 0f;
        audioSource.clip = music;
        audioSource.Play();
        isPlaying = true;
        UpdateUI();
    }

    private void PreviousMusic()
    {
        soundController.PositiveButtonSound(gameObject);

        if (musicController.musicIndex > 0)
        {
            PlayMusic(musicController.listMusic[musicController.musicIndex - 1], musicController.musicIndex - 1);
        }
        else
        {
            PlayMusic(musicController.listMusic[musicController.listMusic.Length - 1], musicController.listMusic.Length - 1);
        }
    }

    private void UpdateUI()
    {
        if (isPlaying)
        {
            playPauseButton.GetComponent<Image>().sprite = pauseIcon;
        }
        else
        {
            playPauseButton.GetComponent<Image>().sprite = playIcon;
        }

        if (PlayerPrefsController.instance.GetBoolIsRepeatMusic() == 0)
        {
            repeatButton.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            repeatButton.GetComponent<Image>().color = Color.white;
        }

        titleMusic.text = audioSource.clip.name;
        sliderMusic.maxValue = audioSource.clip.length;
        lengthTime.text = FormatTime(audioSource.clip.length);
    }

    private void PlayPauseMusic()
    {
        soundController.PositiveButtonSound(gameObject);

        if (isPlaying)
        {
            playPauseButton.GetComponent<Image>().sprite = playIcon;
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            playPauseButton.GetComponent<Image>().sprite = pauseIcon;
            audioSource.Play();
            isPlaying = true;
        }
    }

    private void NextMusic()
    {
        soundController.PositiveButtonSound(gameObject);

        if (musicController.musicIndex == musicController.listMusic.Length - 1)
        {
            PlayMusic(musicController.listMusic[0], 0);
        }
        else
        {
            PlayMusic(musicController.listMusic[musicController.musicIndex + 1], musicController.musicIndex + 1);
        }
    }

    private void ToggleRepeat()
    {
        soundController.PositiveButtonSound(gameObject);

        if (PlayerPrefsController.instance.GetBoolIsRepeatMusic() == 1)
        {
            PlayerPrefsController.instance.SetBoolIsRepeatMusic(0);
            repeatButton.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            PlayerPrefsController.instance.SetBoolIsRepeatMusic(1);
            repeatButton.GetComponent<Image>().color = Color.white;
        }
    }

    private void ToggleShuffle()
    {
        soundController.PositiveButtonSound(gameObject);

        for (int i = musicController.listMusic.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioClip temp = musicController.listMusic[i];
            musicController.listMusic[i] = musicController.listMusic[randomIndex];
            musicController.listMusic[randomIndex] = temp;
        }

        for (int i = 0; i < musicController.listMusic.Length; i++)
        {
            if (musicController.listMusic[i] == audioSource.clip)
            {
                musicController.musicIndex = i;
                break;
            }
        }

        ShowListMusic();
    }

    private void SetMusicVolume(float musicVolume)
    {
        float calValue = 0.5f * musicVolume - 45f;
        musicController.musicMixer.SetFloat("volume", calValue);
        PlayerPrefsController.instance.SetMusicVolume((int)musicVolume);
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
