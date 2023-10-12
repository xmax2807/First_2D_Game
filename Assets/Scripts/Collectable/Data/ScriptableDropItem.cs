using UnityEngine;

[CreateAssetMenu(fileName = "DropItemData", menuName = "Data/Item/DropItemData")]
public class ScriptableDropItem : ScriptableObject{
    public int NumberOfItem;
    public GameObject Item;
    public GameObject SpawnEffect;
}