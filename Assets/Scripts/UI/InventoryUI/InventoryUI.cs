using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public static InventoryUI Instance { get; private set; }
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform inventoryBar;
    [SerializeField] public GameObject newIcon;
    
    private static Animator _animator;
    private static bool inventoryIsOpen = false;

    void Awake()
    {
        Instance = this;
        Player.Instance.newInventory.OnInventoryChanged += OnUpdateInventory;
        _animator = GetComponent<Animator>();
    }

    public void ToggleInventoryUI()
    {
        inventoryIsOpen = !inventoryIsOpen;
        string animTrigger = inventoryIsOpen ? "openInventory" : "closeInventory";
        _animator.SetTrigger(animTrigger);
        newIcon.SetActive(false);
    }

    private void OnUpdateInventory(object sender, InventorySystem.OnInventoryChangedEventArgs e)
    {
        foreach(Transform t in inventoryBar)
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
        obj.transform.SetParent(inventoryBar, false);

        ItemSlotUI slot = obj.GetComponent<ItemSlotUI>();
        slot.Set(item);
    }
}
