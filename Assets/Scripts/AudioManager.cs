﻿using UnityEngine;

[System.Serializable]
public class Sound {
    
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0.5f,1.5f)]
    public float pitch = 1f;   
    [Range(0f,0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f,0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;
    

    private AudioSource source;
    
    public void setSource (AudioSource source)
    {
        this.source = source;
        this.source.clip = this.clip;
        this.source.loop = loop;
    }

    public void Play(){
        source.volume = volume *(1 + Random.Range(-randomVolume/2f, randomVolume/2f));
        source.pitch = pitch *(1 + Random.Range(-randomPitch/2f, randomPitch/2f));
        this.source.Play();
    }

    public void Stop(){
        this.source.Stop();
    }

}

public class AudioManager : MonoBehaviour
{
   
   public static AudioManager instance;
   [SerializeField] Sound[] sounds;

    private void Awake() {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
    }
   private void Start() {
       for (int i = 0; i < sounds.Length; i++)
       {
           GameObject _go = new GameObject("Sound_"+ i + "_" + sounds[i].name);
           _go.transform.SetParent(this.transform);
           sounds[i].setSource(_go.AddComponent<AudioSource>());
       }
       PlaySound("MainMenuMusic");
   }

   public void PlaySound(string _name)
   {
       for (int i = 0; i < sounds.Length; i++)
       {
           if (sounds[i].name == _name)
           {
               sounds[i].Play();
               return;
           }
       }

       //no sound with name
       Debug.LogWarning("Audio Manager : Sound not found in list : "+ _name);
   } 
    public void StopSound(string _name)
   {
       for (int i = 0; i < sounds.Length; i++)
       {
           if (sounds[i].name == _name)
           {
               sounds[i].Stop();
               return;
           }
       }

       //no sound with name
       Debug.LogWarning("Audio Manager : Sound not found in list : "+ _name);
   } 

}
