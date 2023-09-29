using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerShooting : MonoBehaviour
{
  [SerializeField] GameObject bullet;
  private static int bulletCount = 15;
  
  private GameObject[] bullets = new GameObject[bulletCount];

  [SerializeField]
  private UnityEvent launchHitEvent;

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < bulletCount; i++)
    {
      bullets[i] = Instantiate(bullet);
      bullets[i].SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {

    }
  }
}
