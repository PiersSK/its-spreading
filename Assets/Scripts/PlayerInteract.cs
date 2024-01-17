using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float interactRange;
    [SerializeField] private float interactMaxAngle;

    [SerializeField] private Vector3 eyeOffset;
    [SerializeField] private LayerMask interactBlockLayers;

    [SerializeField] Color interactHighlightColor = new Color(0.3f, 0.3f, 0.3f);

    [Header("UI")]
    [SerializeField] TextMeshProUGUI promptTextUI;
    [SerializeField] TextMeshProUGUI interactableNameUI;
    [SerializeField] GameObject cyclePrompt;

    [Header("Debug Fields")]
    // Serialized for debugging only
    [SerializeField] private List<Interactable> interactablesInRange = new();
    [SerializeField] private Interactable selectedInteractable;


    private void Start()
    {
        
    }

    private void Update()
    {
        // Takes snapshot of interactables currently in range
        List<Interactable> initialInteractables = new();
        initialInteractables.AddRange(interactablesInRange);

        UpdateInteractablesInRange();

        // If interactablesInRange has changed (and there are any), highlight a "best interactable"
        if ((!interactablesInRange.All(initialInteractables.Contains)
            || interactablesInRange.Count != initialInteractables.Count)
            && interactablesInRange.Count > 0)
        {
            selectedInteractable = GetBestInteractable();
            HighlightSelectedObject();
        }

        ShowSelectedInteractablePrompt();
    }

    public void InteractWithSelected()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }

    public void CycleInteractable() {
        // If player presses Tab, cycle through interactables in range
        if (interactablesInRange.Count > 1)
        {
            int currentSelectedIndex = interactablesInRange.IndexOf(selectedInteractable);
            int newIndex = (currentSelectedIndex + 1) % interactablesInRange.Count;

            selectedInteractable = interactablesInRange[newIndex];
            HighlightSelectedObject();
        }
    }

    private void UpdateInteractablesInRange()
    {
        foreach (Interactable interactable in FindObjectsOfType<Interactable>())
        {
            float distance = Vector3.Distance(interactable.transform.position, transform.position);
            Vector3 direction = interactable.transform.position - transform.position;
            float angleToPlayer = Vector3.Angle(direction, transform.forward);

            // If the interactable is in range...
            if (distance < interactRange
                && angleToPlayer >= -interactMaxAngle
                && angleToPlayer <= interactMaxAngle
                && interactable.CanInteract()) // ...and it's currently interactable...
            {

                Ray ray = new(transform.position + eyeOffset, direction);
                Debug.DrawRay(ray.origin, ray.direction * distance);
                RaycastHit hitInfo = new();

                // ...and doesn't hit anything that would block it...
                if (!Physics.Raycast(ray, out hitInfo, distance, interactBlockLayers))
                {
                    // ... add it to the list (if not already there)
                    if (!interactablesInRange.Contains(interactable)) interactablesInRange.Add(interactable);
                }
            }
            else
            {
                // Remove from the list and unhighlight
                if (interactablesInRange.Contains(interactable)) interactablesInRange.Remove(interactable);
                if (selectedInteractable == interactable) selectedInteractable = null;
                SetObjectAndChildrenHighlight(interactable.transform, false); 
            }
        }
    }

    private void ShowSelectedInteractablePrompt()
    {
        promptTextUI.text = selectedInteractable == null ? string.Empty : "[E] " + selectedInteractable.promptText;
        interactableNameUI.text = selectedInteractable == null ? string.Empty : selectedInteractable.name;

        cyclePrompt.SetActive(interactablesInRange.Count > 1);
    }

    private void HighlightSelectedObject()
    {
        foreach (Interactable interatable in interactablesInRange)
            SetObjectAndChildrenHighlight(interatable.transform, false);

        SetObjectAndChildrenHighlight(selectedInteractable.transform, true);

    }

    public void SetObjectAndChildrenHighlight(Transform objectToHighlight, bool shouldHighlight)
    {
        SetObjectHighlight(objectToHighlight, shouldHighlight);

        foreach (Transform objectChild in objectToHighlight)
        {
            SetObjectAndChildrenHighlight(objectChild, shouldHighlight);
        }
    }

    private void SetObjectHighlight(Transform objectToHighlight, bool shouldHighlight)
    {
        if (objectToHighlight.GetComponent<Renderer>() != null)
        {
            foreach (Material material in objectToHighlight.GetComponent<Renderer>().materials)
            {
                if (shouldHighlight)
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", interactHighlightColor);
                }
                else
                {
                    material.DisableKeyword("_EMISSION");
                }
            }
        }
    }

    private Interactable GetBestInteractable()
    {
        float minDistance = Mathf.Infinity;
        Interactable toSelect = null;
        foreach(Interactable interactable in interactablesInRange)
        {
            float distanceToInteractable = Vector3.Distance(interactable.transform.position, transform.position);
            if (distanceToInteractable < minDistance)
            {
                minDistance = distanceToInteractable;
                toSelect = interactable;
            }
        }

        return toSelect;
    }


}
