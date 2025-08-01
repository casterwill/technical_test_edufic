using UnityEngine;
using UnityEngine.EventSystems;

public class JawabanDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform originalParent;
    [SerializeField] private string answerText;

    private CanvasGroup canvasGroup;

    void Awake() => canvasGroup = GetComponent<CanvasGroup>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        //transform.SetParent(transform.root); // biar di atas
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetPosition()
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
    }

    public string GetAnswerText()
    {
        return answerText;
    }
}
