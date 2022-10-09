using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public event Action OnSelected;
    public event Action OnReleased;
    public event Action OnDroped;

    private bool isStartInteract;
    private Vector3 startInteractPosition;
    private Quaternion startInteractRotation;

    private bool isInteractable;

    public void SetInteractableActive(bool value)
    {
        isInteractable = value;
    }

    private void OnMouseDown()
    {
        if (isInteractable)
        {
            isStartInteract = true;

            startInteractPosition = transform.position;
            startInteractRotation = transform.rotation;

            OnSelected?.Invoke();
        }
    }

    private void OnMouseDrag()
    {
        if (isInteractable && isStartInteract)
        {
            if (TryRaycastDropArea(out RaycastHit hit, out DropArea dropArea))
            {
                transform.position = dropArea.HitToDropAreaPoin(hit.point);
                transform.forward = -hit.normal;
            }
            else
            {
                transform.position = startInteractPosition;
                transform.rotation = startInteractRotation;
            }
        }
    }

    private void OnMouseUp()
    {
        if (isInteractable && isStartInteract)
        {
            isStartInteract = false;

            if (TryRaycastDropArea())
            {
                OnDroped?.Invoke();
            }
            else
            {
                OnReleased?.Invoke();
            }
        }
    }

    private bool TryRaycastDropArea()
    {
        var isRaycastDropArea = TryRaycastDropArea(out RaycastHit _, out DropArea _);

        return isRaycastDropArea;
    }

    private bool TryRaycastDropArea(out RaycastHit hit, out DropArea dropArea)
    {
        dropArea = null;
        var isRaycastDropArea = 
            InteractableHelper.TryRaycastInteractable(out hit) &&
            hit.transform.TryGetComponent(out dropArea);

        return isRaycastDropArea;
    }
}
