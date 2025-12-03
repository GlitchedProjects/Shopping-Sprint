using Sandbox;

public sealed class TargetGizmo : Component
{
	[Property]
	public GameObject Target;
	[Property]
	public GameObject Target2;
	[Property]
	public GameObject Target3;
	[Property]
	public GameObject Target4;

	protected override void DrawGizmos()
	{
		var P1 = Target.LocalPosition;
		var P2 = Target2.LocalPosition;
		var P3 = Target3.LocalPosition;
		var P4 = Target4.LocalPosition;

		Gizmo.Draw.Color = Color.Red;
		Gizmo.Draw.SolidSphere(P1,5f);
		Gizmo.Draw.Line(P1, P2);

		Gizmo.Draw.Color = Color.Orange;
		Gizmo.Draw.SolidSphere(P2,5f);
		Gizmo.Draw.Line(P2, P3);

		Gizmo.Draw.Color = Color.Yellow;
		Gizmo.Draw.SolidSphere(P3,5f);
		Gizmo.Draw.Line(P3, P4);

		Gizmo.Draw.Color = Color.Green;
		Gizmo.Draw.SolidSphere(P4,5f);
		Gizmo.Draw.Line(P4, P1);
	}
}
