using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
  [SerializeField] int initialHealth = 2;
  [SerializeField] int health = 2;
  [SerializeField] int enemyDamage = 1;
  [SerializeField] private CollectibleManager collectibleManager;
  [SerializeField] private SpawnerManager spawnManager;
  [SerializeField] private SoundManager soundManager;
  [SerializeField] private const float crushVelocityThreshold = -1;


  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      if (collision.gameObject.GetComponent<CharacterController>().velocity.y >= crushVelocityThreshold)
      {
        collision.gameObject.GetComponent<Player>().hit(enemyDamage);
      }
      deactivate();
    }
    if (collision.gameObject.CompareTag("Bullet"))
    {
      int damage = collision.gameObject.GetComponent<Bullet>().getDamage();
      collision.gameObject.SetActive(false);
      hit(damage);
    }
    if (collision.gameObject.CompareTag("Missile"))
    {
      collision.gameObject.SetActive(false);
      spawnManager.checkExplosion(transform.position, collision.gameObject.GetComponent<Bullet>().getDamage());
      deactivate();
    }
  }
  void Update()
  {
  }

  public void deactivate()
  {
    soundManager.playAlienDeathSound(transform.position);
    this.health = this.initialHealth;
    gameObject.SetActive(false);
    if (collectibleManager)
    {
      collectibleManager.trySpawnCollectible(transform.position);
    }
  }

  public void hit(int damage)
  {
    this.health -= damage;
    if (this.health <= 0 && gameObject.activeSelf)
    {
      deactivate();
    }
  }

  public void setCollectibleManager(CollectibleManager cM)
  {
    collectibleManager = cM;
  }

  public void setSpawnManager(SpawnerManager sM)
  {
    spawnManager = sM;
  }

  public void setSoundManager(SoundManager sM)
  {
    soundManager = sM;
  }
}