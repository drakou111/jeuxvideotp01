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
  [SerializeField] float shootTimer;
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
    if (Input.GetButtonDown("Fire1"))
    {
      shootTimer += Time.deltaTime;
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
    GameObject currentBullet = Instantiate(bullet);
    bullets.Add(currentBullet);
    currentBullet.SetActive(false);
  }
  void FireBullet(GameObject iBullet)
  {
    iBullet.SetActive(true);
    iBullet.transform.position = gun.transform.position;
    iBullet.transform.rotation = new Quaternion(gun.transform.rotation.x, 0, gun.transform.rotation.z + MathF.PI / 2, 0);
  }

}
