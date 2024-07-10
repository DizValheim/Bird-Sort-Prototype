using System.Collections;
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

            if (!selectedBranch.isFull)
            {
                if (selectedBranch.CheckBirdMatching(selectedBird))
                    StartMoveSequence();
            }
            else
            {
                Debug.Log("Branch Full!");
            }

            
        }
    }

    void StartMoveSequence()
    {
        selectedBird.transform.SetParent(selectedBranch.transform);
        selectedBird.currBranch.birds.Remove(selectedBird);
        selectedBird.currBranch = selectedBranch;
        selectedBranch.birds.Add(selectedBird);
        selectedBird.birdNumber = selectedBranch.birds.Count;
        StartCoroutine(SetBirdPositionAndRotation());
        selectedBird.GetComponent<SpriteRenderer>().flipX = selectedBranch.isLeftBranch;
        
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

    IEnumerator SetBirdPositionAndRotation()
    {
        Vector3 targetWorldPosition = selectedBranch.transform.TransformPoint(new Vector3(-17f, (selectedBranch.isLeftBranch ? 1 : -1) * birdYPositions[selectedBird.birdNumber - 1], 0f));

        float elapsedTime = 0f;
        float transitionDuration = 0.5f;
        
        Bird currentBird = selectedBird;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            currentBird.transform.position = Vector3.Lerp(currentBird.transform.position, targetWorldPosition, t);

            yield return null;
            
        }

        currentBird.transform.position = targetWorldPosition;

        ResetBirdData();
        if(selectedBranch.isFull && selectedBranch.CheckBirdMatching())
        {
            Destroy(selectedBranch.gameObject);
        }
        ResetBranchData();
    }
}
