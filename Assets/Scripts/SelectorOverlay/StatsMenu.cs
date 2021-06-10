using UnityEngine;

public class StatsMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MoveCameraToSelected moveCameraToSelected;

    public void SelectUnit(ISelectableUnit selectableUnit)
    {
        moveCameraToSelected.NewTarget(selectableUnit.Transform);
    }
}
