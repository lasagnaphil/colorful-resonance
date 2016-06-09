using UnityEngine;
using System.Collections.Generic;

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

    public List<AudioSourceData> audioSources = new List<AudioSourceData>();

    public List<SoundData> soundDataList;

    public void Play(Sounds soundEnum, bool loop = false)
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
            audioSources.Add(new AudioSourceData(soundEnum, gameObject.AddComponent<AudioSource>()));
            audioSources[audioSources.Count - 1].source.loop = loop;
            audioSources[audioSources.Count - 1].source.clip = soundData.audioClip;
            audioSources[audioSources.Count - 1].source.volume = soundData.volume;
            audioSources[audioSources.Count - 1].source.Play();
        }
    }

    void Update()
    {
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

    public void StopAll()
    {
        audioSources.ForEach(x => x.source.Stop());
    }
}