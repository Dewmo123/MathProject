using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySessionUI : MonoBehaviour
{
    private void Awake()
    {
        foreach (var item in GameManager.instance.items)
        {
            item.cnt.OnvalueChanged += (int prev, int next) =>
            {
                Debug.Log("asd");
                foreach (RectTransform rTrm in transform)
                {
                    if (rTrm.GetComponent<Image>().sprite == item.sprite) return;
                }
                foreach (RectTransform rTrm in transform)
                {
                    Image image = rTrm.GetComponent<Image>();
                    if (image.sprite == null)
                    {
                        image.sprite = item.sprite;
                        return;
                    }
                }
            };
        }
    }
}
