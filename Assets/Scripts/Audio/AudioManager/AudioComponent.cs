using UnityEngine;
public enum AudioType{
    OneShot, Loop, Background
}

[System.Serializable]
public class AudioComponent{
    public AudioType Type;
    public AudioClip Clip;
}