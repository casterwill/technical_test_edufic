using UnityEngine;

public class SlideDetailSO : ScriptableObject
{
    public string slideID;
    public SlideType slideType;
    public SlideTheme slideTheme;


    public enum SlideType
    {
        None,
        MainSlide,
        Explanation,
        DragAndDropQuiz,
    }

    public enum SlideTheme
    {
        None,
        HillHouse,
        FlowerOnBottom,
        DesertJourney,
        CityWithMountaine,
        FewHouses,
    }
}
