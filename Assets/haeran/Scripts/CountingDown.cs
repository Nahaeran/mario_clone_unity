using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CountingDown : MonoBehaviour
{
    public Text timerTxt;
    public float time = 9f;
    public float selectCountdown;
    public GameObject BlackScreen;
    public GameObject GameOverText;
    // Start is called before the first frame update
    void Start()
    {
        selectCountdown = time;
    }

    void End_time()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Floor(selectCountdown) <= 0)
        {
            BlackScreen.active = true;
            GameOverText.active = true;
            Invoke("End_time", 3);
        }
        else
        {
            selectCountdown -= Time.deltaTime;
            timerTxt.text = Mathf.Floor(selectCountdown).ToString();
            // Debug.Log(selectCountdown);
        }
    }
}
