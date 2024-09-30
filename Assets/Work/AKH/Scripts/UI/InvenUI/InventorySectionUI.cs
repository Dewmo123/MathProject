using System.Collections.Generic;
using UnityEngine;

public class InventorySectionUI : MonoBehaviour
{
    private List<ItemSO> items;
    private void Start()
    {
        items = GameManager.instance.items;
        foreach (var item in items)
        {
            item.cnt.OnvalueChanged += (int prev, int next) =>
                HandleItemChanged(prev, next, item);
        }
        int index = 0;
        foreach (RectTransform rTrm in transform)
        {
            if (index < items.Count && items[index].cnt.Value > 0)
                rTrm.GetComponent<InventorySlotUI>().item.Value = items[index];
            else
                rTrm.gameObject.SetActive(false);
            index++;
        }
    }
    private void OnDestroy()
    {
        foreach (var item in items)
        {
            item.cnt.OnvalueChanged -= (int prev, int next) =>
                HandleItemChanged(prev, next, item);
        }
    }
    private void HandleItemChanged(int prev, int next, ItemSO item)
    {
        if (prev == 0)
        {
            foreach (RectTransform rTrm in transform)
            {
                InventorySlotUI slot = rTrm.GetComponent<InventorySlotUI>();
                if (slot.item.Value == null)
                {
                    slot.item.Value = item;
                    return;
                }
            }
        }
        if (next == 0)
        {
            foreach (RectTransform rTrm in transform)
            {
                InventorySlotUI slot = rTrm.GetComponent<InventorySlotUI>();
                if (slot.item.Value == item)
                {
                    slot.item.Value = null;
                    return;
                }
            }
        }
    }
}
