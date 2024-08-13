using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : PlayerConnectUI
{
    public NotifyValue<ItemSO> item;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image; 

    private ItemSO _item;

    private Health _playerHealth;
    private Hungry _playerHungry;
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
            _text.text = "0";
            gameObject.SetActive(false);
            _image.sprite = null;
        }
    }
    public void UseItem()
    {
        if (_item != null&&_item.canUse&&_item.cnt.Value>0)
        {
            _playerHealth.ChangeHp(_item.restoreHp);
            _playerHungry.ChangeHungry(_item.restoreHungry);
            _item.cnt.Value--;
        }
    }

    public override void AfterFindPlayer()
    {
        _playerHealth = _player.healthCompo;
        _playerHungry = _player.hungryCompo;
    }
}
