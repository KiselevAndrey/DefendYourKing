using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorOverlay : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> actions;

    #region Update
    private void LateUpdate()
    {
        RotationToCamera();
    }
    #endregion

    #region Show/Hide
    public void Show()
    {
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

    }
    #endregion
}
