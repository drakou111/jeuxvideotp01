using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] int damage = 1;
  [SerializeField] float speed = 1;
  [SerializeField] float bulletCooldown = 6;
  private float bulletTimer = 0;

  public int getDamage()
  {
    return damage;
  }
  void Update()
  {
    transform.Translate(0, 0, speed * Time.deltaTime);
    bulletTimer += Time.deltaTime;
    if (bulletTimer > bulletCooldown)
    {
      gameObject.SetActive(false);
      bulletTimer = 0;
    }
  }
}
