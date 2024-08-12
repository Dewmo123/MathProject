using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public NotifyValue<ItemSO> item;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image; 
    private ItemSO _item;
    private void Awake()
    {
        item.OnvalueChanged += HandleItemChanged;
    }
    private void Update()
    {
        if(_item != null)
        {
            _text.text = _item.cnt.Value.ToString();
        }
    }
    private void HandleItemChanged(ItemSO prev, ItemSO next)
    {
        _item = next;
        if (next != null)
        {
            gameObject.SetActive(true);
            _text.text = next.cnt.Value.ToString();
            _image.sprite = next.sprite;
        }
        else
        {
            Debug.Log("null");
            _text.text = "0";
            gameObject.SetActive(false);
            _image.sprite = null;
        }
    }
}
