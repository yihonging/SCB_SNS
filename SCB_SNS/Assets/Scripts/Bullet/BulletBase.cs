using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;










public class BulletBase : MonoBehaviour
{

    public BulletSO _bulletSO;
    private float MoveSpeed;
    private float Damage;
    private float DisTime;
    private float HitForce;
    private GameObject DisVFX;
    public bool isPassThroughtEnemy;

    protected virtual void OnEnable() {
        this.gameObject.SetActive(true);
        MoveSpeed=_bulletSO.Movespeed;
        Damage=_bulletSO.Damage;
        DisTime=_bulletSO.BulletDisTime;
        HitForce=_bulletSO.HitForce;
        DisVFX=_bulletSO.DisVFX;
    }
    // Start is called before the first frame update
  protected virtual  void Start()
    {

    }

    // Update is called once per frame
  protected virtual  void Update()
    {
        BulletMove();
        SetDisBullet();
    }
    void BulletMove()
    {
        transform.Translate(Vector2.up*MoveSpeed*Time.deltaTime);
    }
    protected virtual void SetDisBullet()
    {
        DisTime-=Time.deltaTime;
        if(DisTime<=0)
        {
            DisBullet();
            DisBulletVFX();
        }
    }


   protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            IDamage idamage=collision.gameObject.GetComponentInParent<IDamage>();
            idamage.TakeDamage(Damage);
            if(!isPassThroughtEnemy)
            {
            this.gameObject.SetActive(false);
            Vector2 hitbackPos = collision.transform.position - transform.position;
            collision.transform.parent.DOMove(new Vector2(
            collision.transform.position.x ,
            collision.transform.position.y + hitbackPos.y / HitForce), .1f).SetEase(Ease.Linear);
             DisBulletVFX();
            }
        }
        if(collision.CompareTag("Wall"))
        {
            DisBullet();
        }
    }
    void DisBulletVFX()
    {
        if(DisVFX==null)
        {
            return;
        }
        if(DisVFX.activeSelf)
        {
        GameObject disvfx=PoolManager.Release(DisVFX,transform.position,Quaternion.identity);
        disvfx.GetComponent<VfxDis>().DisVFX(1f);
        }
    }
   protected  void DisBullet()
    {
         this.gameObject.SetActive(false);

    }
   protected virtual  void OnDisable() {
        DisBullet();
    }
}
