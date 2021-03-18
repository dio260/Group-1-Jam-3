using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
    public bool CanFall { get; set; }
    [SerializeField] private float gravityConstant;
    private float gravity;

    private void Update()
    {
        
    }
}
