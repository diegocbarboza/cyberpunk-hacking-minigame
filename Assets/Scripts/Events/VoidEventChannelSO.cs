using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specifies an event channel with no additional parameters.
/// </summary>
[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject> OnEventRaised;

	public void RaiseEvent(GameObject sender)
	{
		if (OnEventRaised != null)
		{
			OnEventRaised.Invoke(sender);
		}
	}
}
