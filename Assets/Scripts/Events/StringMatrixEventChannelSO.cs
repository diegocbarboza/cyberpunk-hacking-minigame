using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specifies an event channel with a bidimensional string array.
/// </summary>
[CreateAssetMenu(menuName = "Events/String Matrix Event Channel")]
public class StringMatrixEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, string[,]> OnEventRaised;

	public void RaiseEvent(GameObject sender, string[,] value)
	{
		if (OnEventRaised != null)
		{
			OnEventRaised.Invoke(sender, value);
		}
	}
}
