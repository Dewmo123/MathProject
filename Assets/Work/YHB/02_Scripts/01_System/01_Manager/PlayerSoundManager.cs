using System;
using UnityEngine;

public enum TileType
{
    sea,
    sand,
    grass,
    stone
}

public class PlayerSoundManager : MonoBehaviour
{
    static public PlayerSoundManager instance;

    [SerializeField] private SoundSO _seaSoundSO, _sandSoundSO, _grassSoundSO, _stoneSoundSO;

    private TileType _presentType;

    private void Awake()
    {
        if (instance is null) instance = this;
    }

    public void TileTypeChange(TileType tileType)
    {
        _presentType = tileType;
    }

    public SoundSO GetPlayerMoveSound() => _presentType switch
    {
        TileType.sea => _seaSoundSO,
        TileType.sand => _sandSoundSO,
        TileType.grass => _grassSoundSO,
        TileType.stone => _stoneSoundSO,
        _ => throw new Exception("SoundSO not difine")
    };
}
