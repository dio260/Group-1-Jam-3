using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject roomObjects;
    public GameObject ActivateItems;

    public GameObject finalItem;


    [SerializeField]
    private AudioSource BGM;
    [SerializeField]
    private AudioSource dialogue;

    [SerializeField]
    private AudioClip[] dialogueLines;

    [SerializeField]
    private AudioClip[] respawnLines;

    private SkinnedMeshRenderer[] coworkers;

    private MeshRenderer[] skins;

    public GameObject player;
    private Vector3 playerStart;

    public TMP_Text playerHUD;
    public GameObject enemyGroup;
    private EnemyAI[] enemies;



    public TMP_Text finalRoomText;

    [Header("NPC Materials")]
    public Material skinColor;
    public Material pants;
    public Material undershirt;
    public Material shirt;
    public Material belt;
    public Material tie;

    private Material[] coworkerMats = new Material[6];

    private bool firstUpdate;

    private bool mute;

    [HideInInspector]
    public bool soundEnabled, objectsShown, colored, AIenabled, attackEnabled, last;

    private System.Random random;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

    }
    void Start()
    {
        mute = false;

        random = new System.Random();

        playerStart = player.transform.position;

        colored = false;
        attackEnabled = false;
        soundEnabled = false;
        objectsShown = false;
        AIenabled = false;
        last = false;

        coworkers = roomObjects.GetComponentsInChildren<SkinnedMeshRenderer>();

        skins = roomObjects.GetComponentsInChildren<MeshRenderer>();

        //set textures to none
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

        enemies = enemyGroup.GetComponentsInChildren<EnemyAI>();
        foreach (EnemyAI script in enemies)
        {
            script.enabled = false;
        }


        roomObjects.SetActive(false);
        ActivateItems.SetActive(false);
        finalItem.SetActive(false);

        coworkerMats[0] = skinColor;
        coworkerMats[1] = pants;
        coworkerMats[2] = undershirt;
        coworkerMats[3] = shirt;
        coworkerMats[4] = belt;
        coworkerMats[5] = tie;

        StartCoroutine(StartUp());
        dialogue.Play();
    }

    void Update()
    {
        string HUDText = "Click on certain items to pick them up.";

        if (attackEnabled)
        {
            HUDText += "\nClick on coworkers to stun them temporarily.";
        }

        if (mute)
        {
            HUDText += "\n Press E to unmute dialogue";
        }
        else
        {
            HUDText += "\n Press E to mute dialogue";
        }

        HUDText += "\n Press Q to toggle this text on and off";

        playerHUD.text = HUDText;

        if (attackEnabled && colored && soundEnabled && AIenabled && attackEnabled && !last)
        {
            finalItem.SetActive(true);
            foreach (EnemyAI script in enemies)
            {
                script.maxDistance *= 5;
            }
            last = false;
        }

        if (attackEnabled && colored && soundEnabled && AIenabled && attackEnabled)
        {
            finalRoomText.text = "You've gathered all the components and made the game whole! \nGrab the candy bar and enjoy it as you publish your game to the world.";

        }
        else
        {
            finalRoomText.text = "Looks like you still need to find a few more things to complete the game. \nTry searching high and low.";
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerHUD.enabled = !playerHUD.enabled;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!mute)
            {
                StopAllCoroutines();
                dialogue.Stop();
            }
            mute = !mute;
        }
    }

    public void ResetPositions()
    {
        player.transform.position = playerStart;
        foreach (EnemyAI script in enemies)
        {
            script.ResetState();
        }
        if (!mute)
        {
            StopAllCoroutines();
            dialogue.clip = respawnLines[random.Next(3)];
            dialogue.Play();
        }

    }

    public void ActivateSound()
    {
        BGM.Play();
        soundEnabled = true;
        if (!mute)
        {
            StopAllCoroutines();
            dialogue.clip = dialogueLines[2];
            dialogue.Play();
        }

    }

    public void ShowRoom()
    {
        roomObjects.SetActive(true);
        ActivateItems.SetActive(true);
        objectsShown = true;
        if (!mute)
        {
            StopAllCoroutines();
            StartCoroutine(BlueprintDialogue());
        }

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

        if (!mute)
        {
            StopAllCoroutines();
            dialogue.clip = dialogueLines[7];
            dialogue.Play();
        }

    }

    public void StartAI()
    {
        foreach (EnemyAI script in enemies)
        {
            script.enabled = true;
        }
        AIenabled = true;

        if (!mute)
        {
            StopAllCoroutines();
            dialogue.clip = dialogueLines[6];
            dialogue.Play();
        }

    }

    public void EnableAttack()
    {
        attackEnabled = true;
        if(!mute)
        {
            StopAllCoroutines();
            StartCoroutine(CashDialogue());
        }

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
        dialogue.clip = dialogueLines[4];
        dialogue.Play();
        yield return new WaitForSeconds(5f);
        dialogue.clip = dialogueLines[5];
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
