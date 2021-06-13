using UnityEngine;

public class UnitsSoundsManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] protected AudioSource audioSource;

    [Header("Unit Sound Clips")]
    [SerializeField] private AudioClip deathClip;

    public void PlayDeathClip() => PlayClip(deathClip);

    protected void PlayClip(AudioClip audioClip)
    {
        ChangePitch();
        audioSource.PlayOneShot(audioClip);
    }

    protected void ChangePitch() => audioSource.pitch = Random.Range(0.9f, 1.1f);
}
