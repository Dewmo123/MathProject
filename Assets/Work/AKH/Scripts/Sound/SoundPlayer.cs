using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour, IPoolable
{
    [SerializeField] private AudioMixerGroup _sfxGroup, _musicGroup;
    [SerializeField] private string _poolName;
    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    public bool isUI => false;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(SoundSO data)
    {
        if (data.audioType == SoundSO.AudioType.SFX)
        {
            _audioSource.outputAudioMixerGroup = _sfxGroup;
        }
        else if (data.audioType == SoundSO.AudioType.Music)
        {
            _audioSource.outputAudioMixerGroup = _musicGroup;
        }
        _audioSource.volume = data.volume;
        _audioSource.pitch = data.basePitch;
        if (data.randomizePitch)
        {
            _audioSource.pitch += Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
        }
        _audioSource.clip = data.clip;
        _audioSource.loop = data.loop;
        if (!data.loop)
        {
            float time = _audioSource.clip.length + 0.2f;
            DOVirtual.DelayedCall(time, () => PoolManager.instance.Push(this));
        }
        _audioSource.Play();
    }
    public void ResetItem()
    {

    }

    public void StopAndGoToPool()
    {
        _audioSource.Stop();
        PoolManager.instance.Push(this);
    }
}
