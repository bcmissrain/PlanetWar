using UnityEngine;
using System.Collections;

public class SliderLabelUpdater : MonoBehaviour {
    public UISlider sliderObj;
    public UILabel sliderLabel;

	void Start () {
	
	}
	
	void Update () {
        if (sliderObj != null && sliderLabel != null)
        {
            SharedGameData.TroopSendPercent = sliderObj.value;
            sliderLabel.text = ((int)(sliderObj.value * 100)).ToString() + "%";
        }
    }
}
