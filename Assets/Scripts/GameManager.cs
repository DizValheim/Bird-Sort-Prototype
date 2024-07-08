using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Bird selectedBird;
    public Branch selectedBranch;

    public float[] birdYPositions = { -21f, -7f, 7f, 21f };


    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.TryGetComponent<Bird>(out Bird bird))
                    {   
                        if(bird.currBranch.birds.Count == bird.birdNumber)
                        {
                            selectedBird = bird;
                            selectedBird.isSelected = true;
                        }
                        else
                            Debug.Log("Not outermost bird");
                    }
                    else if (hit.collider.gameObject.TryGetComponent<Branch>(out Branch branch))
                    {
                        // Reparent the selected bird to the branch
                        if (selectedBird != null)
                        {
                            selectedBranch = branch;

                            if(selectedBranch.birds.Count <= 3 && selectedBird != selectedBird.currBranch)
                            {
                                StartMoveSequence();
                            }
                            else
                            {
                                Debug.Log("Branch Full!");
                                //Check for same birds
                                //Destroy Branch
                            }

                            ResetBranchData();
                            
                        }
                    }
                }
            }
        }
    }

    private void StartMoveSequence()
    {
        //Change Parent
        selectedBird.transform.SetParent(selectedBranch.transform);

        //Add to the list of branches and Set bird number
        selectedBranch.birds.Add(selectedBird);
        selectedBird.currBranch.birds.Remove(selectedBird);
        
        selectedBird.currBranch = selectedBranch;
        selectedBird.birdNumber = selectedBranch.birds.Count;

        SetBirdPositionAndRotation();
        ResetBirdData();
    }

    private void ResetBranchData()
    {
        selectedBranch = null;
    }

    private void ResetBirdData()
    {
        selectedBird.isSelected = false;
        selectedBird = null;
    }

    private void SetBirdPositionAndRotation()
    {   
        //Play Animation
        if(selectedBranch.isLeftBranch)
            selectedBird.transform.localPosition = new(-15, birdYPositions[selectedBird.birdNumber - 1]);
        else
            selectedBird.transform.localPosition = new(-15, -birdYPositions[selectedBird.birdNumber - 1]);
        selectedBird.GetComponent<SpriteRenderer>().flipX = selectedBranch.isLeftBranch;
        
    }

    
}
