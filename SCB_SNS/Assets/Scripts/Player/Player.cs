using System.Collections;
using System.Collections.Generic;
using UnityEngine;











public class Player : MonoBehaviour
{

    private   bool IsPressShooty;
    public float MoveSpeed;
    private bool TrunRight;
    public bool GameStart;
    public bool CanMove;
    private RotateOBJ _rotateObj;


    [SerializeField]
    private float Distance;
    [SerializeField]
    private LayerMask wall;
    public GameObject Flash;

    private void Awake()
    {

        _rotateObj=GetComponentInChildren<RotateOBJ>();
    }
    void Start()
    {
        GameStart=false;
        CanMove =true;
        UIManager.Instance.UpdateUI("MoveSpeed",MoveSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        Move();
        ChangeDirAndShoot();
        CheckWall();
    }

    void PlayerControl()
    {
        IsPressShooty=UserInput.Instance.shootyAndMove;
    }
    void Move()
    {
        if(GameStart&&CanMove)
        {
            if(TrunRight)
            {
                transform.Translate(Vector2.right*MoveSpeed*Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right*-MoveSpeed*Time.deltaTime);
            }
        }
    }
    void ChangeDirAndShoot()
    {
        if(IsPressShooty)
        {
            GameStart=true;
            TrunRight=!TrunRight;
            _rotateObj.rotateSpped*=-1;


        }
    }


    void CheckWall()
    {
        RaycastHit2D Hitwall= Physics2D.Raycast(transform.position,transform.right,Distance,wall);
        RaycastHit2D HitLeftwall= Physics2D.Raycast(transform.position,-transform.right,Distance,wall);
        Debug.DrawRay(transform.position,transform.right*Distance,Color.red);
        Debug.DrawRay(transform.position,-transform.right*Distance,Color.red);

        if(Hitwall.collider!=null)
        {
            TrunRight=false;
        }
        if(HitLeftwall.collider!=null)
        {
            TrunRight=true;
        }

    }


}
