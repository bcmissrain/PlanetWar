using UnityEngine;
using System.Collections;

public class GameMusicController : MonoBehaviour {
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
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.MUSIC_RESET_EVENT, OnResetMusic);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.SOUND_RESET_EVENT, OnResetSound);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.BUTTON_CLICK_OK_EVENT, OnButtonClickOK);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.BUTTON_CLICK_NO_EVENT, OnButtonClickNo);

        DontDestroyOnLoad(gameObject);
    }

    void Start () {

	}

    void OnDestroy()
    {
        //GameEventDispatcher.instance.RemoveEventHandler(EventNameList.MENU_BEGIN_MUSIC_PLAY, OnMenuEnter);
        //GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_BEGIN_MUSIC_PLAY, OnLevelEnter);
        //GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        //GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
        //GameEventDispatcher.instance.RegistEventHandler(EventNameList.MUSIC_RESET_EVENT, OnResetMusic);
        //GameEventDispatcher.instance.RegistEventHandler(EventNameList.SOUND_RESET_EVENT, OnResetSound);
        //GameEventDispatcher.instance.RemoveEventHandler(EventNameList.BUTTON_CLICK_OK_EVENT, OnButtonClickOK);
        //GameEventDispatcher.instance.RemoveEventHandler(EventNameList.BUTTON_CLICK_NO_EVENT, OnButtonClickNo);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.MENU_BEGIN_MUSIC_PLAY);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.LEVEL_BEGIN_MUSIC_PLAY);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.LEVEL_PLAYER_WIN_EVENT);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.LEVEL_PLAYER_LOSE_EVENT);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.MUSIC_RESET_EVENT);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.SOUND_RESET_EVENT);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.BUTTON_CLICK_OK_EVENT);
        GameEventDispatcher.instance.RemoveEventHandlerByName(EventNameList.BUTTON_CLICK_NO_EVENT);
    }

    public void OnMenuEnter(EventData eventData)
    {
        m_AudioSourceMusic.loop = true;
        m_AudioSourceMusic.clip = m_MenuBackMusic;
        if (SharedGameData.MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnLevelEnter(EventData eventData)
    {
        m_AudioSourceMusic.loop = true;
        m_AudioSourceMusic.clip = m_GameBackMusic;
        if (SharedGameData.MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnGameWin(EventData eventData)
    {
        m_AudioSourceMusic.loop = false;
        m_AudioSourceMusic.clip = m_WinMusic;
        if (SharedGameData.MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnGameLose(EventData eventData)
    {
        m_AudioSourceMusic.loop = false;
        m_AudioSourceMusic.clip = m_LoseMusic;
        if (SharedGameData.MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
    }

    public void OnButtonClickOK(EventData eventData)
    {
        if (SharedGameData.SoundEnable)
        {
            m_AudioSourceSound.clip = m_ClickOk;
            m_AudioSourceSound.Play();
        }
    }

    public void OnButtonClickNo(EventData eventData)
    {
        if (SharedGameData.SoundEnable)
        {
            m_AudioSourceSound.clip = m_ClickNo;
            m_AudioSourceSound.Play();
        }
    }

    public void OnResetMusic(EventData eventData)
    {
        SharedGameData.MusicEnable = !SharedGameData.MusicEnable;
        if (SharedGameData.MusicEnable)
        {
            m_AudioSourceMusic.Play();
        }
        else
        {
            m_AudioSourceMusic.Pause();
        }

        //控制音效
        if (SharedGameData.MusicEnable)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
        }

        GameEventDispatcher.instance.InvokeEvent(EventNameList.OPTION_BUTTON_RESET_EVENT, null);
    }

    public void OnResetSound(EventData eventData)
    {
        SharedGameData.SoundEnable = !SharedGameData.SoundEnable;

        //控制音效
        if (SharedGameData.SoundEnable)
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
