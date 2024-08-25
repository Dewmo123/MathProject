using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeUI : MoveUI
{
    public NotifyValue<int> min;
    public NotifyValue<int> hour;
    [field: SerializeField] public float changeMinVal { get; private set; } = 0;
    public float curTime { get; private set; } = 0;
    private TextMeshProUGUI _timeTxt;

    protected override void Awake()
    {
        base.Awake();
        _timeTxt = GetComponent<TextMeshProUGUI>();
        min.OnvalueChanged += HandleMinChange;
        hour.OnvalueChanged += HandleHourChange;
        hour.Value = 8;
    }
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += (InputAction.CallbackContext context) => { moveCnt.Value++; };
    }
    private void HandleHourChange(int prev, int next)
    {
        if (next == 9)
        {
            GameManager.instance.DayCnt.Value++;
            hour.Value = 8;
        }
        _timeTxt.text = (hour.Value < 10 ? "0" : "") + $"{next}:{min.Value}" + (min.Value == 0 ? "0" : "");
    }

    private void HandleMinChange(int prev, int next)
    {
        if (next == 60)
        {
            min.Value = 0;
            hour.Value++;
            next = 0;
        }
        _timeTxt.text = (hour.Value < 10 ? "0" : "") + $"{hour.Value}:{next}" + (next == 0 ? "0" : "");
    }
    private void Update()
    {
        if (GameManager.instance.isTimeStop)
        {
            curTime += Time.deltaTime;
            if (curTime >= changeMinVal)
            {
                min.Value += 10;
                curTime = 0;
            }
        }
    }

    public override void Move(int pos)
    {
        rTransform.DOMoveX(pos, time);
    }
    private void OnDestroy()
    {
        min.OnvalueChanged -= HandleMinChange;
        hour.OnvalueChanged -= HandleHourChange;
    }
}
