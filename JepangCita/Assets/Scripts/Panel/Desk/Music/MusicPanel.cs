using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicPanel : MonoBehaviour
{
    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private AudioClip[] listMusic;

    [SerializeField]
    private GameObject contentListMusic;

    [SerializeField]
    private GameObject buttonMusicPrefabs;

    [SerializeField]
    private AudioMixer musicMixer;

    [SerializeField]
    private MusicController musicController;

    [SerializeField]
    private Sprite playIcon;

    [SerializeField]
    private Sprite pauseIcon;

    private AudioSource audioSource;

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

    private bool isPlaying = false;

    private bool isRepeat = true;

    private int musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = musicController.GetComponent<AudioSource>();
        isPlaying = audioSource.isPlaying;
        PlayPauseIcon();

        titleMusic.text = audioSource.clip.name;
        sliderMusic.maxValue = audioSource.clip.length;
        lengthTime.text = FormatTime(audioSource.clip.length);

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

        if (isRepeat == false)
        {
            if (audioSource.time >= audioSource.clip.length - 1f)
            {
                NextMusic();
            }
        }
    }

    private void ShowListMusic()
    {
        foreach (Transform child in contentListMusic.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < listMusic.Length; i++)
        {
            GameObject musicInstantiate = Instantiate(buttonMusicPrefabs, contentListMusic.transform);
            int index = i;
            musicInstantiate.GetComponent<Button>().onClick.AddListener(delegate { PlayMusic(listMusic[index], index); });
            musicInstantiate.GetComponent<ButtonMusic>().titleMusic.text = listMusic[index].name;
        }
    }

    private void OnSliderMusicChanged(float value)
    {
        audioSource.time = value;
    }

    private void PlayMusic(AudioClip music, int currentIndex)
    {
        musicIndex = currentIndex;
        audioSource.time = 0f;
        titleMusic.text = music.name;
        sliderMusic.maxValue = music.length;
        lengthTime.text = FormatTime(music.length);
        audioSource.clip = music;
        audioSource.Play();
        isPlaying = true;
        PlayPauseIcon();
    }

    private void PreviousMusic()
    {
        soundController.PositiveButtonSound(gameObject);

        if (musicIndex > 0)
        {
            PlayMusic(listMusic[musicIndex - 1], musicIndex - 1);
        }
        else
        {
            PlayMusic(listMusic[listMusic.Length - 1], listMusic.Length - 1);
        }
    }

    private void PlayPauseIcon()
    {
        if (isPlaying)
        {
            playPauseButton.GetComponent<Image>().sprite = pauseIcon;
        }
        else
        {
            playPauseButton.GetComponent<Image>().sprite = playIcon;
        }
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

        if (musicIndex == listMusic.Length - 1)
        {
            PlayMusic(listMusic[0], 0);
        }
        else
        {
            PlayMusic(listMusic[musicIndex + 1], musicIndex + 1);
        }
    }

    private void ToggleRepeat()
    {
        soundController.PositiveButtonSound(gameObject);

        if (isRepeat)
        {
            isRepeat = false;
            repeatButton.GetComponent<Image>().color = Color.grey;
        }
        else
        {
            repeatButton.GetComponent<Image>().color = Color.white;
            isRepeat = true;
        }
    }

    private void ToggleShuffle()
    {
        soundController.PositiveButtonSound(gameObject);

        for (int i = listMusic.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            AudioClip temp = listMusic[i];
            listMusic[i] = listMusic[randomIndex];
            listMusic[randomIndex] = temp;
        }

        for (int i = 0; i < listMusic.Length; i++)
        {
            if (listMusic[i] == audioSource.clip)
            {
                musicIndex = i;
                break;
            }
        }

        ShowListMusic();
    }

    private void SetMusicVolume(float musicVolume)
    {
        float calValue = -80 + musicVolume / 1.25f;
        musicMixer.SetFloat("volume", calValue);
        PlayerPrefsController.instance.SetMusicVolume((int)musicVolume);
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
