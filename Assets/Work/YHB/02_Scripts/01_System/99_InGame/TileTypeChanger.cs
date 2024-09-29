using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class TileTypeChanger : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private TileType _myTileType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _collisionLayer)
        {
            PlayerSoundManager.instance.TileTypeChange(_myTileType);
        }
    }
}
