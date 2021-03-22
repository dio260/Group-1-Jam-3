using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameController.instance.Colorize();
        Destroy(gameObject);
    }
}
