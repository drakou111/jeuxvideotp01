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
  [SerializeField] GameObject missile;
  [SerializeField] GameObject gun;


  [SerializeField] float shootCooldown;
  float shootTimer = 0;
  private static int bulletCount = 50;
  private static int maxMissiles = 15;

  private int missileCount = 5;
  private float multiShotCooldown = 0;

  private List<GameObject> bullets = new List<GameObject>();
  private List<GameObject> missiles = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < bulletCount; i++)
    {
      GameObject currentBullet = Instantiate(bullet);
      bullets.Add(currentBullet);
      currentBullet.SetActive(false);
    }

    for (int i = 0; i < maxMissiles; i++)
    {
      GameObject currentMissile = Instantiate(missile);
      missiles.Add(currentMissile);
      currentMissile.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
    shootTimer += Time.deltaTime;
    if (Input.GetButton("Fire1"))
    {
      if (shootTimer >= shootCooldown)
      {
        shootTimer = 0;
        Shoot();
      }
    } else if (Input.GetButtonDown("Fire2"))
    {
      if (missileCount != 0)
      {
        missileCount--;
        ShootMissile();
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

  void ShootMissile()
  {
    foreach (GameObject iMissile in missiles)
    {
      if (!iMissile.activeSelf)
      {
        FireMissile(iMissile);
        return;
      }
    }
  }
  void FireMissile(GameObject missile)
  {
    missile.SetActive(true);
    missile.transform.position = gun.transform.position;
    missile.transform.rotation = transform.rotation;
  }

  public void addMissiles(int missileAmount)
  {
    missileCount += missileAmount;
  }

  public void addMultiShot(float multishotAmount)
  {
    multiShotCooldown += multishotAmount;
  }

}
