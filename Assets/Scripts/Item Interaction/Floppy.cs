using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floppy : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameController.instance.ActivateSound();
        Destroy(gameObject);
    }
}
