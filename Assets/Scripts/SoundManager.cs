using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class SoundData
{
    public SoundManager.Sounds soundEnum;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)]
    public float volume = 0.5f;
}

public class AudioSourceData
{
    public SoundManager.Sounds? soundEnum;
    public AudioSource source;

    public AudioSourceData(SoundManager.Sounds? soundEnum, AudioSource source)
    {
        this.soundEnum = soundEnum;
        this.source = source;
    }

    public void Play(bool loop, AudioClip clip, float volume)
    {
        source.loop = loop;
        source.clip = clip;
        source.volume = volume;
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
        Wipe
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
    private HashSet<Sounds> soundsToPlay = new HashSet<Sounds>();

    public bool noOverlappingAudioClip;
    public Ease easeType;
    public float easeDuration;

    public void Play(Sounds soundEnum)
    {
        isPlaying = true;
        soundsToPlay.Add(soundEnum);
    }

    public void PlayBackground(Sounds soundEnum, bool loop = true)
    {
        SoundData soundData = soundDataList.Find(x => x.soundEnum == soundEnum);

        var currentSource = bkgSources[currentBkgSource];
        var nextSource = bkgSources[1 - currentBkgSource];
        if (currentSource.source.isPlaying)
        {
            DOTween.To(
                () => currentSource.source.volume,
                volume => currentSource.source.volume = volume,
                0f,
                easeDuration)
                .SetEase(easeType)
                .OnComplete(() => currentSource.source.Stop());
        }
        nextSource.Play(loop, soundData.audioClip, 0f);
        DOTween.To(
            () => nextSource.source.volume,
            volume => nextSource.source.volume = volume,
            soundData.volume,
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
                sourceWithSameClip.Play(loop, soundData.audioClip, soundData.volume);
                return;
            }
        }
        var foundSource = audioSources.Find(x => !x.source.isPlaying);
        if (foundSource != null)
        {
            foundSource.Play(loop, soundData.audioClip, soundData.volume);
        }
        else
        {
            audioSources.Add(new AudioSourceData(soundEnum, AddAudioSource()));
            audioSources[audioSources.Count - 1].Play(loop, soundData.audioClip, soundData.volume);
        }
    }

    void Awake()
    {
        bkgSources = new AudioSourceData[2]
        {
            new AudioSourceData(null, AddAudioSource()), 
            new AudioSourceData(null, AddAudioSource())
        };
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
            foreach (var sound in soundsToPlay)
            {
                PlayEffect(sound);
            }
            isPlaying = false;
            soundsToPlay.Clear();
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