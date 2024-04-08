using System.Collections;
using System.Collections.Generic;
using UnityEngine;







[CreateAssetMenu(fileName ="EnemySO",menuName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    public float Health;
    public float MoveSpeed;
    public float Scale;
    public bool CanShoot;

}
