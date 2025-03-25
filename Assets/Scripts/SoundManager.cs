using GuanYu;
using GuanYu.UI;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource efxSource1;
    public AudioSource efxSource2;
    public AudioSource efxSource3;

    [Header("Campain")]
    public AudioSource efxSourceCampain1;
    public AudioSource efxSourceCampain2;
    public AudioSource efxSourceCampain3;

    [Header("Home")]
    public AudioSource efxSourceBoss1;
    public AudioSource efxSourceBoss2;
    public AudioSource efxSourceBoss3;

    public AudioSource efxSourceHome1;
    public AudioSource efxSourceHome2;
    public AudioSource efxSourceHome3;

    public AudioSource musicSource;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public AudioClip audioCoin;
    public AudioClip audioDie;
    public AudioClip audioBgm;
    public AudioClip audioSide;
    public AudioClip audioBgmCampain;
    public AudioClip audioBgmArena;
    public AudioClip audioDefeatUpate;
    public AudioClip audioReceiReward;
    [SerializeField] private AudioClip audioWinUpate;
    [SerializeField] private AudioClip buttonAudio;
    [SerializeField] private AudioClip buttonOffAudio;

    [SerializeField] private Image SoundONIcon;
    [SerializeField] private Image SoundOFFIcon;
    private bool muted = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if (PlayerPrefs.GetInt("FirstPlay", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstPlay", 1);
            PlayerPrefs.SetInt("MusicOn", 1);
            PlayerPrefs.SetInt("SoundOn", 1);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }
    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        if (efxSource1.isPlaying)
        {
            PlaySecond(clip);
        }
        else
        {
            efxSource1.PlayOneShot(clip);
        }
    }

    private void PlaySecond(AudioClip clip)
    {
        if (efxSource2.isPlaying)
        {
            PlayThird(clip);
        }
        else
        {
            efxSource2.PlayOneShot(clip);
        }
    }

    private void PlayThird(AudioClip clip)
    {
        efxSource3.PlayOneShot(clip);
    }
    public void PlaySingleHome(AudioClip clip)
    {
        if (efxSourceBoss1.isPlaying)
        {
            PlaySecondHome(clip);
        }
        else
        {
            efxSourceBoss2.PlayOneShot(clip);
        }
    }

    private void PlaySecondHome(AudioClip clip)
    {
        if (efxSourceBoss2.isPlaying)
        {
            PlayThird(clip);
        }
        else
        {
            efxSourceBoss2.PlayOneShot(clip);
        }
    }

    private void PlayThirdHome(AudioClip clip)
    {
        efxSourceBoss3.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        //if (GameController.Instance.IsSoundOn)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
    public void Mute()
    {
        musicSource.volume = 0;
        //efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
        PlayerPrefs.SetInt("MusicOn", 0);
    }

    public void ContinueMusic()
    {
        musicSource.volume = 0.5f;
        //efxSource1.volume = efxSource2.volume = efxSource3.volume = 1;
        PlayerPrefs.SetInt("MusicOn", 1);
    }
    public void ToggleSound(bool isOn)
    {

        if (!isOn)
        {
            efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
            PlayerPrefs.SetInt("SoundOn", 0);
            //PlayerPrefsX.SetBool("IsSoundOn", true);
        }
        else
        {
            efxSource1.volume = efxSource2.volume = efxSource3.volume = 1f;
            PlayerPrefs.SetInt("SoundOn", 1);
            //PlayerPrefsX.SetBool("IsSoundOn", false);
        }
        //if (withSound)
        //{
        //    PlaySingle(UISfxController.Instance.SfxSettingSound);
        //}
    }

    public void OnButtonPress()
    {
        PressButtonAudio();
        PlaySingle(audioSide);
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            SoundONIcon.enabled = true;
            SoundOFFIcon.enabled = false;
        }
        else
        {
            SoundONIcon.enabled = false;
            SoundOFFIcon.enabled = true;
        }
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public void ChangeAudioSpeed()
    {
        if (MenuController.Instance.Menu == Menu.Arena)
        {
            if (efxSource1 != null && efxSource2 != null && efxSource3 != null)
            {
                efxSource1.pitch = BattleController.instance.SpeedGame;
                efxSource2.pitch = BattleController.instance.SpeedGame;
                efxSource3.pitch = BattleController.instance.SpeedGame;
                Debug.Log("efxSource1.volume " + efxSource1.pitch + "efxSource2.volume " + efxSource2.pitch + "efxSourc3.volume " + efxSource3.pitch);
                Debug.Log("SpeedGame" + BattleController.instance.SpeedGame);
            }
            else
            {
                Debug.Log("chua gan âm thanh");
            }
        }
        else if (MenuController.Instance.Menu == Menu.Camp)
        {
            if (efxSource1 != null && efxSource2 != null && efxSource3 != null)
            {
                efxSource1.pitch = 1;
                efxSource2.pitch = 1;
                efxSource3.pitch = 1;
                Debug.Log("efxSource1.volume " + efxSource1.pitch + "efxSource2.volume " + efxSource2.pitch + "efxSourc3.volume " + efxSource3.pitch);
            }
            else
            {
                Debug.Log("chua gan âm thanh");
            }
        }else if (MenuController.Instance.Menu == Menu.Home)
        {
            if (efxSourceBoss1 != null && efxSourceBoss2 != null && efxSourceBoss3 != null)
            {
                efxSourceBoss1.pitch = 1;
                efxSourceBoss2.pitch = 1;
                efxSourceBoss3.pitch = 1;
                Debug.Log("efxSource1.volume " + efxSource1.pitch + "efxSource2.volume " + efxSource2.pitch + "efxSourc3.volume " + efxSource3.pitch);
            }
            else
            {
                Debug.Log("chua gan âm thanh");
            }
        }
    }
    public void StopAudio()
    {
        if (efxSource1 != null && efxSource2 != null && efxSource3 != null)
        {
            efxSource1.Stop();
            efxSource2.Stop();
            efxSource3.Stop();
        }
        else
        {
            Debug.Log("chua gan âm thanh");
        }
    }
    public void SoundCampain1(AudioClip clip)
    {
        if (efxSourceCampain1.isPlaying)
        {
            SoundCampain2(clip);
        }
        else
        {
            efxSourceCampain1.PlayOneShot(clip);
        }
    }

    private void SoundCampain2(AudioClip clip)
    {
        if (efxSourceCampain2.isPlaying)
        {
            SoundCampain3(clip);
        }
        else
        {
            efxSourceCampain2.PlayOneShot(clip);
        }
    }

    private void SoundCampain3(AudioClip clip)
    {
        efxSourceCampain3.PlayOneShot(clip);
    }

    public void SoundHome1(AudioClip clip)
    {
        if (efxSourceHome1.isPlaying)
        {
            SoundHome2(clip);
        }
        else
        {
            efxSourceHome1.PlayOneShot(clip);
        }
    }

    private void SoundHome2(AudioClip clip)
    {
        if (efxSourceHome2.isPlaying)
        {
            SoundHome3(clip);
        }
        else
        {
            efxSourceHome2.PlayOneShot(clip);
        }
    }

    private void SoundHome3(AudioClip clip)
    {
        efxSourceHome3.PlayOneShot(clip);
    }

    public void OnsoundCampain()
    {
        efxSourceCampain1.volume = 1;
        efxSourceCampain2.volume = 1;
        efxSourceCampain3.volume = 1;
        PlayMusicBgmCampain();
    }
    public void OnsoundArena()
    {
        efxSource1.volume = 1;
        efxSource2.volume = 1;
        efxSource3.volume = 1;
        PlayMusicBgArena();
    }
    public void OffSonud()
    {
        efxSource1.volume = 0;
        efxSource2.volume = 0;
        efxSource3.volume = 0;
        efxSourceCampain1.volume = 0;
        efxSourceCampain2.volume = 0;
        efxSourceCampain3.volume = 0;
    }

    public void PlayMusicBgmCampain()
    {
        SoundCampain1(audioBgmCampain);
    }

    public void PlayMusicBgArena()
    {
        PlaySingle(audioBgmArena);
    }

    public void SpawnGoldSound()
    {
        SoundCampain1(audioCoin);
    }

    public void DefeatUpdate()
    {
        SoundHome1(audioDefeatUpate);
        Debug.Log("Defeatupdate");
    }
    public void WinUpdate()
    {
        SoundHome1(audioWinUpate);
        Debug.Log("winupdate");
    }

    public void PressButtonAudio()
    {
        SoundHome1(buttonAudio);
    }

    public void CloseButtonAudio()
    {
        SoundHome1(buttonOffAudio);
    }

    public void AudioReward()
    {
        SoundHome1(this.audioReceiReward);
        Debug.Log("audioreward");
    }
}

