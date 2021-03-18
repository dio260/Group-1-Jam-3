using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float xSpeed, ySpeed, lowerClamp, upperClamp;
    private Action state;
    private Transform cameraObj;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraObj = transform.Find("Camera");
        state = MoveCamera;
    }

    private void Update()
    {
        if (PlayerMovement.StopPlayer) state = StopActions;

        state();
    }

    private void MoveCamera()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime, 0));

        float xAngle = cameraObj.rotation.eulerAngles.x - (Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime);

        if (xAngle > 180) xAngle -= 360;

        xAngle = Mathf.Clamp(xAngle, lowerClamp, upperClamp);

        Vector3 eulerAngles = cameraObj.rotation.eulerAngles;

        cameraObj.rotation = Quaternion.Euler(xAngle, eulerAngles.y, eulerAngles.z);
    }

    private void StopActions()
    {
        if (!PlayerMovement.StopPlayer) state = MoveCamera;
    }
}
