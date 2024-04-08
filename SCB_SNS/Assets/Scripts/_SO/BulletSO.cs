using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;








[CreateAssetMenu(fileName ="BulletSo",menuName ="BulletSO")]
public class BulletSO : ScriptableObject
{
    public float Movespeed;
    public float Damage;
    public float BulletDisTime;
    public float HitForce;
    public GameObject DisVFX;
}
