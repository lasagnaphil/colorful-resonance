using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class SoundData
{
    public SoundManager.Sounds soundEnum;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
}

public class AudioSourceData
{
    public SoundManager.Sounds soundEnum;
    public AudioSource source;

    public AudioSourceData(SoundManager.Sounds soundEnum, AudioSource source)
    {
        this.soundEnum = soundEnum;
        this.source = source;
    }
}

public struct SoundTuple
{
    public SoundManager.Sounds soundEnum;
    public bool loop;

    public SoundTuple(SoundManager.Sounds soundEnum, bool loop)
    {
        this.soundEnum = soundEnum;
        this.loop = loop;
    }
}

public class SoundManager : Singleton<SoundManager>
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
        Move2,
        Move3,
        Move4
    }

    /*
    public int poolCapacity = 19;
    private List<AudioSource> audioSourcePool;
    */
    
    private List<AudioSourceData> audioSources = new List<AudioSourceData>();
    private AudioSourceData bkgAudioSource;

    public List<SoundData> soundDataList;

    public bool isPlaying = false;
    public HashSet<Sounds> soundsToPlay = new HashSet<Sounds>();

    public void Play(Sounds soundEnum)
    {
        isPlaying = true;
        soundsToPlay.Add(soundEnum);
    }

    public void PlayBackground(Sounds soundEnum, bool loop = true)
    {
        SoundData soundData = soundDataList.Find(x => x.soundEnum == soundEnum);
        if (bkgAudioSource == null)
            bkgAudioSource = new AudioSourceData(soundEnum, AddAudioSource());

        bkgAudioSource.source.loop = loop;
        bkgAudioSource.source.clip = soundData.audioClip;
        bkgAudioSource.source.volume = soundData.volume;
        bkgAudioSource.source.Play();
    }

    private void PlayEffect(Sounds soundEnum, bool loop = false)
    {
        SoundData soundData = soundDataList.Find(x => x.soundEnum == soundEnum);
        var foundSource = audioSources.Find(x => !x.source.isPlaying);
        if (foundSource != null)
        {
            foundSource.source.loop = loop;
            foundSource.source.clip = soundData.audioClip;
            foundSource.source.volume = soundData.volume;
            foundSource.source.Play();
        }
        else
        {
            audioSources.Add(new AudioSourceData(soundEnum, AddAudioSource()));
            audioSources[audioSources.Count - 1].source.loop = loop;
            audioSources[audioSources.Count - 1].source.clip = soundData.audioClip;
            audioSources[audioSources.Count - 1].source.volume = soundData.volume;
            audioSources[audioSources.Count - 1].source.Play();
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
        if (bkgAudioSource != null)
            bkgAudioSource.source.Stop();
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