using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : MonoBehaviour
{

    [SerializeField] private Image _weatherImage;
    [SerializeField] private TextMeshProUGUI _weatherTxt;
    public NotifyValue<WeatherSO> curWeather;

    protected void Awake()
    {
        curWeather.OnvalueChanged += HandleWeatherChanged;
    }
    protected void HandleWeatherChanged(WeatherSO prev, WeatherSO next)
    {
        _weatherImage.sprite = next.sprite;
        _weatherTxt.text = next.weatherName;
    }
}
