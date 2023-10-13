using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingDown : MonoBehaviour
{
    public Text timerTxt;
    public float time = 9f;
    public float selectCountdown;
    // Start is called before the first frame update
    void Start()
    {
        selectCountdown = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Floor(selectCountdown) <= 0)
        {
            //카운트 끝났을때 호출할 함수 만들어서 호출해야 함
        }
        else
        {
            selectCountdown -= Time.deltaTime;
            timerTxt.text = Mathf.Floor(selectCountdown).ToString();
            // Debug.Log(selectCountdown);
        }
    }
}
