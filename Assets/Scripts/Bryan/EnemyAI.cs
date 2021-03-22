using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private NavMeshAgent agent;
    private float distance;
    private float contactCooldown = 3;
    private float period = 2.0f;

    public int maxDistance;

    [HideInInspector]
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance > maxDistance)
        {
            //if(Time.time > contactCooldown)
            agent.isStopped = true; //agent.destination = new Vector3(1, 1, 1);
            transform.LookAt(target);
        }
        else
        {
            agent.isStopped = false;
            agent.destination = target.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.name == "Player")
        {
            other.transform.position = startPosition;
            //contactCooldown += period;

        }
    }
}
