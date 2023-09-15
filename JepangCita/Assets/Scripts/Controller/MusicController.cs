using UnityEngine;

public class MusicController : MonoBehaviour
{
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
}
