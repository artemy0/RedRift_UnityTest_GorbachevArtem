using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InteractableHelper
{
    public const string INTERACTABLE_LAYER_MASK_NAME = "Interactable";
    public static int InteractableLayerMask => LayerMask.GetMask(INTERACTABLE_LAYER_MASK_NAME);

    public static bool TryRaycastInteractable(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, 50f, InteractableLayerMask);
    }
}
