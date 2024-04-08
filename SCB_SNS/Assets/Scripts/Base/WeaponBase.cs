using System.Collections;
using System.Collections.Generic;
using UnityEngine;












public class WeaponBase : MonoBehaviour
{
    public WeaponSO _weaponSO;
    public GameObject _bulletPrefab;
    protected  GameObject _FlashVFX;
    public int BulletCount;
    protected int MinusBulletCount;
    protected  bool IsPressShooty;
    public Transform _bulletSpawnPoint;
    protected  PlayerData _playerData;
    public float _Distime=0.5f;
    // Start is called before the first frame update
    protected virtual void Awake() {
        _playerData=GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerData>();
    }



    protected virtual void OnEnable() {
        BulletCount = _weaponSO.BulletCount;
        MinusBulletCount = _weaponSO.MinusBulletCount;
        _FlashVFX=_weaponSO.FlashEffect;
        // _playerData.AddBullet(BulletCount);
    }

    protected virtual void Update()
    {
        ShootyControl();
        Shooty();
    }
    protected virtual void ShootyControl()
    {
        IsPressShooty=UserInput.Instance.shootyAndMove;
    }
    protected virtual void Shooty()
    {
        if(IsPressShooty&&_bulletPrefab.activeSelf&&_FlashVFX.activeSelf)
        {
            ShakeCamera();
            GameObject _Flash=PoolManager.Release(_FlashVFX, _bulletSpawnPoint.position,Quaternion.identity);
            _Flash.GetComponent<VfxDis>().DisVFX(_Distime);
            PoolManager.Release(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            _playerData.MinusBullet(MinusBulletCount);

        }
    }
    void ShakeCamera()
    {
        if(_playerData.isHit==false)
        {
            ScreenShake.Instance.CameraShake(.1f,new Vector2(.1f,.1f),10);
        }
    }




}
