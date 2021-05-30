using UnityEngine;
using DG.Tweening;

public class MobMove : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private float speed;

    [HideInInspector] public Vector3 directionToTarget;

    public void MoveToTarget(Vector3 targetPosition)
    {
        RotateToTarget(targetPosition);

        
        transform.DOMove(transform.position + directionToTarget.normalized * speed * Time.deltaTime, Time.deltaTime);
    }

    public void RotateToTarget(Vector3 targetPosition)
    {
        directionToTarget = GetDirectionToTarget(targetPosition);
        
        if(directionToTarget != Vector3.zero)
            transform.forward = directionToTarget;
    }

    public Vector3 GetDirectionToTarget(Vector3 targetPosition)
    {
        return targetPosition - transform.position;
    }
}
