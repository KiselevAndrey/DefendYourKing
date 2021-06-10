using UnityEngine;
using DG.Tweening;

public class MoveCameraToSelected : MonoBehaviour
{
    public void NewTarget(Transform target)
    {
        print(target);
        transform.parent = target;
        transform.DOLocalMove(Vector3.zero, 1f);
    }
}
