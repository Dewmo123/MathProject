using SerializableDictionary.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int _curWeatherNum;
    [SerializeField] private List<ProblemSO> problems;
    public SerializableDictionary<DifficultEnum, List<ProblemSO>> problemDic;
    public bool isUI { get; private set; } = false;
    public bool isInteractionUI { get; private set; } = false;
    public bool isTotem { get; private set; } = false;

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
    [SerializeField] private float _decHungryPerSec;
    [SerializeField] private float _hitTime;
    [SerializeField] private float _whenDayChangedDecHealth;



    [field: SerializeField] public List<WeatherSO> weathers { get; private set; }
    public List<WeatherSO> curWeathers;

    [SerializeField] private HouseSO _noneHouse;
    public NotifyValue<WeatherSO> CurWeather;
    public NotifyValue<ClothSO> CurCloth;
    public NotifyValue<HouseSO> CurHouse;
    private float _currentTime;
    private int cnt = -1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        SetWeathers();
        CurHouse.Value = _noneHouse;

        foreach (var item in problems)
        {
            if (!problemDic.Dictionary[item.diffucult].Contains(item))
                problemDic.Dictionary[item.diffucult].Add(item);
        }
    }


    private void Start()
    {
        TimeManager.instance.DayCnt.OnvalueChanged += HandleDayChange;
        CurWeather.Value = curWeathers[TimeManager.instance.DayCnt.Value - 1];
    }

    private void Update()
    {
        ChangeStats();
    }

    private void ChangeStats()
    {
        if (!TimeManager.instance.isTimeStop)
        {
            if (_currentTime >= _hitTime && _player != null)
            {
                _currentTime = 0;
                _player.healthCompo.ChangeValue(CurWeather.Value.healthPerSec * CurCloth.Value.decHealthPerSec);
                _player.hungryCompo.ChangeValue(CurWeather.Value.hungryPerSec * (TimeManager.instance.curFireTime > 0 ? _decHungryPerSec : 1F));
                _player.waterCompo.ChangeValue(CurWeather.Value.waterPerSec * CurCloth.Value.decWaterPerSec);
            }
            _currentTime += Time.deltaTime;
        }
    }

    private void SetWeathers()
    {
        curWeathers = new List<WeatherSO>();
        for (int i = 1; i <= _curWeatherNum; i++)
        {
            int num = UnityEngine.Random.Range(0, weathers.Count + 3);
            WeatherSO weather;
            if (num >= weathers.Count)
                weather = weathers[3];
            else
                weather = weathers[num];
            curWeathers.Add(weather);
        }
    }
    public ProblemSO GetRandomProblem(DifficultEnum type)
    {
        int num = UnityEngine.Random.Range(0, problemDic.Dictionary[type].Count);
        return problemDic.Dictionary[type][num];
    }
    private void HandleDayChange(int prev, int next)
    {
        cnt = next - 2;
        isTotem = false;
        CurWeather.Value = curWeathers[TimeManager.instance.DayCnt.Value - 1 % _curWeatherNum];
        _player.healthCompo.Multiply(_whenDayChangedDecHealth * CurHouse.Value.decDayHealth);
    }
    public ItemSO GetItemSO(string name)
    {
        foreach (ItemSO item in items)
        {
            if (item.itemName == name) return item;
        }
        return default;
    }
    public ItemSO GetRandomItem()
    {
        return items[Random.Range(0, items.Count)];
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
        return curWeathers[cnt % _curWeatherNum];
    }
    public void SetHouseSO(HouseSO house)
    {
        CurHouse.Value = house;
    }
    public void UseTotem()
    {
        isTotem = true;
    }
}
