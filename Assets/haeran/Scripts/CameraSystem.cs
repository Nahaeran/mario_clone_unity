using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        // player로 태그된 오브젝트 찾음
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Mathf.Clamp : 최소, 최대값을 설정하여 float값이 범위외의 값을 넘지 않도록 송
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }

    // public void SetResolution()
    // {
    //     int setWidth = 960;
    //     int setHeight = 540;

    //     int deviceWidth = Screen.width;
    //     int deviceHeight = Scree
    // }
}
