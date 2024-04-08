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

    public List<ImageTextPair> imageTextPairs; // �洢Image��Text֮��Ĺ�����ϵ

    // ����UI��ʾ
    public void UpdateUI(string key, float value)
    {
        foreach (var pair in imageTextPairs)
        {
            if (pair.text.name == key) // ���Text�������Ƿ���keyƥ��
            {
                pair.text.text = value.ToString(); // ����Text��ʾ
                break;
            }
        }
    }
   public  void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthBlocks.Length; i++)
        {
            // ���ݵ�ǰѪ��������ʾ������Ѫ����Image
            healthBlocks[i].enabled = i < currentHealth;
        }
    }
}
