using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nature : Agent
{
    private NatureStateMachine _stateMachine;
    [SerializeField] private int _minCnt;
    [SerializeField] private int _maxCnt;
    [SerializeField] private SoundSO _hitSound;
    [SerializeField] private List<ItemSO> items;
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new NatureStateMachine();
        _stateMachine.Add(NatureType.Idle, new NatureIdleState(_stateMachine, this, "Idle"));
        _stateMachine.Add(NatureType.Hit, new NatureHitState(_stateMachine, this, "Hit"));
        _stateMachine.Add(NatureType.Dead, new NatureDeadState(_stateMachine, this, "Dead"));
        _stateMachine.Init(NatureType.Idle);

        healthCompo.OnDecEvent.AddListener(PlayHitSound);
    }

    private void PlayHitSound()
    {
        var soundPlayer = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        soundPlayer.PlaySound(_hitSound);
    }

    private void Start()
    {
        TimeManager.instance.DayCnt.OnvalueChanged += Revive;
    }

    private void Revive(int prev, int next)
    {
        healthCompo.ResetValue();
        SetDead(false);
        _stateMachine.ChangeState(NatureType.Idle);
    }

    private void Update()
    {
        _stateMachine.currentState.UpdateState();
    }
    public void ChangeHitState()
    {
        if (isDead) return;
        _stateMachine.ChangeState(NatureType.Hit);
    }
    public void ChangeDeadState()
    {
        _stateMachine.ChangeState(NatureType.Dead);
        SetDead(true);
    }
    public override void EndTriggerCall()
    {
        _stateMachine.currentState.EndTriggerCalled();
    }
    public void AddItem()
    {
        string message = "";
        var text = PoolManager.instance.Pop("SystemText") as SystemTxtUI;
        foreach (ItemSO item in items)
        {
            int num = UnityEngine.Random.Range(_minCnt, _maxCnt);
            item.cnt.Value += num;
            message += $"{item.itemName} + {num}, ";
        }
        text.GetComponent<TextMeshProUGUI>().text = message;
        text.gameObject.SetActive(true);

    }
}
