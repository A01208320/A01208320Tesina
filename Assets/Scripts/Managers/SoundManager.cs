using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {
    public enum Sound {
        Cannonlaunch,
        BallCorrect,
        BallIncorrect
    }

    public static void PlaySound(Sound sound) {
        GameObject soundObject = new GameObject();
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        AudioClip s;
        switch (sound) {
            case Sound.Cannonlaunch:
                s = GameManager.instance.CannonSound;
                break;
            case Sound.BallCorrect:
                s = GameManager.instance.BallCorrect;
                break;
            case Sound.BallIncorrect:
                s = GameManager.instance.BallIncorrect;
                break;
            default:
                Debug.LogError("Sound not found");
                return;
        }
        audioSource.PlayOneShot(s);
    }
}
