using System;
using Microsoft.VisualBasic;
using Sandbox;

public sealed class Item : Component, Component.ITriggerListener
{
	[Property]
	public bool egg;
	[Property]
	public bool bread;

	public void OnTriggerEnter( Collider other )
	{
		var playa = other.Components.Get<PlayerController>();
		var listM = other.Components.Get<ItemManager>();

		if (playa != null)
       	{
			Log.Info("Player Collected");

			if (egg == true)
            {
				Log.Info("Egg Collected");
                listM.egg = true;
            }

			if (bread == true)
            {
                Log.Info("Bread Collected");
				listM.bread = true;
            }
        }
	}

	public struct ItemInfo
    {
        [KeyProperty] public string ItemName;
		[KeyProperty] public bool ItemBool;
    }
}
