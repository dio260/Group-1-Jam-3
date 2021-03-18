using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private LayerMask hitCast, correctionHitCast;
    [SerializeField] private float maxDistance, minDistance, scrollSpeed;
    [SerializeField] [ReadOnly] private float distance;
    private Transform cameraObj, selectedObj;
    private Camera cameraComponent;
    private Rigidbody selectedRigid;
    private RigidbodyConstraints defaultConstraint;

    private void Awake()
    {
        cameraObj = transform.Find("Camera");
        cameraComponent = cameraObj.GetComponent<Camera>();
    }
    private void Update()
    {
        if (!PlayerMovement.StopPlayer)
        {
            HandleGrabObject();

            HandleUngrabObject();

            HandleScrolling();

            HandleMaintainingDistance();
        }
    }

    private void HandleGrabObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitCast))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    selectedObj = hit.transform;
                    distance = hit.distance > minDistance ? hit.distance : minDistance;
                    selectedRigid = selectedObj.GetComponent<Rigidbody>();
                    defaultConstraint = selectedRigid.constraints;
                    selectedRigid.constraints = RigidbodyConstraints.FreezeRotation | defaultConstraint;
                    selectedRigid.useGravity = false;
                    selectedRigid.velocity = Vector3.zero;
                    selectedObj.SetParent(cameraObj);
                }
            }
        }
    }

    private void HandleUngrabObject()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObj)
            {
                selectedObj.SetParent(null);
                selectedRigid.constraints = defaultConstraint;
                defaultConstraint = default;
                selectedRigid.useGravity = true;
                selectedObj = null;
                selectedRigid = null;
                distance = 0;
            }
        }
    }

    private void HandleMaintainingDistance()
    {
        Ray rayTest = cameraComponent.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(rayTest.origin, rayTest.direction * distance, Color.red);
        if (selectedObj)
        {
            if (Mathf.Abs(Vector3.Distance(cameraObj.position, selectedObj.position) - distance) > .1f)
            {
                Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out RaycastHit hit, distance, correctionHitCast))
                {
                    selectedObj.transform.position = ray.direction * distance + ray.origin;
                }
            }
        }
    }

    private void HandleScrolling()
    {
        float dis = distance;

        dis += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        dis = Mathf.Clamp(dis, minDistance, maxDistance);

        distance = dis;
    }
}
