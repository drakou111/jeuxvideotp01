using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] Player player;
  private void OnEnable()
  {

  }

  [SerializeField] int damage = 1;

  // Start is called before the first frame update
  void Start()
  {

  }

  public int getDamage()
  {
    return damage;
  }


  // Update is called once per frame
  void Update()
  {

  }
}
