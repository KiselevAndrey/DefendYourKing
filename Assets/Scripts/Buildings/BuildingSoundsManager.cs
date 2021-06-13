public class BuildingSoundsManager : UnitsSoundsManager
{
    [UnityEngine.Header("Building Sound Clips")]
    [UnityEngine.SerializeField] private UnityEngine.AudioClip BuildClip;

    public void PlayBuildClip() => PlayClip(BuildClip);

    public void StopPlayClip() => audioSource.Stop();
}
