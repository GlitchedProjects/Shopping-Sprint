using Sandbox;
using Sandbox.VR;

[Title ("Hit Wall Stopper")]
[Category ("Shopping-Sprint")]
[Icon ("waving_hand")]
public sealed class StopperTrigger : Component
{
	[Property]
	public CameraComponent camera;

	[Property]
	public PlayerController player;

	[Property]
	public bool gizmo;
	[Property]
	[Range (12, 24)]
	public int StopRange;

	// public void OnTriggerEnter( Collider other )
	// {
	// 	var plr = other.Components.Get<PlayerController>();

	// 	if( other != null && !other.Tags.Has("player"))
    //     {
    //         Log.Info("hit something");
	// 		GetComponent<PlayerController>();

	// 		if (plr != null)
    //         {
    //             plr.RunSpeed = 250f;
	// 			Log.Info("Player hit something");
    //         }

    //     }
	// }

	protected override void OnUpdate()
	{
		var camPos = camera.WorldPosition;
		var camRot = camera.WorldRotation;

		var camray = Scene.Trace.FromTo(camPos, camPos + player.EyeAngles.Forward * StopRange).WithoutTags("player", "physical").Run();

		Gizmo.Draw.Color = new Vector4(0,0,1,0.2f);

		if (camray.Hit)
        {
			Log.Info("player face planted");
            player.RunSpeed = 100;
			player.JumpSpeed = 100;
			player.DeaccelerationTime = 0;
			Gizmo.Draw.Color = new Vector4(1,0,0,0.2f);
        }

		if (gizmo == true)
        {
     		Gizmo.Draw.SolidSphere(camray.EndPosition, 2f);
        }
		else
        {
            return;
        }
	}
}
