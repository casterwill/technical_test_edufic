using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSlideManager : MonoBehaviour
{
    [SerializeField] private Transform slideParent;
    private List<SlideInfo> slides = new List<SlideInfo>();

    private MovingCloudManager movingCloudManager;

    public static ChangeSlideManager Singleton;

    private int currentIndex = 0;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitiateSlideList();
        ScriptChaching();
    }

    private void ScriptChaching()
    {
        movingCloudManager = MovingCloudManager.Singleton;
    }

    private void InitiateSlideList()
    {
        int slideIndex = 0;
        foreach (Transform childTransform in slideParent)
        {
            if (childTransform.TryGetComponent<Slide>(out Slide slideScript))
            {
                slides.Add(new SlideInfo
                {
                    slideIndex = slideIndex,
                    slideParent = childTransform.GetComponent<Slide>()
                });

                slideIndex++;
            }

        }
    }

    public void CallAfterQuizIEnum()
    {
        StartCoroutine(AfterQuizCorrect());
    }

    private IEnumerator AfterQuizCorrect()
    {

        NextSlide();
        yield return new WaitForSeconds(3f);
        NextSlide();
        Debug.Log("Call AfterQuizCorrect ienum");
    }

    public void NextSlide()
    {
        currentIndex++;
        foreach (var SlideInfo in slides)
        {
            if (SlideInfo.slideIndex == currentIndex)
            {
                SlideInfo.slideParent.gameObject.SetActive(true);
                if (SlideInfo.slideParent.GetShowCloudOnFadeIn()) movingCloudManager.FadeInAll();
                Debug.Log("Next slide");
            }
            else
            {
                SlideInfo.slideParent.gameObject.SetActive(false);
            }
        }
    }

    public void BackToStory(int storyIndex)
    {
        currentIndex = storyIndex;
        foreach (var SlideInfo in slides)
        {
            if (SlideInfo.slideIndex == currentIndex)
            {
                SlideInfo.slideParent.gameObject.SetActive(true);
                Debug.Log("Nexy slide");
            }
            else
            {
                SlideInfo.slideParent.gameObject.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class SlideInfo
    {
        public int slideIndex;
        public Slide slideParent;
    }
}
