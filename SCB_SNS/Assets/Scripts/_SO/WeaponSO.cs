using System.Collections;
using System.Collections.Generic;
using UnityEngine;









[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSO :ScriptableObject
{
    /// <summary>
    /// 获取子弹的数目
    /// </summary>
    public int BulletCount;
    /// <summary>
    /// 射击消耗子弹数
    /// </summary>
    public int MinusBulletCount;
    public GameObject FlashEffect;
    

}
