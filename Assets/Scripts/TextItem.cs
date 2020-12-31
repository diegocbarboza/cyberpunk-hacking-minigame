using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This helper class is used to represent a game object with a text field.
/// </summary>
public class TextItem : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public Text TextObject { get => text; }
}
