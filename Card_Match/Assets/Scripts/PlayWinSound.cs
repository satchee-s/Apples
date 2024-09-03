using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWinSound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    private void OnEnable()
    {
        source.clip = clip;
        source.Play();
    }
}
