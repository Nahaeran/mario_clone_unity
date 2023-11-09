using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automove : MonoBehaviour
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
    private bool grounded = false;
    public enum CURRENTSTATE
    {
        walking,
        falling,
        dead,
        ending
    }
    public CURRENTSTATE state = CURRENTSTATE.falling;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame

    void Update()
    {
        // 앞에서 설정한 state가 어떤 가에 따라서 다른 행동을 한다
        if (state != CURRENTSTATE.dead & state != CURRENTSTATE.ending)
        {
            // 현재의 위치를 파악하기위해 크기와, scale객체를 저장
            Vector3 pos = transform.localPosition;
            Vector3 scale = transform.localScale;

            // 떨어진다면 
            if (state == CURRENTSTATE.falling)
            {
                // position과 velocity를 저장한다
                pos.y += velocity.y * Time.deltaTime;
                velocity.y -= gravity * Time.deltaTime;
            }

            if (state == CURRENTSTATE.walking)
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

        }


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
            // y의 위치를 가장 높은 지점으로 설정합니다.
            velocity.y = 0;
            state = CURRENTSTATE.walking;
        }

        else
        {
            // 아무것도 밑에 걸리지 않았는데 상태가 떨어지지 않는다면
            // 떨어지는 것으로 바꾼다.
            if (state != CURRENTSTATE.falling)
            {
                Fall();
            }
        }
        return pos;
    }

    void CheckWalls(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * 0.4f * 0.32f, pos.y);
        Vector2 originMiddle = new Vector2(pos.x + direction * 0.4f * 0.32f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * 0.4f * 0.32f, pos.y - .3f * 0.32f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, Wallmask);
        RaycastHit2D WallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, Wallmask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, Wallmask);

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

            isWalkingLeft = !isWalkingLeft;
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

        state = CURRENTSTATE.falling;

        grounded = false;

    }
}
