using System.Collections;
using System.Collections.Generic;
using UnityEngine;












public class WeaponManager : MonoBehaviour
{
    public List<GameObject> AllWeapons = new List<GameObject>();
    [SerializeField]
    List<GameObject> ActiveWeapons = new List<GameObject>();

    private void Start()
    {
        // 将所有武器作为当前对象的子对象生成
        GenerateWeaponsAsChildren();
        // 随机切换武器
        RandomSwitchWeapon();
    }

    private void GenerateWeaponsAsChildren()
    {
        // 循环遍历所有武器，并将它们作为子对象生成
        foreach (GameObject weapon in AllWeapons)
        {
          GameObject weaponOnWorld=  Instantiate(weapon, transform.position, Quaternion.identity, transform);
          ActiveWeapons.Add(weaponOnWorld);
        }
    }

    public void RandomSwitchWeapon()
    {
        // 随机选择一个武器索引
        int randomIndex = Random.Range(0, ActiveWeapons.Count);

        // 显示随机选择的武器，并隐藏其他武器
        for (int i = 0; i < ActiveWeapons.Count; i++)
        {
            ActiveWeapons[i].SetActive(i == randomIndex);
        }
    }
}
