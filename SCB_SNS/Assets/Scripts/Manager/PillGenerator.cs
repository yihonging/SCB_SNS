using System.Collections;
using System.Collections.Generic;
using UnityEngine;












public class PillGenerator : Singleton<PillGenerator>
{
    public GameObject pillPrefab; // PillԤ����
    [SerializeField]
    private float OffsetX;


    
    void Update()
    {
        // ����Ƿ��������Pill�����ҵ�ǰû��Pill����
        if ( !PillExists())
        {
            GeneratePill();
        }
    }

    bool PillExists()
    {
        // ��鵱ǰ�������Ƿ���Pill����
        return GameObject.FindGameObjectWithTag("Item") != null;
    }

    void GeneratePill()
    {
        float x= Random.Range(-OffsetX,OffsetX);
        Vector2 GenPoint= new Vector2(transform.position.x+x,transform.position.y);
        // ����һ���µ�Pill
        PoolManager.Release(pillPrefab, GenPoint, Quaternion.identity);
    }
}
