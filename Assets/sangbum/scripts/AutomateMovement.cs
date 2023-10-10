using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// fore


public class AutomateMovement : MonoBehaviour
{

    [SerializeField]
    // 바닥 없는곳에 닿을 경우 떨어질 중력 설정
    // Vector2를 이용한 속도 설정
    // 움직이는 방향에 대한 설정 
    public float gravity;
    public Vector2 velocity;
    public bool isWalkingLeft = true;
    public LayerMask floormask;
    public LayerMask Wallmask;
    public LayerMask Playermask;
    // 죽을 떄에 대한 내
    private bool shouldDie = false;
    private float deathTimer = 0;
    public float timeBeforeDestroy = 1.0f;
    

    // Layermask 라고 하는 이 요소를 public으로 하지 않으니 문제가 생겼다 유니티에서
    // floormask가 어디있냐고 찾는 모습을 보였는데 public으로 하지 않을시 다른 파일들이나 유니티에서
    // 안보이게 되는것이 아닐까 생각이 든다


    //BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

    public void Crush()
    {
        state = Enemystate.dead;

        //GetComponent<Animator>().SetBool("Is_dead", true);
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Is dead", true);
        GetComponent<Collider2D>().enabled = false;

        //spriteRenderer.sprite = newsprite;
        shouldDie = true;

    }
    void CheckCrushed()
    {
        if (shouldDie)
        {
            if (deathTimer <= timeBeforeDestroy)
            {
                deathTimer += Time.deltaTime;
            }
            else
            {
                shouldDie = false;
                Destroy(this.gameObject);
            }
        }
    }
    private bool grounded = false;
    public enum Enemystate
    {
        walking,
        falling,
        dead,
        ending
    }
    public Enemystate state = Enemystate.falling;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame

    void Update()
    {
        CheckCrushed();
        // 앞에서 설정한 state가 어떤 가에 따라서 다른 행동을 한다
        if (state != Enemystate.dead & state != Enemystate.ending)
        {
            // 현재의 위치를 파악하기위해 크기와, scale객체를 저장
            // transform.localScale
            // transform.localPosition
            Vector3 pos = transform.localPosition;
            Vector3 scale = transform.localScale;

            // 떨어진다면 
            if (state == Enemystate.falling)
            {
                // position과 velocity를 저장한다
                pos.y += velocity.y * Time.deltaTime;
                velocity.y -= gravity * Time.deltaTime;
            }

            if (state == Enemystate.walking)
            {
                // 왼쪽으로 움직이고 있다면 
                if (isWalkingLeft)
                {
                    pos.x -= velocity.x * Time.deltaTime;
                    scale.x = -1;
                }
                // 오른쪽으로 움직이고 있다면 
                else
                {
                    pos.x += velocity.x * Time.deltaTime;
                    scale.x = 1;
                }
            }
            // y의 속도가 0보다 적다면 == 떨어지는 중이라면 
            if (velocity.y < 0)
                // 밑에 뭐가 있는지를 확인
                pos = CheckGround(pos);

            // 왼쪽 , 오른쪽에 뭐가 있는지 확/
            CheckWalls(pos, scale.x);
            transform.localPosition = pos;
            transform.localScale = scale;

            CheckPlayer(pos);
            
        }


        //}
    }

