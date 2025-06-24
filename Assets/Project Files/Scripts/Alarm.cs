using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

    [SerializeField] private TriggerZone _triggerZone;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _fadeSpeed;

    private AudioSource _audioSource;
    private Coroutine _fadeVolumeRoutine;

    private int _currentEntryCount;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _audioClip;
    }

    private void OnEnable()
    {
        _triggerZone.Entered += Entered;
        _triggerZone.Exited +=  Exited;
    }

    private void OnDisable()
    {
        _triggerZone.Entered -= Entered;
        _triggerZone.Exited -= Exited;
    }

    private void Entered(Collider collider)
    {
        if (collider.GetComponent<Thief>() == null)
            return;

        if (_currentEntryCount == 0)
            StartFadeVolume(MaxVolume);

        _currentEntryCount++;
    }

    private void Exited(Collider collider)
    {
        if (collider.GetComponent<Thief>() == null)
            return;

        _currentEntryCount = Mathf.Max(0, _currentEntryCount - 1);

        if (_currentEntryCount == 0)
            StartFadeVolume(MinVolume);
    }

    private void StartFadeVolume(float targetVolume)
    {
        if(_fadeVolumeRoutine != null)
            StopCoroutine( _fadeVolumeRoutine);

        if(_audioSource.isPlaying == false)
            _audioSource.Play();

        _fadeVolumeRoutine = StartCoroutine(FadeVolume(targetVolume));
    }

    private IEnumerator FadeVolume(float targetVolume)
    {
        while(Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _fadeSpeed * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(targetVolume, MinVolume))
            _audioSource.Stop();

        _fadeVolumeRoutine = null;
    }
}
