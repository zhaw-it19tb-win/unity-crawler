using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
  public static float volume = 1;
  public Sound[] sounds;
  public static AudioManager instance;
    void Awake()
    {
      if(instance == null){
        instance = this;
      } else {
        Destroy(gameObject);
        return;
      }
      foreach (Sound s in sounds){
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
      }
      updateVolume();
    }

    public void updateVolume(){
      ChangeSoundVolume(volume);
    }

    public void ChangeSoundVolume(float vol){
      foreach (Sound s in sounds){
        s.setVolume(vol);
      }
      volume = vol;


    }

    void Start(){
      Play("Theme");
    }

    public void Play(String name){
      Sound s = Array.Find(sounds, sound => sound.name == name);
      s.source.Play();
    }
}
