using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float timeInSeconds;
    [SerializeField] private bool invert;
    private Transform door, hinge;
    private bool doorIsOpen, isFinished;
    private readonly object _lock = new object();

    private void Awake()
    {
        door = transform.Find("Door").Find("Cube");
        hinge = transform.Find("Pivot");
        isFinished = true;
    }

    public void ToggleDoor()
    {
        if (isFinished) doorIsOpen = !doorIsOpen;

        if (doorIsOpen) StartCoroutine(OpenDoor());
        else StartCoroutine(CloseDoor());
    }

    private IEnumerator OpenDoor()
    {
        if (!isFinished)
        {
            yield break;
        }

        Monitor.Enter(_lock);

        if (!isFinished)
        {
            yield break;
        }

        isFinished = false;

        float time = 0;

        Transform parent = door.parent;

        door.SetParent(hinge);

        float originalY = hinge.rotation.eulerAngles.y;

        float direction = invert ? -1 : 1;

        while (time < timeInSeconds)
        {
            hinge.rotation = Quaternion.Euler(0, originalY + 90 * curve.Evaluate(time / timeInSeconds) * direction, 0);
            time += 1f / 60f;
            yield return new WaitForSeconds(1f / 60f);
        }

        hinge.rotation = Quaternion.Euler(0, originalY + 90 * direction, 0);

        door.SetParent(parent);

        Monitor.Exit(_lock);

        isFinished = true;
    }

    private IEnumerator CloseDoor()
    {
        if (!isFinished)
        {
            yield break;
        }

        Monitor.Enter(_lock);

        if (!isFinished)
        {
            yield break;
        }

        isFinished = false;

        float time = 0;

        Transform parent = door.parent;

        door.SetParent(hinge);

        float originalY = hinge.rotation.eulerAngles.y;

        float direction = invert ? -1 : 1;

        while (time < timeInSeconds)
        {
            hinge.rotation = Quaternion.Euler(0, originalY - 90 * curve.Evaluate(time / timeInSeconds) * direction, 0);
            time += 1f / 60f;
            yield return new WaitForSeconds(1f / 60f);
        }

        hinge.rotation = Quaternion.Euler(0, originalY - 90 * direction, 0);

        door.SetParent(parent);

        Monitor.Exit(_lock);

        isFinished = true;
    }
}
