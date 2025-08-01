using UnityEngine;
using UnityEngine.EventSystems;

public class TargetDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private string expectedAnswer;

    
    [SerializeField] private JawabanDrag currentJawaban;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            var dragged = eventData.pointerDrag.GetComponent<JawabanDrag>();
            if (dragged != null)
            {
                dragged.transform.SetParent(transform);
                dragged.transform.localPosition = Vector3.zero;
                currentJawaban = dragged;

                QuizManager.Instance.CheckAllAnswered();
            }
        }
    }

    public void Clear()
    {
        if (currentJawaban != null)
        {
            currentJawaban.ResetPosition();
            currentJawaban = null;
        }
    }

    public JawabanDrag GetCurrentJawaban()
    {
        return currentJawaban;
    }

    public void SetCurrentJawaban(JawabanDrag jawaban)
    {
        currentJawaban = jawaban;
    }

    public string GetExpectedAnswer()
    {
        return expectedAnswer;
    }
}
