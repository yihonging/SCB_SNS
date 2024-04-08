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
        // 更新 ComboScore 的显示文本
        comboTextPrefab.text = ComboScore.ToString();

        // 根据 ComboScore 的值决定是否激活文本
        comboTextPrefab.gameObject.SetActive(ComboScore >= 5);
    }


}
