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

    public void PlayAlarmCoroutine()
    {
        bool isStay = true;

        RunCoroutine(isStay);
    }

    public void StopAlarmCoroutine()
    {
        bool isStay = false;

        RunCoroutine(isStay);
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private IEnumerator ChangeAlarmVolume(bool isStay)
    {
        bool isDone = false;

        if (_source.volume == 0 & isStay == true)
        {
            _source.Play();
        }

        while (isDone == false)
        {
            if (_source.volume < _maxVolume & isStay == true)
            {
                _recoveryRate = 0.05f;

                _source.volume = Mathf.MoveTowards(_source.volume, _maxStrength, _recoveryRate * Time.deltaTime);
            }
            else if (_source.volume > 0 & isStay == false)
            {
                _recoveryRate = -0.05f;

                _source.volume = Mathf.MoveTowards(_source.volume, _maxStrength, _recoveryRate * Time.deltaTime);
            }
            else
            {
                isDone = true;
            }

            yield return new WaitForEndOfFrame();
        }

        if (_source.volume == 0 & isStay == false)
        {
            _source.Stop();
        }        
    }

    private void RunCoroutine(bool isStay)
    {
        if (_changeAlarmVolumeCoroutine != null) 
        { 
            StopCoroutine(_changeAlarmVolumeCoroutine); 
        }

        _changeAlarmVolumeCoroutine = StartCoroutine(ChangeAlarmVolume(isStay));
    }
}
