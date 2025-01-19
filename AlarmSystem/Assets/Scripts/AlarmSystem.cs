using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _timeOfTransition;
    [SerializeField] private PresenceSensor _presenceSensor;

    private bool _wasPreviousCheckSuccessful = false;
    private bool _isCloseCrook = false;
    private Coroutine _volumeChanger;

    private void Start()
    {
        _audio.volume = 0;
    }

    private void OnEnable()
    {
        _presenceSensor.CrookBeenFound += TurnOnSound;
        _presenceSensor.CrookBeenLost += TurnOffSound;
    }

    private void OnDisable()
    {
        _presenceSensor.CrookBeenFound -= TurnOnSound;
        _presenceSensor.CrookBeenLost -= TurnOffSound;
    }

    private IEnumerator SmoothVolumeChange(float targetVolume)
    {
        float elapsedTime = 0f;
        float startVolume = _audio.volume;

        while (_audio.volume != targetVolume)
        {
            float normalizedPosition = elapsedTime / _timeOfTransition;

            elapsedTime += Time.deltaTime;
            _audio.volume = Mathf.Lerp(startVolume, targetVolume, normalizedPosition);

            yield return null;
        }
    }

    private void TurnOnSound()
    {
        _audio.Play();

        RestartVolumeChange(1);
    }

    private void TurnOffSound()
    {
        RestartVolumeChange(0);
    }

    private void RestartVolumeChange(float targetVolume) 
    {
        _volumeChanger = StartCoroutine(SmoothVolumeChange(targetVolume));
    }
}
