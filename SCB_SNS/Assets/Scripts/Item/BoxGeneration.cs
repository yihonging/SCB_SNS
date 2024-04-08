using System.Collections;
using System.Collections.Generic;
using UnityEngine;









public class BoxGeneration : MonoBehaviour
{
    public ItemSO _itemso;
    public int MaxRepeatTimes = 4;
    public bool playerHasGoldKey = false; // 玩家是否拥有金钥匙道具

    void Start()
    {
        GenerateBoxItems();
    }

  public  void GenerateBoxItems()
    {
        List<GameObject> items = new();

        // 计算事件的相对权重总和
        int eventWeight = 0;
        int randomWeight = Random.Range(1, 101);
        foreach (var itemWeight in _itemso.itemWeights)
        {
            eventWeight += itemWeight.weight;
        }

        // 随机生成一个权重值，用于确定生成的物品

        // 根据相对权重生成物品
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

        // 判断是否执行事件
        if (randomWeight >= eventWeight && randomWeight <= 100)
        {
            int repeat = Random.Range(0, MaxRepeatTimes);
            if (playerHasGoldKey)
            {
                repeat *= 2; // 如果玩家拥有金钥匙，将事件重复次数乘以2
            }
            if (repeat < 2)
            {
                repeat = 2;
            }

            for (int i = 0; i < repeat; i++)
            {
                // 根据相对权重生成事件物品
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

        // 打乱物品列表顺序
        Shuffle(items);

        // 生成物品
        foreach (var item in items)
        {
            if(item.activeSelf)
            {
            Vector3 position = new(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z);
            PoolManager.Release(item, position, Quaternion.identity);
            }
        }
    }

    // Fisher-Yates 洗牌算法
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