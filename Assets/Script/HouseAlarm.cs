using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class HouseAlarm : MonoBehaviour
{
    private AudioSource _source;

    private float maxStrength = 1f;
    private float maxVolume = 1f;
    private float recoveryRate = 0f;

    public void PlayAlarmCoroutine()
    {
        var increaseAlarmVolume = IncreaseAlarmVolume();
        var decreaseAlarmVolume = DecreaseAlarmVolume();

        RunCoroutine(increaseAlarmVolume, decreaseAlarmVolume);
    }

    public void StopAlarmCoroutine()
    {
        var increaseAlarmVolume = IncreaseAlarmVolume();
        var decreaseAlarmVolume = DecreaseAlarmVolume();

        RunCoroutine(decreaseAlarmVolume, increaseAlarmVolume);
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private IEnumerator IncreaseAlarmVolume()
    {
        if (_source.volume == 0)
        {
            _source.Play();

            recoveryRate = 0.05f;

            _source.volume = Mathf.MoveTowards(_source.volume, maxStrength, recoveryRate * Time.deltaTime);
        }
        else if (_source.volume >= maxVolume)
        {
            recoveryRate = 0f;
        }
        else
        {
            recoveryRate = 0.05f;

            _source.volume = Mathf.MoveTowards(_source.volume, maxStrength, recoveryRate * Time.deltaTime);
        }

        yield return new WaitForEndOfFrame();        
    }

    private IEnumerator DecreaseAlarmVolume()
    {
        bool isDone = false;

        while (isDone == false)
        {
            if (_source.volume == 0)
            {
                _source.Stop();

                recoveryRate = 0f;

                isDone = true;
            }
            else
            {
                recoveryRate = -0.05f;

                _source.volume = Mathf.MoveTowards(_source.volume, maxStrength, recoveryRate * Time.deltaTime);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void RunCoroutine(IEnumerator launchableCoroutine, IEnumerator closableCoroutine)
    {
        if (closableCoroutine != null)
        {
            StartCoroutine(launchableCoroutine);
        }

        if (launchableCoroutine == null)
        {
            StopCoroutine(closableCoroutine);
        }
    }
}
