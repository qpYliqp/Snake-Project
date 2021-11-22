using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    //Il faudra gérer la gestion du son en fonction des conditions données (pour pas qu'il y ait des loop etc)
    public static SoundManager instance;


    public Sound[] au_ListSounds;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        foreach (Sound sound in au_ListSounds)
        {
            sound.au_source = gameObject.AddComponent<AudioSource>();
            sound.au_source.clip = sound.au_clip;
            sound.au_source.pitch = sound.f_pitch;
            sound.au_source.volume = sound.f_volume;

        }
    }

    public void PlayAMusic(string name)
    {

        foreach (Sound sound in au_ListSounds)
        {
            if(sound.str_name == name)
            {
               
                Debug.Log("trouvé");
                    sound.au_source.Play();
                  
                
            }
            else
            {
                Debug.Log("didnt find your shit");
            }
        }
       
    }

    public void StopAMusic(string name)
    {
        foreach (Sound sound in au_ListSounds)
        {
            if (sound.str_name == name)
            {

                Debug.Log("trouvé");
                sound.au_source.Stop();


            }
            else
            {
                Debug.Log("didnt find your shit");
            }
        }
    }

  
}
