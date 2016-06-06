using UnityEngine;
using System.Collections;

public class TeachModeManager : MonoBehaviour {
    void Awake()
    {

    }

	void Start () {
        //播放音乐
        GameEventDispatcher.instance.InvokeEvent(EventNameList.LEVEL_BEGIN_MUSIC_PLAY, null);
    }
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("MainScene");
        }
	}
}
