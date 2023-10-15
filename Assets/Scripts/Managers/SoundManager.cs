using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  private static SoundManager instance = null;
  public static SoundManager Instance { get { return instance; } }

  private AudioSource source;

  [SerializeField] private GameObject mainCam;


  [SerializeField] private AudioClip alienDeath;
  [SerializeField] private AudioClip marineDeath;
  [SerializeField] private AudioClip marineDeath2;
  [SerializeField] private AudioClip marineHurt;
  [SerializeField] private AudioClip missileShot;
  [SerializeField] private AudioClip music;
  [SerializeField] private AudioClip music2;
  [SerializeField] private AudioClip powerupAppear;
  [SerializeField] private AudioClip powerupPickup;
  [SerializeField] private AudioClip shoot;
  [SerializeField] private AudioClip tripleShoot;
  [SerializeField] private AudioClip victory;

  public AudioClip marineHurtClip { get { return marineHurt; } }
  public AudioClip missileShotClip { get { return missileShot; } }
  public AudioClip powerupAppearClip { get { return powerupAppear; } }
  public AudioClip shootClip { get { return shoot; } }
  public AudioClip tripleShootClip { get { return tripleShoot; } }

  void Start()
  {
    if (instance == null) instance = this;
    else if (instance != this)
      Destroy(gameObject);
    source = GetComponent<AudioSource>();
    mainCam.GetComponent<AudioSource>().clip = music;
    mainCam.GetComponent<AudioSource>().Play();
  }

  public void playAlienDeathSound(Vector3 position)
  {
    transform.position = position;
    source.PlayOneShot(alienDeath);
  }

  public void playMarineDeathSound(Vector3 position)
  {
    transform.position = position;
    int random = Random.Range(1, 3);
    switch (random)
    {
      case 1:
        source.PlayOneShot(marineDeath);
        break;
      case 2:
        source.PlayOneShot(marineDeath2);
        break;
    }
  }

  public void playCollectibleSound(Vector3 position)
  {
    transform.position = position;
    source.PlayOneShot(powerupPickup);
  }

  public void victorySound()
  {
    IEnumerator fadeOut = FadeOut(mainCam.GetComponent<AudioSource>(), 1, victory);
    StartCoroutine(fadeOut);
  }

  public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, AudioClip nextClip)
  {
    float startVolume = audioSource.volume;

    while (audioSource.volume > 0)
    {
      audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

      yield return null;
    }
    audioSource.Stop();
    audioSource.volume = startVolume;
    audioSource.PlayOneShot(nextClip);
  }
}
