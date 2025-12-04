using Sandbox;

[Title ("Navigation Points")]
[Category ("Shopping-Sprint")]
[Icon ("panorama_fish_eye")]
public sealed class OldLadyNavTrigger : Component, Component.ITriggerListener
{
	[Property]
	public GameObject nextTarget;
	public void OnTriggerEnter( Collider other )
	{
		var agent = other.Components.Get<NavMeshAgent>();
		var OLA = other.Components.Get<NavTest>();

		if ( agent != null )
        {
			Log.Info("TOUCH");
			OLA.navAGENT.MoveTo(nextTarget.WorldPosition);
        }
	}
}
