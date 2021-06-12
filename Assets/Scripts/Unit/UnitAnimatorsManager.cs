using UnityEngine;
using UnityEngine.Events;

public class UnitAnimatorsManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Animator animator;

    [Header("Event")]
    [SerializeField] private UnityEvent OnMeleeAttack;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent OnDestroy;

    private void OnEnable()
    {
        animator.SetTrigger("Spawn");
    }

    #region Start Animations
    public void IsStartMoveAnimation(bool isMoving) => animator.SetBool("Walk", isMoving);

    public void StartMeleeAttackAnimation() => animator.SetTrigger("MeleeAttack");
    #endregion

    #region Actions
    private void MeleeAttackAction() => OnMeleeAttack.Invoke();

    private void Death() => OnDeath.Invoke();

    private void Destroy() => OnDestroy.Invoke();
    #endregion
}
