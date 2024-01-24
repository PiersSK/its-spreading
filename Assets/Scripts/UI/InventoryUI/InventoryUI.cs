using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    void Start()
    {
        Player.Instance.GetComponent<InventorySystem>().onInventoryChangeEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach(InventoryItemData item in Player.Instance.inventory.itemDict.Values)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItemData item)
    {
        GameObject obj = Instantiate(itemSlotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlotUI slot = obj.GetComponent<ItemSlotUI>();
        slot.Set(item);
    }
}
