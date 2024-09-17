using SerializableDictionary.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<ProblemSO> problems;
    public SerializableDictionary<DiffucultEnum, List<ProblemSO>> problemDic;
    public bool isUI { get; private set; } = false;
    public bool isInteractionUI { get; private set; } = false;
    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<Player>();
            if (_player == null)
                Debug.LogWarning("There is no player in scene, but still try access it");
            return _player;
        }
    }

    [field: SerializeField] public List<ItemSO> items { get; private set; }

    [SerializeField] private float _hitTime;
    [SerializeField] private WeatherUI _coreWeatherCompo;

    private WeatherSO _curWeather;

    [field: SerializeField] public List<WeatherSO> weathers { get; private set; }
    public List<WeatherSO> curWeathers;

    public NotifyValue<ClothSO> CurCloth = new NotifyValue<ClothSO>();
    public NotifyValue<HouseSO> CurHouse = new NotifyValue<HouseSO>();

    private float _currentTime;
    private int cnt = -1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        SetWeathers();
        foreach (var item in problems)
        {
            if (!problemDic.Dictionary[item.diffucult].Contains(item))
                problemDic.Dictionary[item.diffucult].Add(item);
        }
    }
    private void Start()
    {
        TimeManager.instance.DayCnt.OnvalueChanged += HandleDayChange;
        _coreWeatherCompo.curWeather.Value = curWeathers[TimeManager.instance.DayCnt.Value - 1];
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TimeManager.instance.DayCnt.Value++;
        if (!TimeManager.instance.isTimeStop)
        {
            if (_currentTime >= _hitTime && _player != null)
            {
                _curWeather = _coreWeatherCompo.curWeather.Value;
               
                _currentTime = 0;
                _player.healthCompo.ChangeValue(_curWeather.healthPerSec*CurCloth.Value.decHealthPerSec);
                _player.hungryCompo.ChangeValue(_curWeather.hungryPerSec);
                _player.waterCompo.ChangeValue(_curWeather.waterPerSec * CurCloth.Value.decWaterPerSec);
            }
            _currentTime += Time.deltaTime;
        }
    }
    private void SetWeathers()
    {
        curWeathers = new List<WeatherSO>();
        for (int i = 1; i <= 57; i++)
        {
            WeatherSO weather = weathers[UnityEngine.Random.Range(0, weathers.Count)];
            curWeathers.Add(weather);
        }
    }
    public ProblemSO GetRandomProblem(DiffucultEnum type)
    {
        int num = UnityEngine.Random.Range(0, problemDic.Dictionary[type].Count);
        return problemDic.Dictionary[type][num];
    }
    private void HandleDayChange(int prev, int next)
    {
        cnt = next - 2;
        _coreWeatherCompo.curWeather.Value = curWeathers[TimeManager.instance.DayCnt.Value - 1 % 57];
    }
    public ItemSO GetItemSO(string name)
    {
        foreach(ItemSO item in items)
        {
            if (item.itemName == name) return item;
        }
        return default;
    }
    public void SetUI(bool value)
    {
        isUI = value;
    }
    public void SetInteractionUI(bool value)
    {
        isInteractionUI = value;
    }
    public WeatherSO GetNextWeather()
    {
        cnt++;
        return curWeathers[cnt % 57];
    }
    public void SetHouseSO(HouseSO house)
    {
        CurHouse.Value = house;
    }
}
