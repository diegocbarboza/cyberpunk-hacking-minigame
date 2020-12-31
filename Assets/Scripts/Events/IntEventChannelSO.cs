using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specifies an event channel with an Int parameter.
/// </summary>
[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, int> OnEventRaised;

	public void RaiseEvent(GameObject sender, int value)
	{
		if (OnEventRaised != null)
		{
			OnEventRaised.Invoke(sender, value);
		}
	}
}
