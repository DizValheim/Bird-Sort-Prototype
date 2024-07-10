using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public Canvas overlay;

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void TurnOnOverlay()
    {
        overlay.gameObject.SetActive(true);
    }

}
