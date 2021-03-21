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

    public SkinnedMeshRenderer[] coworkers;

    public MeshRenderer[] skins;

    void Start()
    {
        firstItem = false;
        secondItem = false;
        thirdItem = false;
        fourthItem = false;
        fifthItem = false;
        sixthItem = false;

        //roomObjects.SetActive(false);

        coworkers = roomObjects.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach(SkinnedMeshRenderer skin in coworkers)
        {
            skin.materials = new Material[6];
            skin.materials[0] = null;
            skin.materials[1] = null;
            skin.materials[2] = null;
            skin.materials[3] = null;
            skin.materials[4] = null;
            skin.materials[5] = null;
            
        }

        skins = roomObjects.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer mesh in skins)
        {
            mesh.materials = new Material[1];
            mesh.materials[0] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSound()
    {
        
        firstItem = true;
    }

    public void ShowRoom()
    {
        roomObjects.SetActive(true);
    }

    public void Colorize()
    {

    }
}
