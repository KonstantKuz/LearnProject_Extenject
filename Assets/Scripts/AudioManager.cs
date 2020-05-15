using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioManager
{
    void PlaySound(string name);
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    public void PlaySound(string name)
    {
        Debug.Log("Play sound with name :" + name);
    }
}
