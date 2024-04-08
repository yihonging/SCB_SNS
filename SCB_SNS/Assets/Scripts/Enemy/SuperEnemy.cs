using System.Collections;
using System.Collections.Generic;
using UnityEngine;











public class SuperEnemy : EnemyBase
{
    public Color color1 ; // ��һ����ɫ
    public Color color2 ; // �ڶ�����ɫ
    public float blinkDuration = 1f; // ��˸�ĳ���ʱ��
    private float timeElapsed = 0f;
    public Transform ShootPoint;
    // Start is called before the first frame update

    protected override void Awake() {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

    }

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
  protected override  void Update()
    {
        base.Update();
        Blink();
    }

    void Blink()
    {
        Color currentColor = Color.Lerp(color1, color2, Mathf.PingPong(timeElapsed / blinkDuration, 1));
            spriteRenderer.color = currentColor;
            timeElapsed += Time.deltaTime;
    }
     void SpwanPoint()
    {
         SpwanEnemy creatEnemy=FindObjectOfType<SpwanEnemy>();
         this.transform.position=creatEnemy.GenPoint();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("DisObj"))
        {
           if(StateIndex<_EnemySO.Length-1)
           {
           StateIndex++;
           UpdateState();
           }
           SpwanPoint();
        }
    }


}
