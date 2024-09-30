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
    private bool _isMove = false;
    protected override void Awake()
    {
        base.Awake();
        SetSlider(false);
        float val;
        _mixer.GetFloat("SVol", out val);
        _SFXSlider.value = val;
        _mixer.GetFloat("BVol", out val);
        _BGMSlider.value = val;
        _mixer.GetFloat("MVol", out val);
        _MAINSlider.value = val;
    }
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
        StartCoroutine(WaitMove(time));
    }

    protected override void Close()
    {
        base.Close();
        SetSlider(false);
        Time.timeScale = 1;
    }
    private void SetSlider(bool v)
    {
        _MAINSlider.interactable = v;
        _SFXSlider.interactable = v;
        _BGMSlider.interactable = v;
    }
    private IEnumerator WaitMove(float num)
    {
        yield return _wait;
        _isMove = false;
        Time.timeScale = num;
    }
    #region Volume
    public void ChangeMAINVolume()
    {
        _mixer.SetFloat("MVol", _MAINSlider.value);
        if (_MAINSlider.value == -40)
            _mixer.SetFloat("MVol", -80);
    }
    public void ChangeBGMVolume()
    {
        _mixer.SetFloat("BVol", _BGMSlider.value);
        if (_BGMSlider.value == -40)
            _mixer.SetFloat("BVol", -80);
    }
    public void ChangeSFXVolume()
    {
        _mixer.SetFloat("SVol", _SFXSlider.value);
        if (_SFXSlider.value == -40)
            _mixer.SetFloat("SVol", -80);
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
        int cHei = Screen.height;
        rTransform.DOMoveY(pos * ((float)cHei / 1080), time);
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
