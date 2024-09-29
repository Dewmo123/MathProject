using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigUI : InteractionUI
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Slider _BGMSlider;
    [SerializeField] private Slider _MAINSlider;
    private WaitForSeconds _wait;
    private bool _isMove=false;
    private void Start()
    {
        _wait = new WaitForSeconds(time);
        GameManager.instance.Player.playerInput.Input.Config.performed += (context) => IncreaseCnt();
    }
    protected override void Open()
    {
        _isMove = true;
        base.Open();
        SetSlider(true);
        StartCoroutine(WaitMove(0));
    }

    private void Update()
    {
        Debug.Log(_isMove);

    }
    protected override void Close()
    {
        base.Close();
        SetSlider(false);
        Time.timeScale = 1;
    }
    private void SetSlider(bool v)
    {
        _SFXSlider.interactable = v;
        _SFXSlider.interactable = v;
        _SFXSlider.interactable = v;
    }
    private IEnumerator WaitMove(int num)
    {
        yield return _wait;
        _isMove = false;
        Time.timeScale = num;
    }
    #region Volume
    public void ChangeMAINVolume()
    {
        _mixer.SetFloat("MVol", _MAINSlider.value);
    }
    public void ChangeBGMVolume()
    {
        _mixer.SetFloat("BVol", _BGMSlider.value);
    }
    public void ChangeSFXVolume()
    {
        _mixer.SetFloat("SVol", _SFXSlider.value);
    }
    #endregion
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public override void Move(int pos)
    {
        transform.DOMoveY(pos, time);
    }
    public override void IncreaseCnt()
    {
        if (_isMove) return;
        base.IncreaseCnt();
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}
