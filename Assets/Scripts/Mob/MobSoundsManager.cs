public class MobSoundsManager : UnitsSoundsManager
{
    [UnityEngine.Header("Mob Sound Clips")]
    [UnityEngine.SerializeField] private UnityEngine.AudioClip meleeAttackClip;

    public void PlayMeleeAttackClip() => PlayClip(meleeAttackClip);
}
