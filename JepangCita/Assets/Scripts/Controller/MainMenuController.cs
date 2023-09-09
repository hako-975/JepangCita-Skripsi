using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayButton);
    }

    private void PlayButton()
    {
        PlayerPrefsController.instance.SetNextScene("Gameplay");
    }
}
