using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button nextSlideButton;
    [SerializeField] private Button backStoryButton;
    [SerializeField] private int backToIndex;

    [Header("Animation")]
    [SerializeField] private Animator slideAnimator;
    [SerializeField] private bool withoutFadeInAnimation = false;
    [SerializeField] private bool withoutFadeOutAnimation = false;

    private const string SLIDE_ANIMATION_HIDE = "Hide";

    [Header("Typing")]
    [SerializeField] private TypingEffect typingEffect;

    [Header("Transition Anim")]
    [SerializeField] private bool hideCloudOnFadeOut = true;
    [SerializeField] private bool showCloudOnFadeIn = true;

    private static ChangeSlideManager changeSlideManager;
    private static MovingCloudManager movingCloudManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScriptCaching();

        GetComponent();   
    }

    private void GetComponent()
    {
        if(!slideAnimator) slideAnimator = GetComponent<Animator>();
        if (TryGetComponent<TypingEffect>(out TypingEffect typing)) typingEffect = typing;
    }

    private void OnEnable()
    {
        if (withoutFadeInAnimation) return;
        slideAnimator.SetTrigger("Show");

        RegisterButtonEvent();
    }

    private void ScriptCaching()
    {
        changeSlideManager = ChangeSlideManager.Singleton;
        movingCloudManager = MovingCloudManager.Singleton;
    }

    private UnityEvent endOfHideEvent = new UnityEvent(); 

    public bool GetShowCloudOnFadeIn()
    {
        return showCloudOnFadeIn;
    }

    private void RegisterButtonEvent()
    {
        if (withoutFadeOutAnimation)
        {
            nextSlideButton?.onClick.AddListener(() => 
            {
                if (CheckIfTyping()) return;

                changeSlideManager.NextSlide();
                RemoveNextSlideButtonListener();
            });
        }
        else
        {
            nextSlideButton?.onClick.AddListener(() => 
            {
                if (CheckIfTyping()) return;

                if (hideCloudOnFadeOut) movingCloudManager.FadeOutAll();

                PlayHideSlideAnimation();
                RemoveNextSlideButtonListener();
            });

            endOfHideEvent.AddListener(() => changeSlideManager.NextSlide());
        }

        //nextSlideButton?.onClick.AddListener(changeSlideManager.NextSlide);
        backStoryButton?.onClick.AddListener(() => 
        {
            PlayHideSlideAnimation();
            endOfHideEvent.AddListener(() => changeSlideManager.BackToStory(backToIndex));
            RemoveBackStoryButtonListener();
        });
    }

    private bool CheckIfTyping()
    {   
        if (!typingEffect) return false;

        if (typingEffect.GetIsTyping())
        {
            typingEffect.SkipTyping();
            return true;
        }

        return false;
    }

    private void RemoveNextSlideButtonListener()
    {
        nextSlideButton.onClick.RemoveAllListeners();
    }

    private void RemoveBackStoryButtonListener()
    {
        backStoryButton.onClick.RemoveAllListeners();
    }

    public void PlayHideSlideAnimation()
    {
        slideAnimator.SetTrigger(SLIDE_ANIMATION_HIDE);
    }

    #region Animation Events

    public void AfterEndOfHideAnim()
    {
        //changeSlideManager.NextSlide();
        endOfHideEvent?.Invoke();
        endOfHideEvent.RemoveAllListeners();
    }

    public void AfterEndOfQuizCorrectAnim()
    {
        changeSlideManager.NextSlide();
        Debug.Log("NextSlide supposed to get called");
    }

    #endregion
}