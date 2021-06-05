using UnityEngine;

public class UnitAnimatorsManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void IsStartMoveAnimation(bool isMoving) => animator.SetBool("Walk", isMoving);

    public void MeleeAttack() => animator.SetTrigger("MeleeAttack");
}
