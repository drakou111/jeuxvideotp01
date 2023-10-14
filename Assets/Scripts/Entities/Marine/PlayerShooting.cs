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

  [SerializeField] GameManager gameManager;
  [SerializeField] SoundManager soundManager;
  [SerializeField] float shootCooldown;

  float shootTimer = 0;
  [SerializeField] private static int bulletCount = 200;
  [SerializeField] private static int maxMissiles = 15;
  [SerializeField] private float multiShotAngleOffset = 5f;

  private List<GameObject> bullets = new List<GameObject>();
  private List<GameObject> missiles = new List<GameObject>();

  private AudioSource source;

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
    if (((Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "") && Input.GetButton("Fire1")) || (Input.GetJoystickNames().Length >= 1 && Input.GetButton("Fire1J")))
    {
      if (shootTimer >= shootCooldown)
      {
        shootTimer = 0;
        Shoot();
      }
    }
    else if (((Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "") && Input.GetButton("Fire2")) || (Input.GetJoystickNames().Length >= 1 && Input.GetButton("Fire2J")))
    {
      if (gameManager.missiles != 0)
      {
        gameManager.addMissiles(-1);
        ShootMissile();
      }
    }
  }

  void Shoot()
  {
    ShootBullet(0);
    if (gameManager.multiShotCooldown > 0)
    {
      source.PlayOneShot(soundManager.tripleShootClip);
      ShootBullet(-multiShotAngleOffset);
      ShootBullet(multiShotAngleOffset);
    } else
    {
      source.PlayOneShot(soundManager.shootClip);
    }
  }

  void ShootBullet(float angleOffset)
  {
    foreach (GameObject iBullet in bullets)
    {
      if (!iBullet.activeSelf)
      {
        FireBullet(iBullet, angleOffset);
        return;
      }
    }
  }
  void FireBullet(GameObject bullet, float angleOffset)
  {
    bullet.SetActive(true);
    bullet.transform.position = gun.transform.position;
    bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, angleOffset, 0); ;
  }

  void ShootMissile()
  {
    foreach (GameObject iMissile in missiles)
    {
      if (!iMissile.activeSelf)
      {
        source.PlayOneShot(soundManager.missileShotClip);
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

  public void setSource(AudioSource src)
  {
    source = src;
  }

}
