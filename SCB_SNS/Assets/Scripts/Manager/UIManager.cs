using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;








public class UIManager : Singleton<UIManager>
{

     [System.Serializable]
    public struct ImageTextPair
    {
        public Image image;
        public Text text;
    }
    public Image[] healthBlocks;

    public List<ImageTextPair> imageTextPairs; // 存储Image和Text之间的关联关系

    // 更新UI显示
    public void UpdateUI(string key, float value)
    {
        foreach (var pair in imageTextPairs)
        {
            if (pair.text.name == key) // 检查Text的名字是否与key匹配
            {
                pair.text.text = value.ToString(); // 更新Text显示
                break;
            }
        }
    }
   public  void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthBlocks.Length; i++)
        {
            // 根据当前血量决定显示或隐藏血量块Image
            healthBlocks[i].enabled = i < currentHealth;
        }
    }
}
