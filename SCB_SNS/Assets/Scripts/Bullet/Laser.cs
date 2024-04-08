using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Laser :WeaponBase
{
    public LineRenderer _linerenderer;
    public float initialWidth = 1f;
    public float finalWidth = 0.1f;
    public float duration = .1f;
    public float laserLength = 10f;
    private EdgeCollider2D edgeCollider2D;
    public BulletSO _bulletSO; // 子弹数据
    public int numSegments;
    private List<Vector2> points;





    protected override void Awake() {
      base.Awake();
        _linerenderer = GetComponent<LineRenderer>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();


    }
   protected override void OnEnable()
   {
        base.OnEnable();
         _linerenderer.enabled=false;
        edgeCollider2D.enabled = false;
        _linerenderer.startWidth = initialWidth;

    }
    protected override void Update() {
      ShootyControl();
      // NormalLaser();
      ElecLaser();
    }


  protected  void NormalLaser()
  {
    if(IsPressShooty)
    {
    _linerenderer.enabled=true;
    edgeCollider2D.enabled = true;
    _linerenderer.startWidth = initialWidth;
    _linerenderer.endWidth = initialWidth;
     ScreenShake.Instance.CameraShake(.1f,new Vector2(.1f,.1f),10);
     _playerData.MinusBullet(MinusBulletCount);


    List<Vector2> points = new();
     points.Clear();
    _linerenderer.SetPosition(0,_bulletSpawnPoint.position);
    _linerenderer.SetPosition(1,_bulletSpawnPoint.position+_bulletSpawnPoint.up*laserLength);


    points.Add(transform.InverseTransformPoint(_bulletSpawnPoint.position));
    points.Add(transform.InverseTransformPoint(_bulletSpawnPoint.position+_bulletSpawnPoint.up*laserLength));
    edgeCollider2D.SetPoints(points);
    DOTween.To(() => _linerenderer.startWidth, x => _linerenderer.startWidth = x, finalWidth, duration);
    DOTween.To(() => _linerenderer.endWidth, x => _linerenderer.endWidth = x, finalWidth, duration)
    .OnComplete(() =>OnDisable()); // 动画完成后调用销毁激光的方法
    }
  }
  void ElecLaser()
  {
    if(IsPressShooty)
    {

      _linerenderer.enabled = true;
        edgeCollider2D.enabled = true;
        _linerenderer.startWidth = initialWidth;
        _linerenderer.endWidth = initialWidth;
        ScreenShake.Instance.CameraShake(.1f, new Vector2(.1f, .1f), 10);
        _playerData.MinusBullet(MinusBulletCount);

        numSegments = UnityEngine.Random.Range(10, 15);

        // 初始化 points 列表
        points = new List<Vector2>();

        // 设置 LineRenderer 的顶点数
        _linerenderer.positionCount = numSegments;

        for (int j = 0; j < numSegments; j++)
        {
            Vector3 endPoint = _bulletSpawnPoint.position + _bulletSpawnPoint.up * laserLength;
            // 在连接两个点的路径中生成两三个中间点，添加随机偏移
            DrawLightning(_bulletSpawnPoint.position, endPoint, j);
        }



         DOTween.To(() => _linerenderer.startWidth, x => _linerenderer.startWidth = x, finalWidth, duration);
        DOTween.To(() => _linerenderer.endWidth, x => _linerenderer.endWidth = x, finalWidth, duration)
      .OnComplete(() =>DestroyLaser());
    }
  }


    private void DestroyLaser()
    {
         _linerenderer.enabled = false;
        edgeCollider2D.enabled = false;


    }
       public void DrawLightning(Vector3 startPoint, Vector3 endPoint, int index)
    {

/// 计算每个点的位置
    float t = index / (float)(numSegments - 1);
    Vector2 lerpedPoint = Vector3.Lerp(startPoint, endPoint, t);
    Vector2 offset = UnityEngine.Random.onUnitSphere*0.5f; // 随机偏移

    // 根据方向和距离设置每个点的位置
    _linerenderer.SetPosition(index, lerpedPoint + offset);

    // 将点添加到EdgeCollider2D
    Vector2 point2D = new(lerpedPoint.x+offset.x, lerpedPoint.y+offset.y); // 只考虑 x 和 y 坐标
    points.Add(transform.InverseTransformPoint(point2D));
    edgeCollider2D.points = points.ToArray();

    }
    protected  void OnDisable() {

        DestroyLaser();
    }
    private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Enemy"))
    {
         IDamage idamage=other.gameObject.GetComponentInParent<IDamage>();
            idamage.TakeDamage(_bulletSO.Damage);
    }
  }
}
