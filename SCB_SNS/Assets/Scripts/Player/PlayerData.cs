
using System.Collections;
using UnityEngine;
using TMPro;
using System;















public class PlayerData :MonoBehaviour
{
    private int score = 0;
    public int Health;
    private int _health;
    public bool isDead;
    private SpriteRenderer _spriteRenderer;
    public Color DefaultColor,HitColor;

    public TMP_Text _BulletCountText;
    public Transform WeaponController;
    public bool isHit;


//武器
    public int BulletCount;



     void Awake()
         {
        _spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        }

    private void OnEnable() {
        GameEventHandler.OnPlayerDeath.AddListener(Dead);


    }

    private void Start() {


        //初始化UI
        UIManager.Instance.UpdateUI("Score",score);
        _health=Health;
        UIManager.Instance.UpdateHealthUI(_health);
        _spriteRenderer.color=DefaultColor;
        isDead=false;
        _BulletCountText.text=BulletCount.ToString();
    }
    private void Update()
    {
        Dead();

    }
    void Dead()
    {
        if((_health<=0)&&!isDead)//生命值为0死亡
        {
            isDead=true;
            _health=0;
            // BulletCount=0;
            GameEventHandler.PlayerDeath();
            GameEventHandler.OnexpsionRange.Invoke(transform.position);
            this.transform.parent. gameObject. SetActive(false);
            Debug.Log("You are die");
        }
    }
    /// <summary>
    /// 碰撞获得武器的子弹数目
    /// </summary>
    /// <param name="ShootBulletCount"></param>
    public void MinusBullet(int ShootBulletCount)
    {
        BulletCount-=ShootBulletCount;
        _BulletCountText.text=BulletCount.ToString();
    }
    public void AddBullet(int AddBulletCount)
    {
        BulletCount+=AddBulletCount;
        _BulletCountText.text=BulletCount.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            // 如果碰撞到Pill，增加分数并更新UI
            score++;
            UIManager.Instance.UpdateUI("Score",score);
        }
        if(other.CompareTag("Enemy"))
        {
            _health--;
            UIManager.Instance.UpdateHealthUI(_health);
            StartCoroutine(Hit());
            ScreenShake.Instance.StopTime(0.05f,1,.1f);
            ScreenShake.Instance.CameraShake(5,new Vector2(.2f,.2f),10);

        }
    }
     IEnumerator Hit()
    {
        isHit=true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < 10; i++)
        {
            SpriteRenderer spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = HitColor;;
            yield return new WaitForSeconds(.05f);
            spriteRenderer.color = DefaultColor;
            yield return new WaitForSeconds(.05f);
        }
        transform.localScale = (Vector2)Vector3.one;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        isHit=false;

    }
       private void OnDisable() {

        GameEventHandler.OnPlayerDeath.RemoveListener(Dead);


      }


}
