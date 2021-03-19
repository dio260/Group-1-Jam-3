using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractControllerTouch : MonoBehaviour
{
    [SerializeField] private LayerMask hitMask;

    private void OnTriggerEnter(Collider other)
    {
        StepHandler(other);
    }

    private void StepHandler(Collider other)
    {
        if ((1 << other.gameObject.layer & hitMask.value) == 0 || PlayerMovement.StopPlayer) return;

        IInteractable interactable = other.transform.GetComponent(typeof(IInteractable)) as IInteractable;

        if (interactable as Component) interactable.Interact();
    }
}
