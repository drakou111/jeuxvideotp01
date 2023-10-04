using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienMovement : MonoBehaviour
{
    [SerializeField] public GameObject goal;
    private NavMeshAgent navMeshAgent;
    private bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) {
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround && navMeshAgent)
            navMeshAgent.destination = goal.transform.position;
    }
}
