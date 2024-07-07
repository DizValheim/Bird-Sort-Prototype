using TMPro;
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
                        selectedBird = bird;
                        selectedBird.isSelected = true;
                    }
                    else if (hit.collider.gameObject.TryGetComponent<Branch>(out Branch branch))
                    {
                        // Reparent the selected bird to the branch
                        if (selectedBird != null)
                        {
                            selectedBranch = branch;

                            if(selectedBranch.birds.Count <= 4)
                            {
                                //Change Parent
                                selectedBird.transform.SetParent(selectedBranch.transform);

                                //Add to the list of branches and Set bird number
                                selectedBranch.birds.Add(selectedBird);
                                selectedBird.birdNumber = selectedBranch.birds.Count + 1;

                                SetBirdPositionAndRotation();
                            }
                            else
                            {
                                Debug.Log("Branch Full!");
                                //Destroy Branch
                            }


                            selectedBird = null;
                            selectedBranch = null;

                            // Set the bird's local position within the branch
                            // based on your fixed positions
                            // e.g., selectedBird.transform.localPosition = ...
                        }
                    }
                }
            }
        }
    }

    private void SetBirdPositionAndRotation()
    {   
        //Play Animation
        selectedBird.transform.localPosition = new(-15, birdYPositions[selectedBird.birdNumber - 2]);
        if (selectedBranch.isLeftBranch)
        {
            // Debug.Log("Flipped");
            selectedBird.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
