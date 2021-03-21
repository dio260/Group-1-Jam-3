using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
    private bool firstItem;
    private bool secondItem;
    private bool thirdItem;
    private bool fourthItem;
    private bool fifthItem;
    private bool sixthItem;
    */

    public static GameController instance;

    public GameObject roomObjects;


    [SerializeField]
    private AudioSource BGM;
    [SerializeField]
    private AudioSource dialogue;

    public SkinnedMeshRenderer[] coworkers;

    public MeshRenderer[] skins;

    [Header("NPC Materials")]
    public Material skinColor;
    public Material pants;
    public Material undershirt;
    public Material shirt;
    public Material belt;
    public Material tie;


    private Material[] coworkerMats = new Material[6];

    private bool firstUpdate;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {

        //roomObjects.SetActive(false);

        coworkerMats[0] = skinColor;
        coworkerMats[1] = pants;
        coworkerMats[2] = undershirt;
        coworkerMats[3] = shirt;
        coworkerMats[4] = belt;
        coworkerMats[5] = tie;

        coworkers = roomObjects.GetComponentsInChildren<SkinnedMeshRenderer>();

        skins = roomObjects.GetComponentsInChildren<MeshRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!firstUpdate)
        {
            foreach (SkinnedMeshRenderer skin in coworkers)
            {
                skin.materials = new Material[6];
                skin.materials[0] = null;
                skin.materials[1] = null;
                skin.materials[2] = null;
                skin.materials[3] = null;
                skin.materials[4] = null;
                skin.materials[5] = null;

            }

            foreach (MeshRenderer mesh in skins)
            {
                mesh.materials = new Material[1];
                mesh.materials[0] = null;
            }
            firstUpdate = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Colorize();
        }
    }

    public void ActivateSound()
    {


    }

    public void ShowRoom()
    {
        roomObjects.SetActive(true);
    }

    public void Colorize()
    {

        foreach (SkinnedMeshRenderer skin in coworkers)
        {
            skin.materials = coworkerMats;

        }

        foreach (MeshRenderer mesh in skins)
        {
            if (mesh.gameObject.GetComponent<MaterialStorage>() != null)
            {
                mesh.materials = mesh.gameObject.GetComponent<MaterialStorage>().mat;
            }

        }


    }

    public void StartAI()
    {

    }

    public void WinGame()
    {

    }
}