    Vector3 CheckGround(Vector3 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - .5f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - .5f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - .5f);
        RaycastHit2D groundLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floormask);
        RaycastHit2D groundMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floormask);
        RaycastHit2D groundRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floormask);

        // 아래 3방향에 대해서 뭐라도 걸렸을 경우 
        if (groundLeft.collider != null || groundMiddle.collider != null || groundRight.collider != null)
        {

            RaycastHit2D hitRay = groundLeft;
            if (groundLeft)
            {
                hitRay = groundLeft;
            }
            else if (groundMiddle)
            {
                hitRay = groundMiddle;

            }
            else if (groundRight)
            {
                hitRay = groundRight;
            }
            // y의 위치를 collider의 위치, + 크기의 반 + 물체의 크기의 반을 더한다 
            pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 0.5f * 0.32f;
            //float highestY = Mathf.Max(groundLeft.point.y, groundMiddle.point.y, groundRight.point.y);

            // y의 위치를 가장 높은 지점으로 설정합니다.
            //pos.y = highestY + 0.5f * transform.localScale.y * worldScale.y;
            velocity.y = 0;
            state = Enemystate.walking;
        }

        else
        {
            // 아무것도 밑에 걸리지 않았는데 상태가 떨어지지 않는다면
            // 떨어지는 것으로 바꾼다.
            if (state != Enemystate.falling)
            {
                Fall();
            }
        }

        return pos;


    }
    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(5.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
    }

    void CheckWalls(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * 0.4f * 0.32f, pos.y);
        Vector2 originMiddle = new Vector2(pos.x + direction * 0.4f * 0.32f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * 0.4f * 0.32f, pos.y - .3f * 0.32f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, Wallmask);
        RaycastHit2D WallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, Wallmask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, Wallmask);

        // 3가지 방향에서 Ray를 쐈을 때 뭐가 걸렸을 경우
        if (wallTop.collider != null || WallMiddle.collider != null || wallBottom.collider != null)
        {

            RaycastHit2D hitRay = wallTop;
            if (wallTop)
            {
                hitRay = wallTop;
            }
            else if (WallMiddle)
            {
                hitRay = WallMiddle;
            }
            else if (wallBottom)
            {
                hitRay = wallBottom;
            }
            //Debug.Log(hitRay.collider.name);

            if (hitRay.collider.tag == "Player")
            {
                
                // Animator animator = GetComponent<Animator>();
                // animator.SetBool("Is dead", true);
                Debug.Log("발견");
                // state = Enemystate.dead;
                
                hitRay.collider.enabled = false;
                Destroy(hitRay.collider);
                // Crush();

                //LoadGameOverScene();
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Gameover");
            }
            isWalkingLeft = !isWalkingLeft;
        }

    }

    void CheckPlayer(Vector3 pos)
    {
        BoxCollider2D boxcollider = GetComponent<BoxCollider2D>();
        Vector2 size = boxcollider.size;
        Vector2 originLeft = new Vector2(pos.x - size.x / 3, pos.y + size.y / 2);
        Vector2 originCenter = new Vector2(pos.x, pos.y + size.y / 2);
        Vector2 originRight = new Vector2(pos.x + size.x / 3, pos.y + size.y / 2);
        //Debug.Log(originCenter);

        RaycastHit2D playerLeft = Physics2D.Raycast(originLeft, Vector2.up, 0.1f, Playermask);
        RaycastHit2D playerCenter = Physics2D.Raycast(originCenter, Vector2.up, 0.1f, Playermask);
        RaycastHit2D playerRight = Physics2D.Raycast(originRight, Vector2.up, 0.1f, Playermask);

        if (playerLeft.collider != null || playerCenter.collider != null || playerRight.collider != null)
        {
            RaycastHit2D hitRay = playerLeft;
            if (playerLeft)
            {
                hitRay = playerLeft;
            } else if (playerCenter) {
                hitRay = playerCenter;
            } else if (playerRight)
            {
                hitRay = playerRight;
            }
            // Debug.Log(hitRay.collider.tag);
            if (hitRay.collider.tag == "Player")
            {
                Debug.Log("발견 했다");
                state = Enemystate.dead;
                Crush();
            }

        }
    }


    private void OnBecameVisible()
    {
        enabled = true;
    }


    // 떨어질떄의 함수를 정함 - 영원히 떨어지는것을 방지하기위해 
    void Fall()
    {
        velocity.y = 0;

        state = Enemystate.falling;

        grounded = false;

    }
}
