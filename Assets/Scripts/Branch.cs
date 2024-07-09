using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public bool isLeftBranch = false;
    public bool isFull;
    public List<Bird> birds;

    void Update()
    {
        isFull = birds.Count == 4;
        if(isFull)
            if(CheckBirdMatching())
                Destroy(gameObject); 
    }

    public bool CheckBirdMatching()
    {
        if (birds.Count != 0)
        {
            bool isMatching = false;
            Bird bird = birds[0];
            foreach (Bird otherBird in birds)
            {
                isMatching = bird.birdType == otherBird.birdType;
            }
            
            return isMatching;
        }
        else
            return true;
    }
    public bool CheckBirdMatching(Bird bird)
    {
        if (birds.Count != 0)
        {
            bool isMatching = false;
            foreach (Bird otherBird in birds)
            {
                isMatching = bird.birdType == otherBird.birdType;
            }
            
            return isMatching;
        }
        else
            return true;
    }

}
