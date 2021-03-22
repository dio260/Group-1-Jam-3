using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameController.instance.WinGame();
        Destroy(gameObject);
    }
}