using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookSectionUI : MonoBehaviour
{
    private ItemSO _rock;
    [SerializeField] private TextMeshProUGUI _fishRockTxt;
    [SerializeField] private TextMeshProUGUI _meatRockTxt;
    private void Start()
    {
        _rock = GameManager.instance.GetItemSO("돌");
        _fishRockTxt.text = "돌: " + GameManager.instance.GetItemSO("물고기").rockCount.ToString();
        _meatRockTxt.text = "돌: " + GameManager.instance.GetItemSO("고기").rockCount.ToString();
    }
    public void Cook(ItemSO item)
    {
        if (_rock.cnt.Value >= item.rockCount && item.cnt.Value > 0 && TimeManager.instance.isFire)
        {
            Debug.Log("Cook");
            _rock.cnt.Value -= item.rockCount;
            item.cnt.Value--;
            item.complete.cnt.Value++;
        }
    }
}
