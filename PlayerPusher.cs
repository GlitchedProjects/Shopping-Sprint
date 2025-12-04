using Sandbox;

public sealed class PlayerPusher : Component, Component.ITriggerListener
{
	[Property]
	public Vector3 push = new Vector3(0,500,50);

	[Property]
	public bool singleUse = true;
	public void OnTriggerEnter( Collider other )
	{
		var plr = other.Components.Get<PlayerController>();

		if ( plr != null )
        {
			Log.Info("push");
            plr.Jump(push);
			plr.JumpSpeed = 250;
			plr.RunSpeed = 450;

			if (singleUse == true)
            {
                GameObject.Enabled = false;
            }
        }
	}
}
