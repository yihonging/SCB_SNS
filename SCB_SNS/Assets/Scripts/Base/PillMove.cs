using System.Collections;
using System.Collections.Generic;
using UnityEngine;















public class PillMove : MonoBehaviour
{

    public float MoveSpeed;
    public GameObject _smoke;

   protected void OnEnable()
     {
        this.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
  protected  void Update()
    {
        Movement();
    }
    void Movement()
    {
        transform.Translate(Vector2.down*MoveSpeed*Time.deltaTime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            this.gameObject.SetActive(false);
            WeaponManager _weaponManager=FindObjectOfType<WeaponManager>();
            _weaponManager.RandomSwitchWeapon();
            if(_smoke.activeSelf)
            {
               GameObject smoke= PoolManager.Release(_smoke,transform.position,Quaternion.identity);
               smoke.GetComponent<VfxDis>().DisVFX(1f);
            }
        }
        if(other.CompareTag("DisObj"))
        {
            this.gameObject.SetActive(false);
            SpwanEnemy creatEnemy=FindObjectOfType<SpwanEnemy>();
            creatEnemy?.SpwanSuperEnemy();
        }
    }
    private void OnDisable() {
        this.gameObject.SetActive(false);
    }
}
