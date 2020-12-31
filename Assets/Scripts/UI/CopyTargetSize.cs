using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class to copy X or Y size from another RectTransform.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class CopyTargetSize : MonoBehaviour
{
    [SerializeField]
    private RectTransform target;

    [SerializeField]
    private bool copyX;

    [SerializeField]
    private bool copyY;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
        
    void Update()
    {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        if (copyX) sizeDelta.x = target.sizeDelta.x;
        if (copyY) sizeDelta.y = target.sizeDelta.y;
        rectTransform.sizeDelta = sizeDelta;
    }
}
