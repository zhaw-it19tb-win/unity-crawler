using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSoundManager : MonoBehaviour
{
  [SerializeField] Slider volumeSlider;
  public AudioSource audio;

  void Start(){
    if(!PlayerPrefs.HasKey("musicVolume")){
      PlayerPrefs.SetFloat("musicVolume", 1f);
    }
    Load();
  }

  public void ChangeVolume(){
    FindObjectOfType<AudioManager>().ChangeSoundVolume(volumeSlider.value);
  }

  private void Load(){
    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
  }

  private void Save(){
    PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
  }

}
