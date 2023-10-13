using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerShooting : MonoBehaviour
{
  [SerializeField] GameObject bullet;
  [SerializeField] GameObject gun;
  [SerializeField] float shootCooldown;
  float shootTimer = 0;
  private static int bulletCount = 50;

  private List<GameObject> bullets = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < bulletCount; i++)
    {
      GameObject currentBullet = Instantiate(bullet);
      bullets.Add(currentBullet);
      currentBullet.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
    shootTimer += Time.deltaTime;
    if (Input.GetButtonDown("Fire1"))
    {
      if (shootTimer >= shootCooldown)
      {
        shootTimer = 0;
        Shoot();
      }
    }
  }

  void Shoot()
  {
    foreach (GameObject iBullet in bullets)
    {
      if (!iBullet.activeSelf)
      {
        FireBullet(iBullet);
        return;
      }
    }
    GameObject newBullet = Instantiate(bullet);
    bullets.Add(newBullet);
    newBullet.SetActive(false);
    FireBullet(newBullet);
  }
  void FireBullet(GameObject bullet)
  {
    bullet.SetActive(true);
    bullet.transform.position = gun.transform.position;
    bullet.transform.rotation = transform.rotation;
  }

}
