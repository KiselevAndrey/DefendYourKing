using System.Collections.Generic;
using UnityEngine;

public class SelectorOverlay : MonoBehaviour
{
    public static SelectorOverlay Instance { get; private set; }

    [SerializeField] private Transform cameraPlant;
    [SerializeField] private List<SpriteRenderer> actions;

    #region Awake Update
    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void LateUpdate()
    {
        RotationToCamera();
    }
    #endregion

    #region Show/Hide
    public void Show(IBuilding building)
    {
        RotationToCamera();
        transform.position = building.GetPosition();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region RotationToCamera
    private void RotationToCamera()
    {
        transform.rotation = cameraPlant.rotation;
    }
    #endregion
}
