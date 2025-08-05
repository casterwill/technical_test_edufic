using DG.Tweening;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        //transform.localPosition = Vector3.zero;

        transform.DOKill();
        transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad);
    }

    public string GetAnswerText()
    {
        return answerText;
    }

    public void ChangeColorToWrong()
    {
        UnityEngine.Color color;

        if (ColorUtility.TryParseHtmlString("#FB3141", out color))
        {
            GetComponent<Image>().color = color;
        }
    }


    public void ChangeColorToNormal()
    {
        UnityEngine.Color color;

        if (ColorUtility.TryParseHtmlString("#FFFFFF", out color))
        {
            GetComponent<Image>().color = color;
        }
    }
}
