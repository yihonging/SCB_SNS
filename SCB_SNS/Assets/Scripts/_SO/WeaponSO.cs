using System.Collections;
using System.Collections.Generic;
using UnityEngine;









[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSO :ScriptableObject
{
    /// <summary>
    /// ��ȡ�ӵ�����Ŀ
    /// </summary>
    public int BulletCount;
    /// <summary>
    /// ��������ӵ���
    /// </summary>
    public int MinusBulletCount;
    public GameObject FlashEffect;
    

}
