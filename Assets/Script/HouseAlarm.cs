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

    public void PlayAlarmCoroutine()
    {
        float target = _maxVolume;

        RunCoroutine(target);
    }

    public void StopAlarmCoroutine()
    {
        float target = 0f;

        RunCoroutine(target);
    }

    private IEnumerator ChangeAlarmVolume(float target)
    {
        if (target == _maxVolume)
        {
            _source.Play();

            while (_source.volume < target)
            {
                _recoveryRate = 0.05f;

                _source.volume = Mathf.MoveTowards(_source.volume, _maxStrength, _recoveryRate * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }
        else if (target == 0f)
        {
            while (_source.volume > target)
            {
                _recoveryRate = -0.05f;

                _source.volume = Mathf.MoveTowards(_source.volume, _maxStrength, _recoveryRate * Time.deltaTime);
                
                yield return new WaitForEndOfFrame();
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
}
