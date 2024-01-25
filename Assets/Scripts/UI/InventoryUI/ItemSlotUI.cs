using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    [SerializeField] private TextMeshProUGUI label;

    public void Set(InventoryItemData item)
    {  
        icon.sprite = item.icon;
        label.text = item.displayName;
    }

}
