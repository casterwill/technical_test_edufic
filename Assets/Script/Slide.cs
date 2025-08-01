using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button nextSlideButton;
    [SerializeField] private Button backStoryButton;
    [SerializeField] private int backToIndex;

    private static ChangeSlideManager changeSlideManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScriptCaching();

        RegisterButtonEvent();
    }

    private void ScriptCaching()
    {
        changeSlideManager = ChangeSlideManager.Singleton;
    }

    private void RegisterButtonEvent()
    {
        nextSlideButton?.onClick.AddListener(changeSlideManager.NextSlide);
        backStoryButton?.onClick.AddListener(() => changeSlideManager.BackToStory(backToIndex));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
