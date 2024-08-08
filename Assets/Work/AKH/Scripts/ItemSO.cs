using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="SO/Material")]
public class ItemSO : ScriptableObject
{
    public NotifyValue<int> cnt;
    public string matName;
    public Sprite sprite;
}
