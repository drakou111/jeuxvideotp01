using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienMovement : MonoBehaviour
{
    [SerializeField] public GameObject goal;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) {
            navMeshAgent.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent && navMeshAgent.enabled)
            navMeshAgent.destination = goal.transform.position;
    }
}
