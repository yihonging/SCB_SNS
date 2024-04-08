using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wiggle : MonoBehaviour
{
    public float wiggleSpeed;
    public float wiggleMagnitude;
    void Update()
    {
        WiggleAnim(wiggleSpeed,wiggleMagnitude);
    }
     void WiggleAnim(float wiggleSpeed,float wiggleMagnitude)
    {
       this.wiggleSpeed=wiggleSpeed;
       this.wiggleMagnitude=wiggleMagnitude;
       transform.localRotation=Quaternion.Euler(0,0,Mathf.Sin(Time.time*wiggleSpeed)*wiggleMagnitude);
    }
}
