using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isSelected;
    public Branch currBranch;
    Branch destBranch;
    public int birdNumber;

    // public int BirdNumber => birdNumber;
    // public void SetBirdNumber(int birdNumber)
    // {
    //     this.birdNumber = birdNumber;
    // }

    // Start is called before the first frame update
    void Start()
    {
        currBranch = GetComponentInParent<Branch>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckInput()
    {
        
    }

}
