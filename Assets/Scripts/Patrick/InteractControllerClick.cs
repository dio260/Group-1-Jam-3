using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractControllerClick : MonoBehaviour
{
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool debugRay;
    private Transform cameraObj;
    private Camera cameraComponent;

    private void Awake()
    {
        cameraObj = transform.parent.Find("Camera");
        cameraComponent = cameraObj.GetComponent<Camera>();
    }

    private void Update()
    {
        if (debugRay)
        {
            Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
        if (Input.GetMouseButtonDown(0)) ClickHandler();
    }

    private void ClickHandler()
    {
        if (PlayerMovement.StopPlayer) return;

        Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitMask)) return;

        IInteractable interact = hit.transform.GetComponent(typeof(IInteractable)) as IInteractable;

        if (interact as Component) interact.Interact();
    }
}
