using UnityEngine;

public class Miner : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEngine.Events.UnityEvent OnMineHit;

    public void MineHit()
    {
        OnMineHit.Invoke();
    }
}
