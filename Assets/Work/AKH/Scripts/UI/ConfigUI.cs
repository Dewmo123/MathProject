using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigUI : MoveUI
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Slider _BGMSlider;
    [SerializeField] private Slider _MAINSlider;
    private WaitForSeconds _wait;
    private void Start()
    {
        _wait = new WaitForSeconds(time);
        GameManager.instance.Player.playerInput.Input.Config.performed += (context) => IncreaseCnt();
    }
    protected override void HandleCnt(int prev, int next)
    {
        if (next == 1)
        {
            Move(_firstPos);
            StartCoroutine(WaitMove(0));
        }
        else if (next == 2)
        {
            Time.timeScale = 1;
            Move(_secondPos);
            moveCnt.Value = 0;
        }
    }

    private IEnumerator WaitMove(int num)
    {
        yield return _wait;
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
}
