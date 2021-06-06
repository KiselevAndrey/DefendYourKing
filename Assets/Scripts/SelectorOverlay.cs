using System.Collections.Generic;
using UnityEngine;

public class SelectorOverlay : MonoBehaviour
{
    public static SelectorOverlay Instance { get; private set; }

    [Header("Parameters")]
    [SerializeField] private int maxActions = 5;

    [Header("References")]
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
        transform.position = building.Position;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(IBuyer buyer)
    {
        RotationToCamera();
        transform.position = buyer.Position;

        for (int i = 0; i < buyer.Purshases.Length; i++)
        {
            actions[i].gameObject.SetActive(true);
            actions[i].sprite = buyer.Purshases[i].icon;
        }

        for (int i = buyer.Purshases.Length; i < maxActions; i++)
        {
            actions[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }
    #endregion

    #region RotationToCamera
    private void RotationToCamera()
    {
        transform.rotation = cameraPlant.rotation;
    }
    #endregion
}
