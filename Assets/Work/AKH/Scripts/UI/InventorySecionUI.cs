using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySecionUI : MonoBehaviour
{
    private void Awake()
    {
        foreach (var item in GameManager.instance.items)
        {
            item.cnt.OnvalueChanged += (int prev, int next) =>
            {
                HandleItemChanged(prev, next, item);
            };

        }
        foreach (RectTransform rTrm in transform)
        {
            rTrm.gameObject.SetActive(false);
        }
    }

    private void HandleItemChanged(int prev, int next, ItemSO item)
    {
        if (prev == 0)
        {
            foreach (RectTransform rTrm in transform)
            {
                Image image = rTrm.GetComponent<Image>();
                if (image.sprite == null)
                {
                    image.sprite = item.sprite;
                    rTrm.gameObject.SetActive(true);
                    return;
                }
            }
        }
        if (next == 0)
        {
            foreach (RectTransform rTrm in transform)
            {
                Image image = rTrm.GetComponent<Image>();
                if (image.sprite == item.sprite)
                {
                    image.sprite = null;
                    rTrm.gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}
