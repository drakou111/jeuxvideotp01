using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spin();
    }

    void spin() {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
