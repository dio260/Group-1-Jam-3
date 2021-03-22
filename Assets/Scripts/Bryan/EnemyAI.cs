using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IInteractable
{
    public float StunTime { get; private set; }
    public bool IsStunned { get; private set; }
    [SerializeField] private float maxDistance, minDistance, stunTime, stunCooldown, escapeDistance;
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private float distance, cooldown;
    private Action state;
    private GameObject text;

    // Start is called before the first frame update
    void Awake()
    {
        text = transform.Find("Stunned Text").gameObject;
        agent = GetComponent<NavMeshAgent>();
        state = SitIdle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsStunned) state = Stunned;

        if (cooldown > 0 && !IsStunned) cooldown -= Time.fixedDeltaTime;

        distance = Vector3.Distance(target.position - new Vector3(0, target.position.y, 0), transform.position - new Vector3(0, transform.position.y, 0));

        state();
    }

    private void SitIdle()
    {
        agent.destination = transform.position;

        if (distance <= maxDistance)
        {
            state = MoveTowardsPlayer;
            return;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (distance > escapeDistance)
        {
            state = SitIdle;
            return;
        }

        if (distance <= minDistance)
        {
            state = AttackPlayer;
            return;
        }

        agent.destination = target.position;
    }

    private void AttackPlayer()
    {
        agent.destination = transform.position;

        transform.LookAt(target);

        if (distance > minDistance + .5f)
        {
            state = MoveTowardsPlayer;
            return;
        }
    }

    private void Stunned()
    {
        if (StunTime <= 0)
        {
            IsStunned = false;
            state = MoveTowardsPlayer;
            text.SetActive(false);
            return;
        }

        StunTime -= Time.fixedDeltaTime;

        agent.destination = transform.position;
    }

    private void Stun()
    {
        if (cooldown <= 0)
        {
            IsStunned = true;
            cooldown = stunCooldown;
            StunTime = stunTime;
            text.SetActive(true);
        }
    }

    public void Interact()
    {
        Stun();
    }

    public float GetStunTimeSetting()
    {
        return stunTime;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
