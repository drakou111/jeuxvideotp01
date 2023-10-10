using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollectible : MonoBehaviour
{
    [SerializeField] private int missileAmount = 5;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collidier)
    {
        if (collidier.gameObject.CompareTag("Player"))
        {
            collidier.gameObject.GetComponent<Player>().addMissile(missileAmount);
            gameObject.SetActive(false);
        }
    }
}
