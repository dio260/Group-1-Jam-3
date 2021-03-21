using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool firstItem;
    private bool secondItem;
    private bool thirdItem;
    private bool fourthItem;
    private bool fifthItem;
    private bool sixthItem;

    public GameObject roomObjects;

    private AudioSource bgm;
    private AudioSource dialogue;


    void Start()
    {
        firstItem = false;
        secondItem = false;
        thirdItem = false;
        fourthItem = false;
        fifthItem = false;
        sixthItem = false;

        roomObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSound()
    {
        
        firstItem = true;
    }
}
