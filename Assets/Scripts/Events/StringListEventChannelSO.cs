using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specifies an event channel with a string list parameter.
/// </summary>
[CreateAssetMenu(menuName = "Events/String List Event Channel")]
public class StringListEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, List<string>> OnEventRaised;

	public void RaiseEvent(GameObject sender, List<string> value)
	{
		if (OnEventRaised != null)
		{
			OnEventRaised.Invoke(sender, value);
		}
	}
}
