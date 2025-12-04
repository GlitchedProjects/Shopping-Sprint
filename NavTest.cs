using System.Diagnostics;
using Sandbox;
using Sandbox.Navigation;

[Title ("Navigate Points")]
[Category ("Shopping-Sprint")]
[Icon ("flip_camera_android")]
public sealed class NavTest : Component
{
	[Property]
	public GameObject Target;
	[Property]
	public GameObject Target2;
	[Property]
	public GameObject Target3;
	[Property]
	public GameObject Target4;

	[Property]
	public NavMeshAgent navAGENT;
	public NavMeshArea navAREA;
	public NavMesh navALL;
	public NavMeshPathPoint navPoint;
	public NavMeshPath navPath;

	protected override void OnStart()
	{
		navAGENT.MoveTo(Target.WorldPosition);
	}
}
