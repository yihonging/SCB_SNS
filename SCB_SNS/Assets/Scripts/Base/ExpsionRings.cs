using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;








public class ExpsionRings :MonoBehaviour
{
    public GameObject debrisPrefab; // ��ը��ƬԤ����
    public int initialCircleCount = 4; // ��ʼ����Ȧ��
    public int minDebrisCountPerCircle = 5; // ÿȦ��С���ɵ���Ƭ����
    public int maxDebrisCountPerCircle = 10; // ÿȦ������ɵ���Ƭ����
    public float initialCircleRadius = .5f; // ��ʼ����Ȧ�İ뾶
    public float circleRadiusIncrement = 5f; // Ȧ�뾶�����ӵ���
    public float circleInterval = 2f; // Ȧ֮��ļ��



    private void OnEnable() {
        GameEventHandler.OnexpsionRange.AddListener(ExpsionRange);
    }

    private void ExpsionRange(Vector3 PlayerPos)
    {
        GenerateExplosionCircles(10,3,PlayerPos);
    }

    public void GenerateExplosionCircles(int circleCount, float initialRadius,Vector3 Pos)
    {
        for (int i = 0; i < circleCount; i++)
        {
            // circleRadiusIncrement=Random.Range(.5f,1f);
            float radius = initialRadius + i * circleRadiusIncrement;
            GenerateExplosionCircle(radius, i*circleInterval/circleCount,Pos);
        }
    }

    void GenerateExplosionCircle(float radius, float delay,Vector3 Pos)
    {
        transform.DOScale(Vector3.one * radius, 0.01f)
            .SetEase(Ease.OutCirc)
            .SetDelay(delay)
            .OnComplete(() => GenerateDebrisCircle(radius,Pos));
    }

    void GenerateDebrisCircle(float radius,Vector3 Pos)
    {
        int debrisCount = Random.Range(minDebrisCountPerCircle, maxDebrisCountPerCircle + 1);

        for (int i = 0; i < debrisCount; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 spawnPosition = Pos+ Quaternion.Euler(0, 0, angle) * Vector3.right * radius;

           GameObject expsionPrefabs= PoolManager.Release(debrisPrefab, spawnPosition, Quaternion.identity);
           expsionPrefabs.GetComponent<VfxDis>().DisVFX(1f);

        }
    }
    private void OnDisable() {
        GameEventHandler.OnexpsionRange.RemoveListener(ExpsionRange);
    }
}
