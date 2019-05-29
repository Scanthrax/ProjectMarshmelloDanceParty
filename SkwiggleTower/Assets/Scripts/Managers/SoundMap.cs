using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
///This class is used for mapping sound enumerations to sounds in a dictionary through the inspector
public class SoundMap
{
    public Sounds label;
    public List<AudioClip> audioClips;
}
