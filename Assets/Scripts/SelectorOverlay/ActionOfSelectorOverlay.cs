using UnityEngine;

public class ActionOfSelectorOverlay : UnityEngine.MonoBehaviour, ISelectable
{
    public bool NeedHidePrevios => false;

    public Transform Transform => null;

    public void Deselect()
    {
    }

    public void Select()
    {
    }
}
