using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specifies an event channel with a bool parameter.
/// </summary>
[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
public class BoolEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, bool> OnEventRaised;

	public void RaiseEvent(GameObject sender, bool value)
	{
		if (OnEventRaised != null)
		{
			OnEventRaised.Invoke(sender, value);
		}
	}
}
