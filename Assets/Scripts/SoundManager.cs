using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] public AudioClip alienDeath;
    [SerializeField] public AudioClip elevator;
    [SerializeField] public AudioClip marineDeath;
    [SerializeField] public AudioClip marineDeath2;
    [SerializeField] public AudioClip marineHurt;
    [SerializeField] public AudioClip missileShot;
    [SerializeField] public AudioClip music;
    [SerializeField] public AudioClip music2;
    [SerializeField] public AudioClip powerupAppear;
    [SerializeField] public AudioClip powerupPickup;
    [SerializeField] public AudioClip shoot;
    [SerializeField] public AudioClip tripleShoot;
    [SerializeField] public AudioClip victory;
}
