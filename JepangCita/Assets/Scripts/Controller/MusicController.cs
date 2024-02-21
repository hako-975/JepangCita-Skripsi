using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioClip[] listMusic;

    private AudioSource audioSource;

    private int musicVolume;

    public AudioMixer musicMixer;
    
    [HideInInspector]
    public int musicIndex = 0;

    [HideInInspector]
    public bool updateUI = false;

    void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MusicController");

        if (musicObj.Length > 1)
        {
            if (musicObj[0].GetComponent<AudioSource>().clip == musicObj[1].GetComponent<AudioSource>().clip)
            {
                Destroy(musicObj[1]);
            }
            else
            {
                Destroy(musicObj[0]);
            }
        }

        DontDestroyOnLoad(gameObject);

        GetComponent<AudioSource>().volume = 1f;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        musicVolume = PlayerPrefsController.instance.GetMusicVolume();
        float calValue = -80 + musicVolume / 1.25f;
        musicMixer.SetFloat("volume", calValue);

        if (!audioSource.isPlaying)
        {
            if (audioSource.clip != null)
            {
                for (int i = 0; i < listMusic.Length; i++)
                {
                    if (audioSource.clip == listMusic[i])
                    {
                        PlayMusic(listMusic[i], i);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (PlayerPrefsController.instance.GetBoolIsRepeatMusic() == 0)
        {
            if (audioSource.time >= audioSource.clip.length - 1f)
            {
                NextMusic();
            }
        }
    }

    private void PlayMusic(AudioClip music, int currentIndex)
    {
        musicIndex = currentIndex;
        audioSource.time = 0f;
        audioSource.clip = music;
        audioSource.Play();
        updateUI = true;
    }

    private void NextMusic()
    {

        if (musicIndex == listMusic.Length - 1)
        {
            PlayMusic(listMusic[0], 0);
        }
        else
        {
            PlayMusic(listMusic[musicIndex + 1], musicIndex + 1);
        }
    }
}
