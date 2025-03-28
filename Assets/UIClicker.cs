using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIClicker : MonoBehaviour
{
    public Camera playerCamera;
    private NonVRController controllerScript;

    void Start()
    {
        // Find and cache the movement controller
        controllerScript = GetComponent<NonVRController>();

        // Set the player camera for all world space canvases
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                canvas.worldCamera = playerCamera;
            }
        }
    }

    void Update()
    {
        // Handle UI click
        if (Input.GetMouseButtonDown(0))
        {
            ClickUI();
        }

        // Disable movement if focused on an InputField
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            TMP_InputField field = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
            if (field != null && field.isFocused)
            {
                if (controllerScript != null) controllerScript.enabled = false;

                // Optional: listen for submit to re-enable movement
                field.onEndEdit.AddListener(OnInputFieldSubmit);
            }
        }
    }

    void ClickUI()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Canvas canvas = hit.collider.GetComponentInParent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
            {
                GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
                if (raycaster != null)
                {
                    PointerEventData pointerData = new PointerEventData(EventSystem.current)
                    {
                        position = playerCamera.WorldToScreenPoint(hit.point)
                    };

                    List<RaycastResult> results = new List<RaycastResult>();
                    raycaster.Raycast(pointerData, results);

                    foreach (RaycastResult result in results)
                    {
                        ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
                        break;
                    }
                }
                else
                {
                    Debug.Log("No GraphicRaycaster found on the canvas.");
                }
            }
            else
            {
                Debug.Log("No canvases found.");
            }
        }
        else
        {
            Debug.Log("I hit nothing.");
        }
    }

    void OnInputFieldSubmit(string input)
    {
        // Re-enable movement when input is submitted
        if (controllerScript != null) controllerScript.enabled = true;

        // Unsubscribe to prevent duplicate listeners
        TMP_InputField field = EventSystem.current.currentSelectedGameObject?.GetComponent<TMP_InputField>();
        if (field != null)
        {
            field.onEndEdit.RemoveListener(OnInputFieldSubmit);
        }
    }
}
