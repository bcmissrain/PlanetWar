using UnityEngine;
using System.Collections;

public class GameMusicController : MonoBehaviour {

    public AudioSource m_AudioSource;
    public AudioClip m_WinMusic;
    public AudioClip m_LoseMusic;

	void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
	}

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
    }

    public void OnGameWin(EventData eventData)
    {
        m_AudioSource.loop = false;
        m_AudioSource.clip = m_WinMusic;
        m_AudioSource.Play();
    }

    public void OnGameLose(EventData eventData)
    {
        m_AudioSource.loop = false;
        m_AudioSource.clip = m_LoseMusic;
        m_AudioSource.Play();
    }
}
