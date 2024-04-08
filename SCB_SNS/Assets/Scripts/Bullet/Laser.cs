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
    public BulletSO _bulletSO; // �ӵ�����
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
    .OnComplete(() =>OnDisable()); // ������ɺ�������ټ���ķ���
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

        // ��ʼ�� points �б�
        points = new List<Vector2>();

        // ���� LineRenderer �Ķ�����
        _linerenderer.positionCount = numSegments;

        for (int j = 0; j < numSegments; j++)
        {
            Vector3 endPoint = _bulletSpawnPoint.position + _bulletSpawnPoint.up * laserLength;
            // �������������·���������������м�㣬������ƫ��
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

/// ����ÿ�����λ��
    float t = index / (float)(numSegments - 1);
    Vector2 lerpedPoint = Vector3.Lerp(startPoint, endPoint, t);
    Vector2 offset = UnityEngine.Random.onUnitSphere*0.5f; // ���ƫ��

    // ���ݷ���;�������ÿ�����λ��
    _linerenderer.SetPosition(index, lerpedPoint + offset);

    // ������ӵ�EdgeCollider2D
    Vector2 point2D = new(lerpedPoint.x+offset.x, lerpedPoint.y+offset.y); // ֻ���� x �� y ����
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
