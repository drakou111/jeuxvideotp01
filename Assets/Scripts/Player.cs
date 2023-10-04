using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] int health = 20;
  [SerializeField] float speed = 20;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void hit(int health)
  {
    this.health = health;
    if (this.health < 0)
    {
      this.health = 0;
    }
  }
}
