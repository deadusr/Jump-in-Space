using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAudio : MonoBehaviour {

    [SerializeField]
    AudioClip launchSound;
    
    [SerializeField]
    AudioClip boostersSound;

    public void PlayLaunchSound() {
        AudioSource.PlayClipAtPoint(launchSound, transform.position);
        AudioSource.PlayClipAtPoint(boostersSound, transform.position);
    }

    public void PlayBoosterSound() {
        AudioSource.PlayClipAtPoint(launchSound, transform.position);
    }


}