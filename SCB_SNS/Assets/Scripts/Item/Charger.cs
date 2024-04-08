using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : PillMove
{
    public int Count;


   protected override void OnTriggerEnter2D(Collider2D other)
   {
    base.OnTriggerEnter2D(other);
    if(other.tag == "Player")
    {
        other.GetComponentInParent<PlayerData>().AddBullet(Count);
    }
   }

}
