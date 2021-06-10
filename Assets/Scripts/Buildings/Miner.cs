using UnityEngine;

public class Miner : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEngine.Events.UnityEvent OnMineHit;

    private void MineHit()
    {
        OnMineHit.Invoke();
    }
}
