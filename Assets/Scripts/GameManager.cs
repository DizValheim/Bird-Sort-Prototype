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
            HandleTouchInput();
        }
    }

    void HandleTouchInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<Bird>(out Bird tappedBird))
                {
                    HandleBirdSelection(tappedBird);
                }
                else if (hit.collider.gameObject.TryGetComponent<Branch>(out Branch tappedBranch))
                {
                    HandleBranchSelection(tappedBranch);
                }
            }
        }
    }

    void HandleBirdSelection(Bird bird)
    {
        if (bird.currBranch.birds.Count == bird.birdNumber)     //To check if the bird is outermost
        {
            selectedBird = bird;
            selectedBird.isSelected = true;
        }
        else
        {
            Debug.Log("Not outermost bird");
        }
    }

    void HandleBranchSelection(Branch branch)
    {
        if (selectedBird != null)
        {
            selectedBranch = branch;

            if (!selectedBranch.isFull )
            {
                if (selectedBranch.CheckBirdMatching(selectedBird))
                    StartMoveSequence();
            }
            else
            {
                Debug.Log("Branch Full!");
                // Handle branch full scenario (e.g., display a message or destroy the branch)
            }

            ResetBranchData();
        }
    }

    void StartMoveSequence()
    {
        selectedBird.transform.SetParent(selectedBranch.transform);
        selectedBranch.birds.Add(selectedBird);
        selectedBird.currBranch.birds.Remove(selectedBird);
        selectedBird.currBranch = selectedBranch;
        selectedBird.birdNumber = selectedBranch.birds.Count;
        SetBirdPositionAndRotation();
        ResetBirdData();
    }

    void ResetBranchData()
    {
        selectedBranch = null;
    }

    void ResetBirdData()
    {
        selectedBird.isSelected = false;
        selectedBird = null;
    }

    void SetBirdPositionAndRotation()
    {
        float xPosition =  -15f;
        float yPosition = (selectedBranch.isLeftBranch ? 1 : -1) * birdYPositions[selectedBird.birdNumber - 1];
        selectedBird.transform.localPosition = new Vector3(xPosition, yPosition, 0f);
        selectedBird.GetComponent<SpriteRenderer>().flipX = selectedBranch.isLeftBranch;
    }
}
