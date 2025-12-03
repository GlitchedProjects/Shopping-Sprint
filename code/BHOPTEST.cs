using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Threading;
using System.Timers;
using Sandbox;
using Sandbox.Rendering;
using Sandbox.Services;
using Sandbox.VR;

public sealed class BHOPTEST : Component
{
	[Property]
	public PlayerController playerController { get; set; }
	[Property]
	public CameraComponent camera {get; set;}
	[Property]
	public SoundPointComponent windSound {get; set;}
	[Property]
	public SoundPointComponent musicSound {get; set;}
	[Property]
	public GameObject speedParticles;

	[Property]
	public bool AutoHop = true;

	public float StartAF = 0.3f;
	public float StartJS = 200f;
	public float StartGF = 0.5f;
	public float StartDS = 70f;

	public float ogFOV = 100f;
	public float wsVOL = 0f;
	public float wpPITCH = 1f;

	protected override void OnUpdate()
	{
		playerController.GroundFriction = 1f;

		var Mb = camera.GetComponent<MotionBlur>();
		var Ws = windSound.Volume;
		var Wp = windSound.Pitch;
		var mS = musicSound.Volume;
		var mP = musicSound.Pitch;

		var af = playerController.AirFriction;
		var js = playerController.JumpSpeed;
		var gf = playerController.DeaccelerationTime;
		var bp = playerController.BrakePower;
		var rs = playerController.RunSpeed;
		var ds = playerController.DuckedSpeed;

		var fov = camera.FieldOfView;

		playerController.AirFriction = af.Clamp(-0.2f, 0.3f);
		playerController.DeaccelerationTime = gf.Clamp(0.5f, 10f);
		playerController.JumpSpeed = js.Clamp(50f, 300f);
		playerController.BrakePower = bp.Clamp(0.1f, 1000f);
		playerController.RunSpeed = rs.Clamp(0f, 1250f);
		Mb.Scale = Mb.Scale.Clamp(0.01f, 1f);
		ds = ds.Clamp(0f, 70f);

		Ws = Ws.Clamp(0f,1f);
		Wp = Wp.Clamp(1f,2f);
		mS = mS.Clamp(0f, 1f);
		mP = mP.Clamp(0f, 1.2f);
	
		camera.FieldOfView = fov.Clamp(120, 140f);

		var vel = playerController.Velocity;

		if (playerController.RunSpeed >= 650f)
        {
			camera.FieldOfView = fov.LerpTo(130, 0.4f * Time.Delta);
			
			Mb.Scale += 0.1f;

			windSound.Volume = Ws.LerpTo(0.5f,0.2f * Time.Delta);
			windSound.Pitch = Wp.LerpTo(2f, 0.1f * Time.Delta);

			musicSound.Volume = mS.LerpTo(1f, 0.1f * Time.Delta);
			musicSound.Pitch = mP.LerpTo(1.2f, 0.3f * Time.Delta);

			speedParticles.Enabled = true;
        }
		else
        {
            camera.FieldOfView = fov.LerpTo(120, 1f * Time.Delta);
			windSound.Volume = Ws.LerpTo(0f,1f * Time.Delta);
			windSound.Pitch = Wp.LerpTo(1f, 1f * Time.Delta);

			speedParticles.Enabled = false;

			// musicSound.Volume = mS.LerpTo(0.5f, 0.2f * Time.Delta);
			// musicSound.Pitch = mP.LerpTo(0.7f, 0.4f * Time.Delta);
        }

		// if (playerController.IsOnGround && playerController.TimeSinceUngrounded < 1f)
        // {
		// 	Log.Info("youve been grounded");
        //     camera.FieldOfView = fov.LerpTo(110, 1f * Time.Delta);
        // }

		if (Input.Down("Duck") && playerController.IsOnGround)
        {
			Log.Info("Grounded Duck");
			var fJ = new Vector3(0,0,10f);

            playerController.DeaccelerationTime = 10f;

			// playerController.Jump(fJ);
        }
		else
        {
            playerController.WishVelocity = StartDS;
        }

		if (playerController.TimeSinceUngrounded < 0.5f)
		{
			Log.Info("working jump ts");
			
			if (Input.Down("Jump"))
            {
				var V = new Vector3(playerController.WishVelocity.x,playerController.WishVelocity.y,js);

                playerController.AirFriction += 0.003f;
				playerController.RunSpeed += 1f;
				playerController.JumpSpeed += 1f;
				playerController.DeaccelerationTime += 0.05f;
				playerController.BodyMass += 1f;
				playerController.BrakePower -= 0.1f;
				playerController.GroundFriction = 0f;

				// camera.FieldOfView = fov.LerpTo(120, 0.2f * Time.Delta);
				

				if (AutoHop == true)
                {
                    var vJ = new Vector3(0,0,js);

					if (playerController.IsOnGround)
                	{
						TimeSince timeSince = 23f;
						if (timeSince <= 23f)
                    	{
							// camera.FieldOfView = fov.LerpTo(120, 0.2f * Time.Delta);
                        	playerController.Jump(vJ);
							Log.Info("ts works");
                    	}
                    
                	}
                }

            }
			
		}
		else
		{
			playerController.AirFriction = StartAF;
			playerController.JumpSpeed = StartJS;

			if (!playerController.IsDucking)
            {
                playerController.DeaccelerationTime = StartGF;
            }
			
			playerController.BodyMass = 500;
			playerController.BrakePower = 1f;
			playerController.GroundFriction = 1f;
			playerController.RunSpeed = 310f;

			Mb.Scale = 0.1f;
		}
	}
}
