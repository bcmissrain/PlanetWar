using UnityEngine;
using System.Collections;

public class GameMusicController : MonoBehaviour {
    public static bool MusicEnable = true;
    public static bool SoundEnable = true;

    public AudioSource m_AudioSourceMusic;
    public AudioSource m_AudioSourceSound;

    public AudioClip m_MenuBackMusic;
    public AudioClip m_GameBackMusic;
    public AudioClip m_WinMusic;
    public AudioClip m_LoseMusic;

    public AudioClip m_ClickOk;
    public AudioClip m_ClickNo;

    void Awake()
    {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.MENU_BEGIN_MUSIC_PLAY, OnMenuEnter);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_BEGIN_MUSIC_PLAY, OnLevelEnter);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.BUTTON_CLICK_OK_EVENT, OnButtonClickOK);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.BUTTON_CLICK_NO_EVENT, OnButtonClickNo);
    }

    void Start () {

	}

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.MENU_BEGIN_MUSIC_PLAY, OnMenuEnter);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_BEGIN_MUSIC_PLAY, OnLevelEnter);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
    }

    public void OnMenuEnter(EventData eventData)
    {
        m_AudioSourceMusic.loop = true;
        m_AudioSourceMusic.clip = m_MenuBackMusic;
        if (MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnLevelEnter(EventData eventData)
    {
        m_AudioSourceMusic.loop = true;
        m_AudioSourceMusic.clip = m_GameBackMusic;
        if (MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnGameWin(EventData eventData)
    {
        m_AudioSourceMusic.loop = false;
        m_AudioSourceMusic.clip = m_WinMusic;
        if (MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnGameLose(EventData eventData)
    {
        m_AudioSourceMusic.loop = false;
        m_AudioSourceMusic.clip = m_LoseMusic;
        if (MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnButtonClickOK(EventData eventData)
    {
        if (SoundEnable)
        {
            m_AudioSourceSound.clip = m_ClickOk;
            m_AudioSourceSound.Play();
        }
    }

    public void OnButtonClickNo(EventData eventData)
    {
        if (SoundEnable)
        {
            m_AudioSourceSound.clip = m_ClickNo;
            m_AudioSourceSound.Play();
        }
    }

    public void ResetMusic()
    {
        MusicEnable = !MusicEnable;
        if (MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
        else
        {
            m_AudioSourceMusic.Pause();
        }

        //控制音效
        if (MusicEnable)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
        }

        GameEventDispatcher.instance.InvokeEvent(EventNameList.OPTION_BUTTON_RESET_EVENT, null);
    }

    public void ResetSound()
    {
        SoundEnable = !SoundEnable;

        //控制音效
        if (SoundEnable)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            m_AudioSourceSound.Stop();
        }

        GameEventDispatcher.instance.InvokeEvent(EventNameList.OPTION_BUTTON_RESET_EVENT, null);
    }
}
