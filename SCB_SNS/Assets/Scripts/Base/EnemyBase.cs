using System.Collections;
using System.Collections.Generic;
using UnityEngine;











public class EnemyBase : MonoBehaviour,IDamage
{
    public EnemySO[] _EnemySO;
    public int StateIndex;
    protected EnemySO CurEnemyState;
    private float Health;
    private float speed ;
    private float EnemyScale;
    [SerializeField]
    private Color NormalColor,HitColor;
    protected SpriteRenderer  spriteRenderer;



    public float chaseRadius = 10f;
    public float separationDistance = 2f;
    public float yDistanceThreshold = 3f;
    private Transform player;
    public GameObject Vfx;
    protected bool _isDead;

    protected virtual void Awake() {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }

   protected virtual  void OnEnable()
    {
        this.gameObject.SetActive(true);
        _isDead=false;
        StateIndex=0;
        CurEnemyState = _EnemySO[StateIndex];
        UpdateState();
        SetNormalColor();
        GameEventHandler.OnPlayerDeath.AddListener(Dead);
        // GameEventHandler.OnCombo.AddListener(Dead);


    }

   protected virtual void Start()
    {


        player = GameObject.FindGameObjectWithTag("Player")?.transform;


    }

 protected virtual  void Update()
    {
        if(player==null)
        {
            return;
        }


       // �������ͷ���
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float yDistance = transform.position.y - player.position.y;


         // �����Һ͵��˵�Y��������3������£�������ǰ�ƶ�
        if (yDistance < yDistanceThreshold)
        {
            MoveForward();
        }
        // �����׷��뾶�ڣ�׷�����
        else if (distanceToPlayer < chaseRadius)
        {
            // �ƶ�������ҵķ���
            MoveTowards(directionToPlayer);

            // ����λ���Ա���������������ײ
            AdjustPosition();
        }
        if(Health<=0&&!_isDead)
        {
            Health=0;
            _isDead=true;
            StateIndex=0;
            Dead();
            GameEventHandler.Combo();

        }
    }


  protected virtual void UpdateState()
    {
        CurEnemyState = _EnemySO[StateIndex];
        Health=CurEnemyState.Health;
        speed=CurEnemyState.MoveSpeed;
        EnemyScale=CurEnemyState.Scale;
        gameObject.transform.localScale=Vector2.one*EnemyScale;

    }

    void MoveTowards(Vector2 direction)
    {
        // �򵥵�׷����Ϊ
        transform.Translate(direction * Time.deltaTime*speed);
    }
    void MoveForward()
    {
        // ��ǰ�ƶ�
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
   protected virtual void Dead()
    {

            this.gameObject.SetActive(false);
            if(Vfx.activeSelf)
            {
               GameObject VFX= PoolManager.Release(Vfx,transform.position,Quaternion.identity);
               VFX.GetComponent<VfxDis>().DisVFX(1f);
            }

    }
    public void TakeDamage(float Damage)
    {
        Health-=Damage;
    }

    void AdjustPosition()
    {
        // ��ȡ���������е���
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != gameObject)  // �����Լ�
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < separationDistance)
                {
                    // ����Զ���������˵ķ���
                    Vector2 separationDirection = (transform.position - enemy.transform.position).normalized;
                    // separationDirection = Vector2.ClampMagnitude(separationDirection, 1f);

                    // �ƶ�һ��ľ��룬�Ա���һ���ļ��
                    transform.Translate(separationDirection * Time.deltaTime);
                }
            }
        }
    }
     void SetNormalColor()
    {
    spriteRenderer.color=NormalColor;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("DisObj"))
        {
           gameObject.SetActive(false);
        }
        if(other.CompareTag("Bullet"))
        {
            spriteRenderer.color=HitColor;
            Invoke(nameof(SetNormalColor),.1f);
           

        }
    }
    private void OnDisable() {
         GameEventHandler.OnPlayerDeath.RemoveListener(Dead);
        //  GameEventHandler.OnCombo.RemoveListener(Dead);
    }

}
