using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class HouseAlarm : MonoBehaviour
{
    private AudioSource _source;

    private float _maxVolume = 1f;
    private float _recoveryRate = 0f;

    private Coroutine _changeAlarmVolumeCoroutine;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        float target = _maxVolume;

        RunCoroutine(target);
    }

    public void Stop()
    {
        float target = 0f;

        RunCoroutine(target);
    }

    private IEnumerator ChangeAlarmVolume(float target)
    {
        if (_source.volume == 0)
        {
            _source.Play();
        }

        while (_source.volume < target)
        {
            Change(target);

            yield return null;
        }

        while (_source.volume > target)
        {
             Change(target);
                
           yield return null;
        }

        if (_source.volume == 0)
        {
            _source.Stop();
        }
    }

    private void RunCoroutine(float target)
    {
        if (_changeAlarmVolumeCoroutine != null) 
        { 
            StopCoroutine(_changeAlarmVolumeCoroutine); 
        }

        _changeAlarmVolumeCoroutine = StartCoroutine(ChangeAlarmVolume(target));
    }

    private void Change(float target)
    {
        _recoveryRate = 0.05f;

        _source.volume = Mathf.MoveTowards(_source.volume, target, _recoveryRate * Time.deltaTime);
    }
}
