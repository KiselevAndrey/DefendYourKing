using UnityEngine;

public class SelectedCell : MonoBehaviour, ISelectable
{
    [Header("Parameters")]
    [Range(0, 5)] public int numberOfCell;

    [Header("References")]
    [SerializeField] private UnityEngine.UI.Image icon;

    [Header("Events")]
    [SerializeField] private UnityEngine.Events.UnityEvent OnClick;

    #region Deselect Select
    public void Deselect()
    {        
    }

    public bool SelectAndDeselectPrevious()
    {
        OnClick.Invoke();
        return false;
    }
    #endregion

    public void SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
