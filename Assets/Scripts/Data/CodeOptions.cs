using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies a list of possible codes to be used in the hacking minigame.
/// </summary>
[CreateAssetMenu(fileName = "CodeOptions", menuName = "ScriptableObjects/Code Options", order = 1)]
public class CodeOptions : ScriptableObject
{
    public List<string> codes;
}
