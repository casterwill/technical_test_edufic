using DG.Tweening;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JawabanDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform originalParent;
    [SerializeField] private string answerText;

    private Vector3 posisiAwal;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas parentCanvas;

    private TargetDrop targetDrop;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();

        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (targetDrop != null) targetDrop.SetCurrentJawaban(null);
        targetDrop = null;

        transform.SetParent(originalParent);
        posisiAwal = rectTransform.anchoredPosition;
        //transform.SetParent(transform.root); // biar di atas
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        // Cek apakah pointer berada di atas objek yang punya TargetDrop
        targetDrop = null;
        foreach (var result in raycastResults)
        {
            targetDrop = result.gameObject.GetComponent<TargetDrop>();
            if (targetDrop != null)
                break;
        }

        if (targetDrop != null)
        {
            // handle all logics in IDropHandler
        }
        else
        {
            // Jika tidak, kembalikan ke parent awal
            canvasGroup.blocksRaycasts = true;
            ResetPosition();
        }
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
