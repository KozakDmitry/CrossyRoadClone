using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public enum SwipeDir
    {
        Up,
        Down,
        Left,
        Right
    }
    private bool Moving = false;
    private Vector2 fingerUpPos;
    private Vector2 fingerDownPos;
    [SerializeField]
    private GameManager gameManager;
    private MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    public void KillAnimation()
    {
        mesh.material.DOFade(0, 0.3f);
        StartCoroutine(Death());
        

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
        gameManager.LoseGame();
    }
    void Update()
    {

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !Moving)
            {

                if (transform.position.z % 1 != 0)
                {
                    MovePlayer(new Vector3(0, 0, 1));
                }

            }
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }
        }



        //if (Input.GetKeyDown(KeyCode.W)&&!Moving)
        //{
        //    if (gameManager.AttemptToMove(new Vector3(1, 0, 0)))
        //    {
        //        MovePlayer(new Vector3(1, 0, 0));
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.A)&&!Moving)
        //{
        //    if (gameManager.AttemptToMove(new Vector3(0, 0, 1)))
        //    {
        //        MovePlayer(new Vector3(0, 0, 1));
        //    }
                
        //}
        //if (Input.GetKeyDown(KeyCode.D) && !Moving)
        //{
        //    if (gameManager.AttemptToMove(new Vector3(0, 0, -1)))
        //    {
        //        MovePlayer(new Vector3(0, 0, -1));
        //    }
               
        //}
        //if (Input.GetKeyDown(KeyCode.S) && !Moving)
        //{
        //    if (gameManager.AttemptToMove(new Vector3(-1, 0, 0)))
        //    {
        //        MovePlayer(new Vector3(-1, 0, 0));
        //    }
               
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().IsItRift())
            {
                transform.parent = collision.collider.transform;
            }
                    
        }
        if(collision.collider.gameObject.tag == "Collectable")
        {
            gameManager.ChangeCoins();
            collision.collider.gameObject.GetComponent<StaticObject>().Destroy();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().IsItRift())
            {
                transform.parent = null;
            }

        }
    }


    private void MovePlayer(Vector3 movement)
    {
        transform.DOMove(movement+transform.position, 0.3f, false);
        //transform.position += movement;
    }
    private void DetectSwipe()
    {
        if (SwipeDistCheck())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPos.y - fingerUpPos.y > 0 ? SwipeDir.Up : SwipeDir.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPos.x - fingerUpPos.x > 0 ? SwipeDir.Right : SwipeDir.Left;
                SendSwipe(direction);
            }
            fingerDownPos = fingerUpPos;
        }
    }
    private bool IsVerticalSwipe()
    {
        return VerticalMoveDistance() > HorizontalMoveDistance();
    }
    private bool SwipeDistCheck()
    {
        return VerticalMoveDistance() > 2f || HorizontalMoveDistance() > 2f;
    }
    private float VerticalMoveDistance()
    {
        return Mathf.Abs(fingerUpPos.y - fingerDownPos.y);
    }
    private float HorizontalMoveDistance()
    {
        return Mathf.Abs(fingerUpPos.x - fingerDownPos.x);
    }
    private void SendSwipe(SwipeDir direction)
    {
        switch (direction)
        {
            case SwipeDir.Up:
                MovePlayer(new Vector3(1, 0, 0));
                break;
            case SwipeDir.Down:
                MovePlayer(new Vector3(-1, 0, 0));
                break;
            case SwipeDir.Left:
                MovePlayer(new Vector3(0, 0, 1));
                break;
            case SwipeDir.Right:
                MovePlayer(new Vector3(0, 0, -1));
                break;
            default:
                break;
        }
    }
}
