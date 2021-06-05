using UnityEngine;
using UnityEngine.Events;

public class UnitAnimatorsManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Animator animator;

    [Header("Event")]
    [SerializeField] private UnityEvent OnMeleeAttack;

    #region Start Animations
    public void IsStartMoveAnimation(bool isMoving) => animator.SetBool("Walk", isMoving);

    public void StartMeleeAttackAnimation() => animator.SetTrigger("MeleeAttack");
    #endregion

    #region Actions
    public void MeleeAttackAction() => OnMeleeAttack.Invoke();
    #endregion
}
