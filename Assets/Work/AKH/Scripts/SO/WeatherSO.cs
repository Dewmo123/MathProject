using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherEnum
{
    Sunny,
    Rainy,
    Thunderstorm,
    Storm,
    Snow
}
[CreateAssetMenu(menuName ="SO/Weather")]
public class WeatherSO : ScriptableObject
{
    public WeatherEnum weatherEnum;
    public string weatherName;
    public Sprite sprite;
    public float healthPerSec;
    public float hungryPerSec;
    public float waterPerSec;
    public SoundSO sound;
    
}
