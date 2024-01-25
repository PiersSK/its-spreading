using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    void Start()
    {
        Player.Instance.newInventory.OnInventoryChanged += OnUpdateInventory;
    }

    private void OnUpdateInventory(object sender, InventorySystem.OnInventoryChangedEventArgs e)
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach(InventoryItemData item in Player.Instance.newInventory.inventory.Values)
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
