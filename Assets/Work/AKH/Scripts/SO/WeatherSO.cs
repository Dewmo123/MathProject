using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Weather")]
public class WeatherSO : ScriptableObject
{
    public string weatherName;
    public string sprite;
    public float hungryPerSec;
    public float waterPerSec;
    public float degreePerSec;
    
}
