using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SoundManager : MonoBehaviour
{
    List<AudioSource> _channels;
    List<AudioClip> _audioClips;
    // Start is called before the first frame update
    void Awake()
    {
        
        _channels = GetComponents<AudioSource>().ToList();
        _audioClips = new List<AudioClip>();

        LoadSounds();
  
    }

    void LoadSounds()
    {
        //SoundEffects
        _audioClips.Add(Resources.Load<AudioClip>("Sounds/Player_Hurt_SFX"));
        _audioClips.Add(Resources.Load<AudioClip>("Sounds/Player_Lose_SFX"));
        _audioClips.Add(Resources.Load<AudioClip>("Sounds/Player_Jump_SFX"));


        //Musixs
        _audioClips.Add(Resources.Load<AudioClip>("Sounds/Game_Music"));
        _audioClips.Add(Resources.Load<AudioClip>("Sounds/End_Music"));
    }


    public void PlaySound(Channel channel,Sound sound )
    {
        _channels[(int)channel].clip = _audioClips[(int)sound];
        _channels[(int)channel].Play();
    }

    public void PlayMusic(Sound sound)
    {
        _channels[(int)Channel.MUSIC].clip = _audioClips[(int)sound];
        _channels[(int)Channel.MUSIC].volume = .25f;
        _channels[(int)Channel.MUSIC].loop = true;
        _channels[(int)Channel.MUSIC].Play();
    }
}
