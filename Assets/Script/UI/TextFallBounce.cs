using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextFallBounce : MonoBehaviour
{
    public string fullText = "Halo Dunia!";
    public GameObject letterPrefab;
    public Transform letterParent;

    public float spacing = 35f;
    public float symbolSpacing = 5f;

    public float startYOffset = 100f;
    public float fallDuration = 0.5f;
    public float delayBetweenLetters = 0.05f;

    void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        float xOffset = 0f;

        for (int i = 0; i < fullText.Length; i++)
        {
            char c = fullText[i];

            GameObject letterObj = Instantiate(letterPrefab, letterParent);
            TextMeshProUGUI tmp = letterObj.GetComponent<TextMeshProUGUI>();
            RectTransform rt = letterObj.GetComponent<RectTransform>();

            tmp.text = c.ToString();
            tmp.ForceMeshUpdate();

            // Tunggu 1 frame supaya bounds valid
            yield return null;

            float charWidth = Mathf.Max(tmp.textBounds.size.x, 10f);
            float spacingBuffer = 2f;

            rt.anchoredPosition = new Vector2(xOffset, startYOffset);

            Vector2 targetPos = new Vector2(xOffset, 0f);
            rt.DOAnchorPos(targetPos, fallDuration).SetEase(Ease.OutBounce);

            xOffset += charWidth + spacingBuffer;

            yield return new WaitForSeconds(delayBetweenLetters);
        }
    }
}
