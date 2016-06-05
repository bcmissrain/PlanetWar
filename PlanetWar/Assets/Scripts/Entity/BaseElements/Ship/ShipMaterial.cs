using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipMaterial : MonoBehaviour {

    public ShipElement shipElement;

    public Material shipGreyMat;
    public Material shipBlueMat;
    public Material shipRedMat;
    public Material shipGreenMat;
    public Material shipYellowMat;
    public Material shipOrangeMat;
    public Material shipBlackMat;

    void Start()
    {

    }

    void Update()
    {

    }

    public Material GetMaterialByShip(string colorStr)
    {
        Material resultMat = null;
        switch (colorStr)
        {
            case StarThemeColor.Grey:
                resultMat = shipGreyMat;
                break;
            case StarThemeColor.Blue:
                resultMat = shipBlueMat;
                break;
            case StarThemeColor.Red:
                resultMat = shipRedMat;
                break;
            case StarThemeColor.Green:
                resultMat = shipGreenMat;
                break;
            case StarThemeColor.Yellow:
                resultMat = shipYellowMat;
                break;
            case StarThemeColor.Orange:
                resultMat = shipOrangeMat;
                break;
            case StarThemeColor.Black:
                resultMat = shipBlackMat;
                break;
        }
        return resultMat;
    }
}
