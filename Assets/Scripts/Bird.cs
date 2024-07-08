using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isSelected;
    public Branch currBranch;
    Branch destBranch;
    public int birdNumber;

    void Start()
    {
        currBranch = GetComponentInParent<Branch>();
        birdNumber = currBranch.birds.FindIndex(item => item == this) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawOutline()
    {
        
    }

}
