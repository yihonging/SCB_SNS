using System.Collections;
using System.Collections.Generic;
using UnityEngine;












public class PillGenerator : Singleton<PillGenerator>
{
    public GameObject pillPrefab; // Pill预制体
    [SerializeField]
    private float OffsetX;


    
    void Update()
    {
        // 检查是否可以生成Pill，并且当前没有Pill存在
        if ( !PillExists())
        {
            GeneratePill();
        }
    }

    bool PillExists()
    {
        // 检查当前场景中是否有Pill存在
        return GameObject.FindGameObjectWithTag("Item") != null;
    }

    void GeneratePill()
    {
        float x= Random.Range(-OffsetX,OffsetX);
        Vector2 GenPoint= new Vector2(transform.position.x+x,transform.position.y);
        // 生成一个新的Pill
        PoolManager.Release(pillPrefab, GenPoint, Quaternion.identity);
    }
}
