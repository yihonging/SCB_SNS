using UnityEngine;
using TMPro;








public class ComboSystem : MonoBehaviour
{
    public int ComboScore = 0;
    public TMP_Text comboTextPrefab;

    private void OnEnable()
    {
        GameEventHandler.OnCombo.AddListener(ComboManager);
        UpdateComboText();
    }

    private void ComboManager()
    {
        ComboScore++;
        UpdateComboText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Item"))
        {
            ComboScore = 0;
            UpdateComboText();
        }
    }

    private void OnDisable()
    {
        GameEventHandler.OnCombo.RemoveListener(ComboManager);
    }

    private void UpdateComboText()
    {
        // ���� ComboScore ����ʾ�ı�
        comboTextPrefab.text = ComboScore.ToString();

        // ���� ComboScore ��ֵ�����Ƿ񼤻��ı�
        comboTextPrefab.gameObject.SetActive(ComboScore >= 5);
    }


}
