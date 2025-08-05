using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;
    [SerializeField] private string fullText;
    [SerializeField] private float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Awake()
    {
        fullText = tmpText.text;

    }

    private void OnEnable()
    {
        typingCoroutine = StartCoroutine(TypeText());
    }

    void Update()
    {
        //if (isTyping && Input.GetKeyDown(skipKey))
        //{
        //    // Skip animasi, tampilkan teks penuh
        //    StopCoroutine(typingCoroutine);
        //    tmpText.text = fullText;
        //    isTyping = false;
        //}
    }

    public void SkipTyping()
    {
        StopCoroutine(typingCoroutine);
        tmpText.text = fullText;
        isTyping = false;
    }

    public bool GetIsTyping()
    {
        return isTyping;
    }
    IEnumerator TypeText()
    {
        isTyping = true;
        tmpText.text = "";

        foreach (char c in fullText)
        {
            tmpText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
