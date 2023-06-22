using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;


public class AudioModule :BaseModule<AudioModule>
{
    //private List<AudioSourceDto> cacheSource = new List<AudioSourceDto>();
    private Dictionary<string,AudioClip> cacheClipDic = new Dictionary<string, AudioClip>();
    private Queue<AudioSourceDto> freeCacheSource = new Queue<AudioSourceDto>();
    private List<AudioSourceDto> useCacheSource = new List<AudioSourceDto>();

    private float curAudioVolume;
    private bool isPlayAudio ;

    public void SetSound(float volume, bool isPlay)
    {
        curAudioVolume = volume;
        isPlayAudio = isPlay;
    }

    public (float, bool) GetSountCfg()
    {
        return (curAudioVolume, isPlayAudio);
    }


    private AudioClip GetAudio(string path)
    {
        AudioClip tempClip = null;
        string audioName = path.Split('.')[0];

        if (cacheClipDic.ContainsKey(audioName))
        {
            tempClip = cacheClipDic[audioName];
        }
        else
        {
            tempClip = UIUtil.Instance.GetAudio(path);
            cacheClipDic.Add(audioName, tempClip);
        }
        return tempClip;
    }

    public void PlayAudio(string path, bool isBg = false, bool isLoop = false, AudioEnum audioEnum= AudioEnum.Audio_2D)
    {
        if (isBg)
        {
            var bgSound= useCacheSource.Find(item => item.isBg);
            if (bgSound!=null)
            {
                bgSound.audioSource.Stop();
            }
        }
        AudioSourceDto tempSourceDto = freeCacheSource.Count>0 ? freeCacheSource.Dequeue():null;
        if (tempSourceDto == null)
        {
            tempSourceDto = new AudioSourceDto();
            tempSourceDto.isBg = isBg;
            tempSourceDto.audioSource= gameObject.AddComponent<AudioSource>();
        }
        useCacheSource.Add(tempSourceDto);
        AudioSource tempSource = tempSourceDto.audioSource;
        tempSource.clip= GetAudio(path);
        tempSource.Play();
        tempSource.volume = curAudioVolume;
        tempSource.mute= isPlayAudio == false;
        tempSource.loop = isLoop;
        tempSource.playOnAwake = false;
    }

    public void SetVolume(float value)
    {
        curAudioVolume = value;
        CacheLogic.Instance.SaveVolume(value);
    }

    public void SetAudioState(bool state)
    {
        isPlayAudio = state==true;
        bool isMute = isPlayAudio == false;

        for (int i = 0; i < useCacheSource.Count; i++)
        {
            var tempSource = useCacheSource[i];
            tempSource.audioSource.mute = isMute;
        }
        CacheLogic.Instance.SaveIsPlayerSound(isPlayAudio);
    }

    public override void Update(float deltaTime)
    {
        if (useCacheSource.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < useCacheSource.Count; i++)
        {
            var tempSource = useCacheSource[i];
            if (!tempSource.audioSource.isPlaying)
            {
                tempSource.audioSource.clip = null;
                useCacheSource.Remove(tempSource);
                freeCacheSource.Enqueue(tempSource);
            }
            if (tempSource.audioSource.volume < curAudioVolume)
            {
                tempSource.audioSource.volume += deltaTime;
            }
            else if (tempSource.audioSource.volume > curAudioVolume)
            {
                tempSource.audioSource.volume -= deltaTime;
            }
        }
    }
}
