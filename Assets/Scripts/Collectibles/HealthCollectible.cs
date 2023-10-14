using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
  [SerializeField] private int healthAmount = 2;
  private SoundManager soundManager;

  private void OnTriggerEnter(Collider collidier)
  {
    if (collidier.gameObject.CompareTag("Player"))
    {
      collidier.gameObject.GetComponent<Player>().heal(healthAmount);
      soundManager.playCollectibleSound(transform.position);
      gameObject.SetActive(false);
    }
  }
  public void setSoundManager(SoundManager sM)
  {
    soundManager = sM;
  }
}
