using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExamPanel : MonoBehaviour
{
    [SerializeField]
    private ExamList examList;

    private ExamQuestion[] listExamQuestions;

    [SerializeField]
    private TextMeshProUGUI titleExamText;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private DetailQuestion detailQuestionPrefabs;
    [SerializeField]
    private Button finishButton;

    int score = 0;

    private bool[,] answers;

    private DetailQuestion detailQuestion;

    int row;
    int column;

    [SerializeField]
    private ClassroomController classroomController;

    [SerializeField]
    private DateTimeController dateTimeController;

    bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Hiragana")
        {
            listExamQuestions = examList.listHiraganaExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Katakana")
        {
            listExamQuestions = examList.listKatakanaExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Angka")
        {
            listExamQuestions = examList.listAngkaExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Waktu")
        {
            listExamQuestions = examList.listWaktuExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Ganti")
        {
            listExamQuestions = examList.listKataGantiExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Benda")
        {
            listExamQuestions = examList.listKataBendaExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Kerja")
        {
            listExamQuestions = examList.listKataKerjaExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Sifat")
        {
            listExamQuestions = examList.listKataSifatExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Keterangan")
        {
            listExamQuestions = examList.listKataKeteranganExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Tanya")
        {
            listExamQuestions = examList.listKataTanyaExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Hubung")
        {
            listExamQuestions = examList.listKataHubungExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Seru")
        {
            listExamQuestions = examList.listKataSeruExam;
        }
        else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Perkenalan Diri")
        {
            listExamQuestions = examList.listPerkenalanDiriExam;
        }

        row = listExamQuestions.Length + 1;
        column = 5;
        answers = new bool[row, column];

        titleExamText.text = PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()];
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

            detailQuestion.questionText.text = i + 1 + ". " + listExamQuestions[i].question;

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

    public void FinishButtonClick()
    {
        if (isClicked == false)
        {
            isClicked = true;

            int gameDay = PlayerPrefsController.instance.GetDateDay();
            int gameMonth = PlayerPrefsController.instance.GetDateMonth();
            int gameYear = PlayerPrefsController.instance.GetDateYear();

            DateTime date = new DateTime(gameYear, gameMonth, gameDay);

            string dayName = date.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));
            // Check if it's Monday or wednesday
            if (dayName == "Senin" || dayName == "Rabu")
            {
                PlayerPrefsController.instance.SetHour(dateTimeController.gameHour = 12);
                PlayerPrefsController.instance.SetMinute(dateTimeController.gameMinute = 0);
            }
            else if (dayName == "Jumat")
            {
                PlayerPrefsController.instance.SetHour(dateTimeController.gameHour = 16);
                PlayerPrefsController.instance.SetMinute(dateTimeController.gameMinute = 0);
            }

            finishButton.interactable = false;

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

            GetComponent<Animator>().SetTrigger("Hide");

            if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Hiragana")
            {
                PlayerPrefsController.instance.SetHiraganaScore(Mathf.Clamp(score, 0, 100));
                // misi keenam
                PlayerPrefsController.instance.SetMission(5, classroomController.soundController);
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Katakana")
            {
                PlayerPrefsController.instance.SetKatakanaScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Angka")
            {
                PlayerPrefsController.instance.SetAngkaScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Waktu")
            {
                PlayerPrefsController.instance.SetWaktuScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Ganti")
            {
                PlayerPrefsController.instance.SetKataGantiScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Benda")
            {
                PlayerPrefsController.instance.SetKataBendaScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Kerja")
            {
                PlayerPrefsController.instance.SetKataKerjaScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Sifat")
            {
                PlayerPrefsController.instance.SetKataSifatScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Keterangan")
            {
                PlayerPrefsController.instance.SetKataKeteranganScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Tanya")
            {
                PlayerPrefsController.instance.SetKataTanyaScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Hubung")
            {
                PlayerPrefsController.instance.SetKataHubungScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Kata Seru")
            {
                PlayerPrefsController.instance.SetKataSeruScore(Mathf.Clamp(score, 0, 100));
            }
            else if (PlayerPrefsController.instance.listMaterials[PlayerPrefsController.instance.GetCurrentMateri()] == "Ujian - Perkenalan Diri")
            {
                PlayerPrefsController.instance.SetPerkenalanDiriScore(Mathf.Clamp(score, 0, 100));
                // misi ketujuh
                PlayerPrefsController.instance.SetMission(6, classroomController.soundController);
            }
                
            StartCoroutine(classroomController.OpenDialogPanel("Sensei", "Oke みなさん waktu ujian sudah selesai, kalian bisa cek nilainya di situs JepangCita. じゃ、またあした。", true, true));
        }
    }
}
