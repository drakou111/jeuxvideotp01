using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollectible : MonoBehaviour
{
  [SerializeField] private int missileAmount = 5;
  [SerializeField] PlayerShooting player;
  private SoundManager soundManager;
  private void OnTriggerEnter(Collider collidier)
  {
    if (collidier.gameObject.CompareTag("Player"))
    {
      collidier.gameObject.GetComponent<Player>().addMissiles(missileAmount);
      soundManager.playCollectibleSound(transform.position);
      gameObject.SetActive(false);
    }
  }
  public void setSoundManager(SoundManager sM)
  {
    soundManager = sM;
  }
}
