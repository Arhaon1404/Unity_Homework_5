using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        recoveryRate = 0.05f;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _source.volume = Mathf.MoveTowards(_source.volume, maxStrength, recoveryRate * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        recoveryRate = -0.05f;
    }
}
