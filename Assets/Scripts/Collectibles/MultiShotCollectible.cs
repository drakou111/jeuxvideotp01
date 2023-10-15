using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotCollectible : MonoBehaviour
{
  [SerializeField] private float multiShotAmount = 10.0f;
  [SerializeField] PlayerShooting player;
  public SoundManager soundManager;
  private void OnTriggerEnter(Collider collidier)
  {
    if (collidier.gameObject.CompareTag("Player"))
    {
      collidier.gameObject.GetComponent<Player>().addMultiShot(multiShotAmount);
      soundManager.playCollectibleSound(transform.position);
      gameObject.SetActive(false);
    }
  }
  public void setSoundManager(SoundManager sM)
  {
    soundManager = sM;
  }
}
