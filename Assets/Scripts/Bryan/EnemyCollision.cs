using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    //private GameObject player;
    private Vector3 startPos = new Vector3(0, 1.52f, -15f);

   /* void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
            other.transform.position = startPos;
    }*/

    void OnTriggerEnter(Collider other)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
