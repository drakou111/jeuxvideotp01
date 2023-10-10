using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotCollectible : MonoBehaviour
{
    [SerializeField] private int multiShotAmount = 10;
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
            collidier.gameObject.GetComponent<Player>().addMultiShot(multiShotAmount);
            gameObject.SetActive(false);
        }
    }
}
