using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailMessage : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI senderMail;
    public TextMeshProUGUI titleMail;
    public TextMeshProUGUI messageMail;
    public TextMeshProUGUI dateMail;

    [Header("Buttons")]
    public Button changePasswordButton;
    public Button editButton;
    public Button deleteButton;
}
