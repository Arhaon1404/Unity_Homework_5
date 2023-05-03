using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class HouseAlarm : MonoBehaviour
{
    private AudioSource _source;

    private float _maxStrength = 1f;
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
        float numberDeterminant;

        if (target == _maxVolume)
        {
            _source.Play();

            numberDeterminant = 1;

            while (_source.volume < target)
            {
                Change(numberDeterminant);

                yield return null;
            }
        }
        else if (target == 0f)
        {
            numberDeterminant = -1;

            while (_source.volume > target)
            {
                Change(numberDeterminant);
                
                yield return null;
            }

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

    private void Change(float numberDeterminant)
    {
        _recoveryRate = 0.05f * numberDeterminant;

        _source.volume = Mathf.MoveTowards(_source.volume, _maxStrength, _recoveryRate * Time.deltaTime);
    }
}
