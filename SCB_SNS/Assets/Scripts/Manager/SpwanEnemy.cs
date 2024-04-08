// using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;










public class SpwanEnemy : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject SuperEnemy;
    public float MinSpawnTime = 1f;
    public float MaxSpawnTime = 2f;
    [SerializeField]
    private float OffsetX;
    // Start is called before the first frame update
    void Start()
    {
       UpdateSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {

    }
     void UpdateSpawnTime()
    {
        float spawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
        Invoke(nameof(SpawnEnemy), spawnTime);
    }
    void SpawnEnemy()
    {
        SpwanNormalEnemy();
        UpdateSpawnTime();
    }
    public void SpwanNormalEnemy()
    {
        float x= Random.Range(-OffsetX,OffsetX);
        if(Enemy.activeSelf)
        {
           Vector2 GenPoint= new Vector2(transform.position.x+x,transform.position.y);
           PoolManager.Release(Enemy,GenPoint,Quaternion.identity);
        }

    }
    public Vector2 GenPoint()
    {
         float x= Random.Range(-OffsetX,OffsetX);
         Vector2 genPoint= new(transform.position.x+x,transform.position.y);
         return genPoint;
    }
   public void SpwanSuperEnemy()
    {

        if(SuperEnemy.activeSelf)
        {
           PoolManager.Release(SuperEnemy,GenPoint(),Quaternion.identity);
        }
    }
    
}
