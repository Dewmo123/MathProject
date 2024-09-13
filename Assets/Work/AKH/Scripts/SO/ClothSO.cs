using UnityEngine;

[CreateAssetMenu(menuName ="SO/ClothSO")]
public class ClothSO : ScriptableObject
{
    public string clothName;
    public Sprite clothImage;
    public float decHealthPerSec;
    public float decWaterPerSec;
    public int leatherCount;
    public int durability;
    public string info;
}
