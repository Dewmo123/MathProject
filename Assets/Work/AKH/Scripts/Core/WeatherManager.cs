using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GameManager.instance.Player;
    }

}
