using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="SO/Material")]
public class ItemSO : ScriptableObject
{
    public NotifyValue<int> cnt;
    public string itemName;
    public Sprite sprite;

    public bool canUse;
    public int restoreHp,restoreHungry,restoreWater,restoreTemperature;
    public bool canCook;
    public ItemSO complete;
}
