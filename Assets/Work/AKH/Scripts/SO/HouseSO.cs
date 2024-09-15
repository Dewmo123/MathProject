using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/HouseSO")]
public class HouseSO : ScriptableObject
{
    public string houseName;
    public Sprite houseImage;
    public int decDayHealth;
    public int woodCount;
    public int rockCount;
    public string info;
}
