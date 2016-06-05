using UnityEngine;
using System.Collections;

public class StarMaterial : MonoBehaviour {

    public StarElement starElement;

    public Material troopStarGreyMat;
    public Material troopStarBlueMat;
    public Material troopStarRedMat;
    public Material troopStarGreenMat;
    public Material troopStarYellowMat;
    public Material troopStarOrangeMat;
    public Material troopStarBlackMat;

    public Material masterStarGreyMat;
    public Material masterStarBlueMat;
    public Material masterStarRedMat;
    public Material masterStarGreenMat;
    public Material masterStarYellowMat;
    public Material masterStarOrangeMat;
    public Material masterStarBlackMat;

    public Material defenceStarGreyMat;
    public Material defenceStarBlueMat;
    public Material defenceStarRedMat;
    public Material defenceStarGreenMat;
    public Material defenceStarYellowMat;
    public Material defenceStarOrangeMat;
    public Material defenceStarBlackMat;

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 根据行星的色彩主题获取对应的材质
    /// </summary>
    public Material GetMaterialByStar(string colorStr)
    {
        string starType = starElement.m_StarType;
        Material resultMat = null;
        if (starType == StarType.TroopStar)
        {
            switch (colorStr)
            {
                case StarThemeColor.Grey:
                    resultMat = troopStarGreyMat;
                    break;
                case StarThemeColor.Blue:
                    resultMat = troopStarBlueMat;
                    break;
                case StarThemeColor.Red:
                    resultMat = troopStarRedMat;
                    break;
                case StarThemeColor.Green:
                    resultMat = troopStarGreenMat;
                    break;
                case StarThemeColor.Yellow:
                    resultMat = troopStarYellowMat;
                    break;
                case StarThemeColor.Orange:
                    resultMat = troopStarOrangeMat;
                    break;
                case StarThemeColor.Black:
                    resultMat = troopStarBlackMat;
                    break;
            }
        }
        else if (starType == StarType.DefenceStar)
        {
            switch (colorStr)
            {
                case StarThemeColor.Grey:
                    resultMat = defenceStarGreyMat;
                    break;
                case StarThemeColor.Blue:
                    resultMat = defenceStarBlueMat;
                    break;
                case StarThemeColor.Red:
                    resultMat = defenceStarRedMat;
                    break;
                case StarThemeColor.Green:
                    resultMat = defenceStarGreenMat;
                    break;
                case StarThemeColor.Yellow:
                    resultMat = defenceStarYellowMat;
                    break;
                case StarThemeColor.Orange:
                    resultMat = defenceStarOrangeMat;
                    break;
                case StarThemeColor.Black:
                    resultMat = defenceStarBlackMat;
                    break;
            }
        }
        else if (starType == StarType.MasterStar)
        {
            switch (colorStr)
            {
                case StarThemeColor.Grey:
                    resultMat = masterStarGreyMat;
                    break;
                case StarThemeColor.Blue:
                    resultMat = masterStarBlueMat;
                    break;
                case StarThemeColor.Red:
                    resultMat = masterStarRedMat;
                    break;
                case StarThemeColor.Green:
                    resultMat = masterStarGreenMat;
                    break;
                case StarThemeColor.Yellow:
                    resultMat = masterStarYellowMat;
                    break;
                case StarThemeColor.Orange:
                    resultMat = masterStarOrangeMat;
                    break;
                case StarThemeColor.Black:
                    resultMat = masterStarBlackMat;
                    break;
            }
        }
        return resultMat;
    }
}
