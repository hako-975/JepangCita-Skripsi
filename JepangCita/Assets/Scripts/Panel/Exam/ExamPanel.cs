using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private DetailQuestion detailQuestionPrefabs;
    [SerializeField]
    private ExamQuestion[] listExamQuestions;
    [SerializeField]
    private Button finishButton;

    int score = 0;

    private bool[,] answers;

    private DetailQuestion detailQuestion;

    int row;
    int column;

    // Start is called before the first frame update
    void Start()
    {
        row = listExamQuestions.Length + 1;
        column = 5;
        answers = new bool[row, column];

        nameText.text = "Nama: " + PlayerPrefsController.instance.GetCharacterName();
        startButton.onClick.AddListener(StartButtonClick);
        finishButton.onClick.AddListener(FinishButtonClick);
    }

    private void StartButtonClick()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        finishButton.interactable = true;

        for (int i = 0; i < listExamQuestions.Length; i++)
        {
            detailQuestion = Instantiate(detailQuestionPrefabs, content.transform);

            detailQuestion.questionText.text = listExamQuestions[i].question;

            int j = 0;
            foreach (ExamData examData in listExamQuestions[i].examData)
            {
                detailQuestion.toggleAnswerButton[j].isOn = false;
                detailQuestion.toggleAnswerButton[j].GetComponentInChildren<TextMeshProUGUI>().text = examData.answer;
                int questionIndex = i;
                int answerIndex = j;
                detailQuestion.toggleAnswerButton[j].onValueChanged.AddListener(delegate { CheckAnswer(questionIndex, answerIndex, examData.isRightAnswer); });
                j++;
            }
        }
    }

    private void CheckAnswer(int questionIndex, int answerIndex, bool answer)
    {
        for (int i = 0; i < column; i++)
        {
            answers[questionIndex, i] = false;
        }

        if (answer)
        {
            answers[questionIndex, answerIndex] = true;
        }
    }

    private void FinishButtonClick()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (answers[i, j])
                {
                    score += 20;
                }
            }
        }
        Debug.Log("Score: " + score);
    }
}
