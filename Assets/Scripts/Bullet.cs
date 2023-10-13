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
  [SerializeField] float speed = 1;
  [SerializeField] float bulletCooldown = 6;
  private float bulletTimer = 0;


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
    transform.Translate(speed * Mathf.Cos(transform.rotation.x)*Time.deltaTime, 0, speed * Mathf.Sin(transform.rotation.z) * Time.deltaTime);
    bulletTimer += Time.deltaTime;
    if (bulletTimer > bulletCooldown)
    {
      gameObject.SetActive(false);
      bulletTimer = 0;
    }
  }
}
