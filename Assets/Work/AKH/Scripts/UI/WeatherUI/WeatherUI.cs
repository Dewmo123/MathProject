using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : MonoBehaviour
{
    public NotifyValue<WeatherSO> curWeather { get; private set; }

    [SerializeField] private Image _weatherImage;
    [SerializeField] private TextMeshProUGUI _weatherTxt;
    protected void Awake()
    {
        curWeather = new NotifyValue<WeatherSO>();
        curWeather.OnvalueChanged += HandleWeatherChanged;
    }
    protected void HandleWeatherChanged(WeatherSO prev, WeatherSO next)
    {
        _weatherImage.sprite = next.sprite;
        _weatherTxt.text = next.weatherName;
    }
}
