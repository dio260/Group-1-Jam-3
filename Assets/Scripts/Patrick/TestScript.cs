using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"This is the test script. I belong to - {name}");
    }
}
