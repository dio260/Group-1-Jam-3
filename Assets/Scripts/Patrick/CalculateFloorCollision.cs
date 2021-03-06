using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateFloorCollision : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    [SerializeField] private LayerMask mask;

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & mask.value) == 0)
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & mask.value) == 0)
        {
            IsGrounded = false;
        }
    }
}
