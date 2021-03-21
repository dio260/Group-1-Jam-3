using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameController.instance.ShowRoom();
        Destroy(gameObject);
    }
}
