using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSlideManager : MonoBehaviour
{
    public List<SlideInfo> slides;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AfterQuizCorrect()
    {
        NextSlide();
        yield return new WaitForSeconds(3f);
        NextSlide();
    }

    public void NextSlide()
    {
        currentIndex++;
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
