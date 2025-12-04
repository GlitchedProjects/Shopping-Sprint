using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.Utility;

public sealed class LobbyDoorsTrigger : Component, Component.ITriggerListener
{
	[Property]
	public bool doorsOpen = false;

	[Property]
	public GameObject RightDoor;
	[Property]
	public GameObject leftDoor;
	[Property]
	public GameObject doorSoundOpen;
	[Property]
	public GameObject doorSoundClose;

	public void OnTriggerEnter( Collider other )
    {
		var plr = other.Components.Get<PlayerController>();
		if ( plr != null)
        {
            doorsOpen = true;
        }

		var dPOS = doorSoundClose.Components.Get<SoundPointComponent>();
		if (dPOS != null)
        {
			dPOS.PlayOnStart = true;
        }
    }

	public void OnTriggerExit( Collider other )
    {
        var plr = other.Components.Get<PlayerController>();
		if ( plr != null)
        {
            doorsOpen = false;
        }
    }

	protected override void OnUpdate()
	{
		
		var ldC = leftDoor.Components.Get<BoxCollider>();
		var rdC = RightDoor.Components.Get<BoxCollider>();

		var rP = RightDoor.LocalPosition;
		var rV2 = new Vector3(62, 0.7f, 0 );
		var rV1 = new Vector3(25.4f, 0.7f, 0 );
		var lP = leftDoor.LocalPosition;
		var lV2 = new Vector3(-62, 0.7f, 0 );
		var lV1 = new Vector3(-25.2f, 0.7f, 0 );

		if (doorsOpen == true)
        {
            RightDoor.LocalPosition = rP.LerpTo(rV2, 3f * Time.Delta);
			leftDoor.LocalPosition = lP.LerpTo(lV2, 3f * Time.Delta);
			doorSoundOpen.Enabled = true;
			doorSoundClose.Enabled = false;

			if(rdC != null && ldC != null)
            {
                rdC.IsTrigger = true;
				ldC.IsTrigger = true;
            }
            
        }
		else
        {
        	RightDoor.LocalPosition = rP.LerpTo(rV1, 3f * Time.Delta);
			leftDoor.LocalPosition = lP.LerpTo(lV1, 3f * Time.Delta);
			doorSoundClose.Enabled = true;
			doorSoundOpen.Enabled = false;

			if(rdC != null && ldC != null)
            {
				// Log.Info("FISH");
                rdC.IsTrigger = false;
				ldC.IsTrigger = false;
			}
        }
	}
}
