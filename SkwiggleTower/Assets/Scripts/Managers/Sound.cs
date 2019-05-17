using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    /// <summary>
    /// An assigned label/category that identifies a sound
    /// </summary>
    public Sounds sound;
    /// <summary>
    /// The index of a desired sound in the list of sounds of a desired category
    /// </summary>
    public int index;
    /// <summary>
    /// The channel that this sound will reside in
    /// </summary>
    public SoundChannels channel;

    [Range(0f,1f)]
    public float volume;

    /// <summary>
    /// 0 means that the sound is played in 2D space; 1 means 3D space
    /// </summary>
    [Range(0f, 1f)]
    public float spatialBlend;
    /// <summary>
    /// Determines whether this sound loops
    /// </summary>
    public bool loop;
}
