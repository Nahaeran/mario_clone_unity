using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flag_Interaction : MonoBehaviour
{
    public GameObject PlayerObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // 캐릭터의 상태를 ending으로 변경            //obj = GameObject.Find("Player");

            //Application.LoadLevel("Gameover");
            //PlayerObject.GetComponent<state> = AutomateMovement.Enemystate.ending;

            Debug.Log("success");

            //obj = other.gameObjectGam.GetComponent<AutomateMovement>();
            //obj.state = AutomateMovement.Enemystate.ending;

            // 캐릭터를 물체의 맨 위로 옮기기
            Vector3 pos = transform.position;
            other.GetComponent<Transform>().position = new Vector3(pos.x, pos.y + transform.localScale.y / 2, pos.z);
            other.GetComponent<AutomateMovement>().state = AutomateMovement.Enemystate.dead;
            other.GetComponent<PlayerScript>().moveX = 0;

            // 1초 후 게임 오버 화면으로 이동
            StartCoroutine(LoadGameOverScene());
        }
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(5.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
    }
}


//{
//    GameObject obj;
//    // Start is called before the first frame update
//    void OnTriggerEnter2D(Collider2D other){
//        if (other.tag == "Player")
//        {
//            //Application.LoadLevel('')
//            // 게임 종료 화면으로 이동
//            // 플레이어 잡음
//            obj = GameObject.Find("Player");
//            obj.GetComponent<AutomateMovement>().state = AutomateMovement.Enemystate.ending;
//            Vector3 pos;
//            pos = transform.localPosition;
//            pos.y -= transform.localScale.y / 2;

//            obj.transform.localPosition = pos;
//            new WaitForSeconds(1);


//            //UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
//        }
//    }
//}
