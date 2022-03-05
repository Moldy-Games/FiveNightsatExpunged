using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAudio : MonoBehaviour
{
    public AudioSource sounds;
    public AudioClip[] audios;
    public int maxChance;
    public void PlaySound()
    {
        int chancer = Random.Range(0, maxChance);
        if(chancer <= audios.Length - 1)
        {
            sounds.clip = audios[chancer];
            sounds.Play();
        }
    }
}
