using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicPanel : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = musicController.GetComponent<AudioSource>();

        for (int i = 0; i < listMusic.Length; i++)
        {
            GameObject musicInstantiate = Instantiate(buttonMusicPrefabs, contentListMusic.transform);
            int index = i;
            musicInstantiate.GetComponent<Button>().onClick.AddListener(delegate { PlayMusic(listMusic[index]); });
            musicInstantiate.GetComponent<ButtonMusic>().titleMusic.text = listMusic[index].name;
        }

        backwardStepButton.onClick.AddListener(PreviousMusic);
        playPauseButton.onClick.AddListener(PlayPauseMusic);
        forwardStepButton.onClick.AddListener(NextMusic);
        repeatButton.onClick.AddListener(ToggleRepeat);
        shuffleButton.onClick.AddListener(ToggleShuffle);
    }

    void Update()
    {
        volumeMusic.value = PlayerPrefsController.instance.GetMusicVolume();
        sliderMusic.value = audioSource.time;
        currentTime.text = FormatTime(audioSource.time);
    }

    private void PlayMusic(AudioClip music)
    {
        titleMusic.text = music.name;
        sliderMusic.maxValue = music.length;
        lengthTime.text = FormatTime(music.length);
        audioSource.clip = music;
        audioSource.Play();
        isPlaying = true;
    }

    private void PreviousMusic()
    {
        // Implement logic for playing previous music
    }

    private void PlayPauseMusic()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            audioSource.Play();
            isPlaying = true;
        }
    }

    private void NextMusic()
    {
        // Implement logic for playing next music
    }

    private void ToggleRepeat()
    {
        // Implement logic for toggling repeat mode
    }

    private void ToggleShuffle()
    {
        // Implement logic for toggling shuffle mode
    }

    public void SetMusicVolume(float musicVolume)
    {
        float calValue = -50 + musicVolume / 2;
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
