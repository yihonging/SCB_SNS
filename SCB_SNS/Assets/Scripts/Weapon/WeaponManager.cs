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
        // ������������Ϊ��ǰ������Ӷ�������
        GenerateWeaponsAsChildren();
        // ����л�����
        RandomSwitchWeapon();
    }

    private void GenerateWeaponsAsChildren()
    {
        // ѭ��������������������������Ϊ�Ӷ�������
        foreach (GameObject weapon in AllWeapons)
        {
          GameObject weaponOnWorld=  Instantiate(weapon, transform.position, Quaternion.identity, transform);
          ActiveWeapons.Add(weaponOnWorld);
        }
    }

    public void RandomSwitchWeapon()
    {
        // ���ѡ��һ����������
        int randomIndex = Random.Range(0, ActiveWeapons.Count);

        // ��ʾ���ѡ�����������������������
        for (int i = 0; i < ActiveWeapons.Count; i++)
        {
            ActiveWeapons[i].SetActive(i == randomIndex);
        }
    }
}
