using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
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
      DontDestroyOnLoad(gameObject);
      foreach (Sound s in sounds){
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
      }
    }

    public void Stop(String name){
      Sound s = Array.Find(sounds, sound => sound.name == name);
      s.source.Stop();
    }

    void Start(){
      Play("theme");
    }

    public void Play(String name){
      Sound s = Array.Find(sounds, sound => sound.name == name);
      s.source.Play();
    }
}
