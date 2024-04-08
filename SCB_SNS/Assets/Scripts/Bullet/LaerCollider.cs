using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaerCollider : MonoBehaviour
{
    private EdgeCollider2D edgeCollider2D;
    public BulletSO _bulletSO; // ×Óµ¯Êý¾Ý

    private void Awake() {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }
  private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Enemy"))
    {
         IDamage idamage=other.gameObject.GetComponentInParent<IDamage>();
            idamage.TakeDamage(_bulletSO.Damage);
    }
  }
}
