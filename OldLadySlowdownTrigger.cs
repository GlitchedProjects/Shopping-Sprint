using Sandbox;
using Sandbox.Movement;

[Title ("Old Lady Slowdown Trigger")]
[Category ("Shopping-Sprint")]
[Icon ("hiking")]
public sealed class OldLadySlowdownTrigger : Component, Component.ITriggerListener
{
	[Property]
	[ReadOnly]
	public string Warn = "This requires a box collider set to trigger";

	[Property]
	public SoundPointComponent Sting;
	public void OnTriggerEnter( Collider other )
	{
		var plr = other.Components.Get<PlayerController>();

		if (plr != null)
        {
            Log.Info("Slow Down");
			plr.RunSpeed = 0;
			plr.WishVelocity = 0f;
			plr.DeaccelerationTime = 0.01f;
			plr.AirFriction = 1000000;
			plr.JumpSpeed = 0;
			Sting.StartSound();

			TimeSince since = 15;

			if (since > 15)
            {
                plr.UseInputControls = true;
				Log.Info("Controls Enabled");

            }
        }
	}
}
