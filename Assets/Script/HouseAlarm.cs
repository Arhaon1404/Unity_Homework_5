using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class HouseAlarm : MonoBehaviour
{
    private AudioSource _source;

    private float maxStrength = 1f;
    private float recoveryRate = 0f;

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    void Update()
    {
        _source.volume = Mathf.MoveTowards(_source.volume, maxStrength, recoveryRate * Time.deltaTime);
    }

    public void PlayAlarmCoroutine()
    {
        StopCoroutine(StopAlarm());

        StartCoroutine(PlayAlarm());
    }

    public void StopAlarmCoroutine()
    {
        StopCoroutine(PlayAlarm());

        StartCoroutine(StopAlarm());
    }

    private IEnumerator PlayAlarm()
    {
        _source.Play();

        recoveryRate = 0.05f;

        yield return new WaitUntil(() => _source.volume == 1);

        recoveryRate = 0f;
    }

    private IEnumerator StopAlarm()
    {
        recoveryRate = -0.05f;

        yield return new WaitUntil(() => _source.volume == 0);

        _source.Stop();

        recoveryRate = 0f;
    }
}
