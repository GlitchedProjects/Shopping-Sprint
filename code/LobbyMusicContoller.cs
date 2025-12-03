using Sandbox;

public sealed class LobbyMusicContoller : Component, Component.ITriggerListener
{
	[Property]
	public SoundBoxComponent sond;
	[Property]
	public SoundPointComponent gameMusic;

	[Property]
	private bool Exit = false;

	public void OnTriggerExit( Collider other )
	{
		Exit = true;
		Log.Info("Skib");

		var plr = other.Components.Get<PlayerController>();
		var push = new Vector3(0f,500f,100f);

		if ( plr != null )
        {
			Log.Info("push");
            plr.Jump(push);
			plr.JumpSpeed = 250;
			plr.RunSpeed = 450;
        }
	}

	protected override void OnUpdate()
	{
		var sP = sond.Pitch;
		var sV = sond.Volume;

		var gmV = gameMusic.Volume;
		var gmP = gameMusic.Pitch;

		if( Exit == true )
        {
			// Log.Info("Exited is true");
            sond.Pitch = sP.LerpTo(0, 0.3f * Time.Delta);
			sond.Volume = sV.LerpTo(0, 0.3f * Time.Delta);

			gameMusic.StartSound();
			gameMusic.Pitch = gmP.LerpTo(0.7f, 1f * Time.Delta);
			gameMusic.Volume = gmV.LerpTo(1f, 0.7f * Time.Delta);
        }
	}

}
