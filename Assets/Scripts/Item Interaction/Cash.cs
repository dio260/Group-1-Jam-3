using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameController.instance.EnableAttack();
        Destroy(gameObject);
    }
}
