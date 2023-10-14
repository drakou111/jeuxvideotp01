using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
  [SerializeField] int initialHealth = 2;
  [SerializeField] int health = 2;
  [SerializeField] int enemyDamage = 1;
  [SerializeField] public CollectibleManager collectibleManager;
  [SerializeField] public SpawnerManager spawnManager;
  [SerializeField] private const float crushVelocityThreshold = -1;
  private AudioSource source;

  // Start is called before the first frame update
  void Start()
  {
    this.source = GetComponent<AudioSource>();
  }

  private void OnEnable()
  {
  }

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
    this.health = this.initialHealth;
    gameObject.SetActive(false);
    if (collectibleManager)
    {
      //source.PlayOneShot(collectibleManager.gameManager.soundManager.alienDeath);
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
}