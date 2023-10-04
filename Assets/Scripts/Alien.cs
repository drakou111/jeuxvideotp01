using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
  [SerializeField] int health = 2;
  [SerializeField] int damage = 1;

  // Start is called before the first frame update
  void Start()
  {

  }

  private void OnCollisionEnter(Collision collision)
  {
    if (this.health <= 0 && gameObject.active)
    {
      if (collision.gameObject.CompareTag("Player"))
      {
        this.health = 0;
        collision.gameObject.GetComponent<Player>().hit(damage);
      }

      if (collision.gameObject.CompareTag("Bullet"))
      {
        int damage = collision.gameObject.GetComponent<Bullet>().getDamage();
        hit(damage);
      }
    }
  }


  // Update is called once per frame
  void Update()
  {
    
  }

  void hit(int health)
  {
    this.health -= health;
    if (this.health < 0)
    {
      this.health = 0;
    }
  }
}