using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotCollectible : MonoBehaviour
{
  [SerializeField] private float multiShotAmount = 10.0f;
  [SerializeField] PlayerShooting player;
  private AudioSource source;
  public SoundManager soundManager;
  void Start()
  {
    source = gameObject.GetComponent<AudioSource>();
  }

  void Update()
  {

  }

  private void OnTriggerEnter(Collider collidier)
  {
    if (collidier.gameObject.CompareTag("Player"))
    {
      collidier.gameObject.GetComponent<Player>().addMultiShot(multiShotAmount);
      //source.PlayOneShot(soundManager.powerupPickup);
      gameObject.SetActive(false);
    }
  }
}
