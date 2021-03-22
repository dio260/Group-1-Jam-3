using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject roomObjects;
    public GameObject ActivateItems;


    [SerializeField]
    private AudioSource BGM;
    [SerializeField]
    private AudioSource dialogue;

    [SerializeField]
    private AudioClip[] dialogueLines;

    [SerializeField]
    private AudioClip[] respawnLines;

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

    [HideInInspector]
    public bool colored;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

    }
    void Start()
    {

        coworkers = roomObjects.GetComponentsInChildren<SkinnedMeshRenderer>();

        skins = roomObjects.GetComponentsInChildren<MeshRenderer>();

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

        roomObjects.SetActive(false);
        ActivateItems.SetActive(false);

        coworkerMats[0] = skinColor;
        coworkerMats[1] = pants;
        coworkerMats[2] = undershirt;
        coworkerMats[3] = shirt;
        coworkerMats[4] = belt;
        coworkerMats[5] = tie;

        StartCoroutine(StartUp());
        dialogue.Play();
    }

    public void ResetPositions()
    {

    }

    public void ActivateSound()
    {
        BGM.Play();
        StopAllCoroutines();
        dialogue.clip = dialogueLines[2];
        dialogue.Play();
    }

    public void ShowRoom()
    {
        roomObjects.SetActive(true);
        ActivateItems.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(BlueprintDialogue());
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
        colored = true;
    }

    public void StartAI()
    {
        dialogue.clip = dialogueLines[6];
        dialogue.Play();
    }

    public void EnableAttack()
    {

        StopAllCoroutines();
        StartCoroutine(CashDialogue());
    }

    public void WinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator StartUp()
    {
        dialogue.clip = dialogueLines[0];
        dialogue.Play();
        yield return new WaitForSeconds(6.5f);
        dialogue.clip = dialogueLines[1];
        dialogue.Play();
    }

    IEnumerator BlueprintDialogue()
    {
        dialogue.clip = dialogueLines[3];
        dialogue.Play();
        yield return new WaitForSeconds(2.5f);
        dialogue.clip = dialogueLines[4];
        dialogue.Play();
    }

    IEnumerator CashDialogue()
    {
        dialogue.clip = dialogueLines[8];
        dialogue.Play();
        yield return new WaitForSeconds(3.5f);
        dialogue.clip = dialogueLines[9];
        dialogue.Play();
    }

}
