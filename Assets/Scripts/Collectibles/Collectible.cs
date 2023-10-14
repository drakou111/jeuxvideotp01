using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 50f;
    [SerializeField] private float defaultLifeTime = 15;
    [SerializeField] private float lifeTime = 0;
    private void OnEnable()
    {
        this.lifeTime = 0;
    }
    void Update()
    {
        spin();
        lifeTime += Time.deltaTime;
        if (lifeTime >= defaultLifeTime) {
            gameObject.SetActive(false);
        }
    }

    void spin() {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
