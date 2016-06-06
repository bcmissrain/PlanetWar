using UnityEngine;
using System.Collections;

public class TestMainPage : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void PlayGame()
    {
        Application.LoadLevel("LevelTest");
    }
}
