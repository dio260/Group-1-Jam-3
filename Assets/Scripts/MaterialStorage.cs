using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStorage : MonoBehaviour
{
    // Start is called before the first frame update
    //[HideInInspector]
    public Material[] mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
