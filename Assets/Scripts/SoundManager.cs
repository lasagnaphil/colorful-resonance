using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class SoundData
{
    public SoundManager.Sounds soundEnum;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)]
    public float Musicvolume = 0.5f;
}

public class AudioSourceData
{
    public SoundManager.Sounds? soundEnum;
    public AudioSource source;
    public bool isBgm;
    public float volume;

    public AudioSourceData(SoundManager.Sounds? soundEnum, AudioSource source, float volume, bool isBgm)
    {
        this.soundEnum = soundEnum;
        this.source = source;
        this.volume = volume;
        this.isBgm = isBgm;
    }

    public void Play(bool loop, AudioClip clip)
    {
        source.loop = loop;
        source.clip = clip;
        source.volume = volume*(isBgm ? SoundManager.BgmVolume : SoundManager.SfxVolume);
        source.Play();
    }
}

public struct SoundTuple
{
    public SoundManager.Sounds? soundEnum;
    public bool loop;

    public SoundTuple(SoundManager.Sounds soundEnum, bool loop)
    {
        this.soundEnum = soundEnum;
        this.loop = loop;
    }
}

public class SoundManager : MonoBehaviour
{
    public static float BgmVolume = 1f;
    public static float SfxVolume = 1f;

    public enum Sounds
    {
        Main,
        Clear,
        Die,
        Red,
        Blue,
        Yellow,
        Blink,
        Explosion,
        GetOrb,
        Hit,
        MonsterDie,
        TileActivate,
        Move1,
        Green,
        MonsterEmerge,
        Shoot,
        Wipe,
        Opening
    }

    /*
    public int poolCapacity = 19;
    private List<AudioSource> audioSourcePool;
    */

    private List<AudioSourceData> audioSources = new List<AudioSourceData>();

    private AudioSourceData[] bkgSources = new AudioSourceData[2];
    private int currentBkgSource = 0;

    public List<SoundData> soundDataList;

    private bool isPlaying = false;
    public bool IsMainBgmPlaying = false;
    private HashSet<SoundTuple> soundsToPlay = new HashSet<SoundTuple>();

    public bool noOverlappingAudioClip;
    public Ease easeType;
    public float easeDuration;

    public void Play(Sounds soundEnum, bool loop = false)
    {
        isPlaying = true;
        soundsToPlay.Add(new SoundTuple(soundEnum, false));
    }

    /*
    public void PlayRepeat(Sounds soundEnum, bool loop = true)
    {
        SoundData soundData = soundDataList.Find(x => x.soundEnum == soundEnum);
        var soundSource = audioSources.Find(x => x.soundEnum == soundEnum);

        if (soundSource != null && !soundSource.source.isPlaying)
        {            
            soundSource.Play(loop, soundData.audioClip, SfxVolume);
            return;
        }

        var foundSource = audioSources.Find(x => !x.source.isPlaying);
        if (foundSource != null && !foundSource.source.isPlaying)
        {         
            foundSource.Play(loop, soundData.audioClip, SfxVolume);
            return;
        }
        else
        {         
            audioSources.Add(new AudioSourceData(soundEnum, AddAudioSource()));
            if(!audioSources[audioSources.Count - 1].source.isPlaying)
                audioSources[audioSources.Count - 1].Play(loop, soundData.audioClip, SfxVolume);
            return;
        }
    }
    */


    public void PlayLevelResult(Sounds soundEnum, bool isWin, bool loop = false)
    {

    }

