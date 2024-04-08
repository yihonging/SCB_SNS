using System.Collections;
using System.Collections.Generic;
using UnityEngine;









public class BoxGeneration : MonoBehaviour
{
    public ItemSO _itemso;
    public int MaxRepeatTimes = 4;
    public bool playerHasGoldKey = false; // ����Ƿ�ӵ�н�Կ�׵���

    void Start()
    {
        GenerateBoxItems();
    }

  public  void GenerateBoxItems()
    {
        List<GameObject> items = new();

        // �����¼������Ȩ���ܺ�
        int eventWeight = 0;
        int randomWeight = Random.Range(1, 101);
        foreach (var itemWeight in _itemso.itemWeights)
        {
            eventWeight += itemWeight.weight;
        }

        // �������һ��Ȩ��ֵ������ȷ�����ɵ���Ʒ

        // �������Ȩ��������Ʒ
        for (int i = 0; i < _itemso.itemWeights.Length; i++)
        {
            int minWeight = i == 0 ? 1 :_itemso.itemWeights[i - 1].weight + 1;
            int maxWeight = _itemso.itemWeights[i].weight+minWeight;
            if (randomWeight >= minWeight && randomWeight <= maxWeight)
            {
                items.Add(_itemso.itemWeights[i].itemPrefab);
                break;
            }
        }

        // �ж��Ƿ�ִ���¼�
        if (randomWeight >= eventWeight && randomWeight <= 100)
        {
            int repeat = Random.Range(0, MaxRepeatTimes);
            if (playerHasGoldKey)
            {
                repeat *= 2; // ������ӵ�н�Կ�ף����¼��ظ���������2
            }
            if (repeat < 2)
            {
                repeat = 2;
            }

            for (int i = 0; i < repeat; i++)
            {
                // �������Ȩ�������¼���Ʒ
                int secondRandomWeight = Random.Range(1, 101);
                for (int j = 0; j < _itemso.eventItems.Length; j++)
                {
                    int minWeight = j == 0 ? 1 : _itemso.eventItems[j - 1].weight + 1;
                    int maxWeight = _itemso.eventItems[j].weight;

                    if (secondRandomWeight >= minWeight && secondRandomWeight <= maxWeight)
                    {
                        items.Add(_itemso.eventItems[j].itemPrefab);
                        break;
                    }
                }
            }
        }

        // ������Ʒ�б�˳��
        Shuffle(items);

        // ������Ʒ
        foreach (var item in items)
        {
            if(item.activeSelf)
            {
            Vector3 position = new(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z);
            PoolManager.Release(item, position, Quaternion.identity);
            }
        }
    }

    // Fisher-Yates ϴ���㷨
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}