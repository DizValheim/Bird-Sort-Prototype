using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BirdType
{
    Blue,
    Green,
    Red
}

public class Bird : MonoBehaviour
{
    public BirdType birdType;
    public bool isSelected;
    public Branch currBranch;
    public int birdNumber;

    void Start()
    {
        currBranch = GetComponentInParent<Branch>();
        birdNumber = currBranch.birds.FindIndex(item => item == this) + 1;
    }


}
