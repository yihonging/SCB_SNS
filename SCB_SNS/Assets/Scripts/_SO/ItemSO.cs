using System.Collections;
using System.Collections.Generic;
using UnityEngine;









[CreateAssetMenu(fileName = " CreatItem", menuName = "ItemList")]
public class ItemSO : ScriptableObject
{
    public ItemWeight[] itemWeights ;
    public ItemWeight[] eventItems ;

    // Start is called before the first frame update
}

[System.Serializable]
public class ItemWeight
{
     public GameObject itemPrefab;
    public int weight;
}
