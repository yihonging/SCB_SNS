using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponBase
{
    public int NumberOfBullets;
    public float spreadAngle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   protected override  void Update()
    {
        ShootyControl();
        FireShotGun();
    }

    void FireShotGun()
    {
        if(IsPressShooty&&_bulletPrefab.activeSelf&&_FlashVFX.activeSelf)
        {
            ScreenShake.Instance.CameraShake(.1f,new Vector2(.1f,.1f),10);
         _playerData.MinusBullet(MinusBulletCount);
        float angleIncrement = spreadAngle / (NumberOfBullets - 1);
        GameObject _Flash=PoolManager.Release(_FlashVFX, _bulletSpawnPoint.position,Quaternion.identity);
         _Flash.GetComponent<VfxDis>().DisVFX(.5f);

        for (int i = 0; i < NumberOfBullets; i++)
        {
            float currentAngle = -spreadAngle / 2f + i * angleIncrement;
            Quaternion bulletRotation = _bulletSpawnPoint.rotation * Quaternion.Euler(0, 0, currentAngle);
            PoolManager.Release(_bulletPrefab, _bulletSpawnPoint.position, bulletRotation);
        }
        }
     }

}
