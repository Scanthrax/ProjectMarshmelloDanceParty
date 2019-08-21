//Author:   Ron Weeden
//Modified: 6/20/2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Sounds { Music, GroundImpact, AsphaltFootsteps, SlingshotStretch, Groans, FruitSplat, RockImpact}
public enum SoundChannels { Music, SFX, Footsteps, GroundImpact}

/// <summary>
/// The AudioManager singleton class handles audio requests
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// singleton insance
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// The GameObject that contains sources for 2D sounds
    /// </summary>
    public GameObject audioSourceContainer;

    /// <summary>
    /// List of sounds 2D sounds that are facilited through the Audio Manager
    /// </summary>
    public List<Sound> managerSounds;

    /// <summary>
    /// A Dictionary that contains the audio sources of the manager sounds; the key is a Sounds enum
    /// </summary>
    public Dictionary<Sounds, AudioSource> sourceDictionary;

    /// <summary>
    /// The list of sound mappings that will be set through the inspector.  A Sounds enum will map to a list of sounds of that category
    /// </summary>
    public List<SoundMap> listOfSoundMaps;

    /// <summary>
    /// A Dictionary that contains sounds for each label/category
    /// </summary>
    public Dictionary<Sounds, List<AudioClip>> soundDictionary;

    /// <summary>
    /// The audio mixer for the level
    /// </summary>
    public AudioMixer mixer;


    private void Awake()
    {
        #region Singleton Pattern
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("Duplicate singleton instance.  Deleting...", gameObject);
            Destroy(this);
        }
        #endregion

        // Initialize dictionaries
        sourceDictionary = new Dictionary<Sounds, AudioSource>();
        soundDictionary = new Dictionary<Sounds, List<AudioClip>>();

        // go through & populate the sounds dictionary from the list of mappings
        foreach (var item in listOfSoundMaps)
        {
            soundDictionary.Add(item.label, item.audioClips);
        }
        // clear for optimization
        listOfSoundMaps.Clear();


        foreach (var item in managerSounds)
        {
            var source = audioSourceContainer.AddComponent<AudioSource>();
            source.clip = soundDictionary[item.sound][item.index];
            source.volume = item.volume;
            source.spatialBlend = item.spatialBlend;
            source.loop = item.loop;
            source.playOnAwake = false;
            source.outputAudioMixerGroup = mixer.FindMatchingGroups(item.channel.ToString())[0];

            sourceDictionary.Add(item.sound, source);
        }

    }


    private void Start()
    {
        // play music at the beginning of the level
        PlaySound(Sounds.Music);
    }





    public void PlaySound(Sounds sound)
    {
        sourceDictionary[sound].Play();
    }


    public AudioSource AddSource(GameObject go, Sounds sound, int i, SoundChannels channel)
    {
        var source = go.AddComponent<AudioSource>();
        source.clip = soundDictionary[sound][i];
        source.playOnAwake = false;
        source.spatialBlend = 1;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(channel.ToString())[0];
        return source;
    }
    public AudioSource AddSource(GameObject go, Sounds sound, SoundChannels channel)
    {
        var source = go.AddComponent<AudioSource>();
        source.clip = soundDictionary[sound][0];
        source.playOnAwake = false;
        source.spatialBlend = 1;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(channel.ToString())[0];
        return source;
    }


    public void PlaySoundpool(AudioSource source, Sounds sound)
    {
        source.clip = soundDictionary[sound][Random.Range(0,soundDictionary[sound].Count)];
        source.Play();
    }





}
