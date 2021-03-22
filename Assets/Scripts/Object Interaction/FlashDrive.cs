using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDrive : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameController.instance.StartAI();
        Destroy(gameObject);
    }
}
