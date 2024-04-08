using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Brust : WeaponBase
{


    public float fireRate;
    public float TimeBTWshot;
    public int ShootBulletCount;


    protected override void Update() {
        ShootyControl();
        BrustBullet();
    }

    private void BrustBullet()
    {
        if(IsPressShooty&&_bulletPrefab.activeSelf)
        {
        for(int i = 0; i < ShootBulletCount; i++)
        {
            Invoke(nameof(SpwanBullet),i*TimeBTWshot);
        }
            _playerData.MinusBullet(MinusBulletCount);
        }
    }
    void SpwanBullet()
    {
        if(_FlashVFX.activeSelf)
        {
             GameObject _Flash=PoolManager.Release(_FlashVFX, _bulletSpawnPoint.position,Quaternion.identity);
            _Flash.GetComponent<VfxDis>().DisVFX(.5f);
            ScreenShake.Instance.CameraShake(.1f,new Vector2(.1f,.1f),10);
            PoolManager.Release(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        }
    }
}