using System.Collections;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;


    void Awake() => Instance = this;

    public TargetDrop[] targets;
    
    private ChangeSlideManager changeSlideManager;

    [SerializeField] private Animator quizAnimator;

    private void Start()
    {
        ScriptCaching();

        quizAnimator ??= GetComponentInParent<Animator>();
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
            //changeSlideManager.CallAfterQuizIEnum();
            quizAnimator.SetTrigger("RightAnswer");
        }
        else
        {
            Debug.Log("Jawaban salah. Reset.");

            StartCoroutine(StartWrongAnswerIEnum());
            //ClearTargetDrop();
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
                //target.SetCurrentJawaban(null);
            }
        }
    }

    private IEnumerator StartWrongAnswerIEnum()
    {
        foreach (var target in targets)
        {
            target.GetCurrentJawaban().ChangeColorToWrong();
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var target in targets)
        {
            target.GetCurrentJawaban().ResetPosition();
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var target in targets)
        {
            target.GetCurrentJawaban().ChangeColorToNormal();
        }

        foreach (var target in targets)
        {
            target.SetCurrentJawaban(null);
        }
    }
}
