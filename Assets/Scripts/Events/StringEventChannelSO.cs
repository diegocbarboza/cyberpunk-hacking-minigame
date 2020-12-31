using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Specifies an event channel with a string parameter.
/// </summary>
[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, string> OnEventRaised;

	public void RaiseEvent(GameObject sender, string value)
	{
		if (OnEventRaised != null)
		{
			OnEventRaised.Invoke(sender, value);
		}
	}
}
