using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressItems : MonoBehaviour
{

    public GameObject floppy, brush, usb, blueprint, cash;
    // Start is called before the first frame update
    void Start()
    {
        floppy.SetActive(false);
        brush.SetActive(false);
        usb.SetActive(false);
        blueprint.SetActive(false);
        cash.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameController.instance.colored && !brush.activeInHierarchy)
        {
            brush.SetActive(true);
        }

        if(GameController.instance.soundEnabled && !floppy.activeInHierarchy)
        {
            floppy.SetActive(true);
        }

        if(GameController.instance.attackEnabled && !cash.activeInHierarchy)
        {
            cash.SetActive(true);
        }

        if(GameController.instance.objectsShown && !blueprint.activeInHierarchy)
        {
            blueprint.SetActive(true);
        }

        if(GameController.instance.AIenabled && !usb.activeInHierarchy)
        {
            usb.SetActive(true);
        }
    }
}