    public void PlayBackground(Sounds soundEnum, bool loop = true)
    {
        SoundData soundData = soundDataList.Find(x => x.soundEnum == soundEnum);

        var currentSource = bkgSources[currentBkgSource];
        var nextSource = bkgSources[1 - currentBkgSource];
        if (currentSource.source.isPlaying)
        {
            currentSource.source.Stop();
            /* DOTween.To(
                 () => currentSource.source.volume,
                 volume => currentSource.source.volume = volume,
                 0f,
                 easeDuration)
                 .SetEase(easeType)
                 .OnComplete(() => currentSource.source.Stop());*/
        }
        nextSource.volume = soundData.Musicvolume;
        nextSource.Play(loop, soundData.audioClip);
        DOTween.To(
            () => nextSource.source.volume,
            volume => nextSource.source.volume = volume,
            soundData.Musicvolume * BgmVolume,
            easeDuration)
            .SetEase(easeType);

        currentBkgSource = 1 - currentBkgSource;
    }

    private void PlayEffect(Sounds soundEnum, bool loop = false)
    {
        SoundData soundData = soundDataList.Find(x => x.soundEnum == soundEnum);
        if (noOverlappingAudioClip)
        {
            var sourceWithSameClip = audioSources.Find(x => x.soundEnum == soundEnum);
            if (sourceWithSameClip != null)
            {
                sourceWithSameClip.Play(loop, soundData.audioClip);
                return;
            }
        }
        var foundSource = audioSources.Find(x => !x.source.isPlaying);
        if (foundSource != null)
        {
            foundSource.Play(loop, soundData.audioClip);
        }
        else
        {
            audioSources.Add(new AudioSourceData(soundEnum, AddAudioSource(), soundData.Musicvolume, false));
            // Debug.Log(soundData.Musicvolume);
            audioSources[audioSources.Count - 1].Play(loop, soundData.audioClip);
        }
    }

    public TweenCallback AddSound(Sounds effectSound)
    {
        TweenCallback soundEffect = () => Play(effectSound);
        return soundEffect;
    }

    public void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _Instance = this;
        DontDestroyOnLoad(gameObject);
        bkgSources = new AudioSourceData[2]
        {
            new AudioSourceData(null, AddAudioSource(), 0f, true),
            new AudioSourceData(null, AddAudioSource(), 0f, true)
        };
    }
    private static SoundManager _Instance;
    public static SoundManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>("SoundManager"));
            }
            return _Instance;
        }
    }
    void Start()
    {
        /*
        audioSourcePool = new List<AudioSource>(poolCapacity);
        for(int i = 0; i < poolCapacity; i++)
        {
            audioSourcePool[i] = gameObject.AddComponent<AudioSource>();
        }
        */
    }

    void Update()
    {
        if (isPlaying)
        {
            foreach (var soundTuple in soundsToPlay)
            {
                PlayEffect(soundTuple.soundEnum.Value, soundTuple.loop);
            }
            isPlaying = false;
            soundsToPlay.Clear();
        }

        // Update volume of currently playing SFX and BGM
        foreach (var audioSource in audioSources)
        {
            audioSource.source.volume = audioSource.volume * SfxVolume;
        }
        foreach (var audioSource in bkgSources)
        {
            audioSource.source.volume = audioSource.volume * BgmVolume;
        }

        // delete the sources which are not playing
        var sourcesToDelete = audioSources.FindAll(x => !x.source.isPlaying);
        sourcesToDelete.ForEach(x => Destroy(x.source));
        sourcesToDelete.ForEach(x => audioSources.Remove(x));
    }

    public void Stop(Sounds soundEnum)
    {
        audioSources
            .FindAll(x => x.soundEnum == soundEnum)
            .ForEach(x => x.source.Stop());
    }

    public void StopBackground()
    {
        IsMainBgmPlaying = false;
        bkgSources[0].source.Stop();
        bkgSources[1].source.Stop();
    }

    public void StopAll()
    {
        audioSources.ForEach(x => x.source.Stop());
        StopBackground();
    }

    private AudioSource AddAudioSource()
    {
        return gameObject.AddComponent<AudioSource>();
        /*
        List<AudioSource> foundSources = audioSourcePool.FindAll(a => !a.isPlaying);
        if (foundSources.Count == 0)
        {
            audioSourcePool.Add();
        }
        */
    }

    private void DestroyAudioSource(AudioSource source)
    {
        Destroy(source);
    }
}