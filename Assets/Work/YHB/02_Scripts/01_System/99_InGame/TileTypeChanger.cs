using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class TileTypeChanger : MonoBehaviour
{
    [SerializeField] private TileType _myTileType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerSoundManager.instance.TileTypeChage(_myTileType);
    }
}
