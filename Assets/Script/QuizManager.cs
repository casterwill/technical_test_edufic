using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;


    void Awake() => Instance = this;

    public TargetDrop[] targets;
    
    private ChangeSlideManager changeSlideManager;

    private void Start()
    {
        ScriptCaching();
    }

    private void ScriptCaching()
    {
        changeSlideManager = ChangeSlideManager.Singleton;
    }

    public void CheckAllAnswered()
    {
        foreach (var target in targets)
        {
            if (target.GetCurrentJawaban() == null)
                return; // belum semua terisi
        }

        bool allCorrect = true;
        foreach (var target in targets)
        {
            if (target.GetCurrentJawaban().GetAnswerText() != target.GetExpectedAnswer())
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            StartCoroutine(changeSlideManager.AfterQuizCorrect());
        }
        else
        {
            Debug.Log("Jawaban salah. Reset.");
            ClearTargetDrop();
        }
    }

    private void ClearTargetDrop()
    {
        foreach (var target in targets)
        {
            if (target.GetCurrentJawaban() != null)
            {
                // Kembalikan jawaban ke asal dan bersihkan referensi target
                target.GetCurrentJawaban().ResetPosition();
                target.SetCurrentJawaban(null);
            }
        }
    }
}
