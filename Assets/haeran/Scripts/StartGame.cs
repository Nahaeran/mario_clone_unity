using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OnClick(string scenename)
    {
        // Debug.Log("button click!");
        SceneManager.LoadScene(scenename);
    }
}
