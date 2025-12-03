using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Sandbox;
using System.Collections.Generic;

public sealed class ItemManager : Component
{
	[Property]
	public bool egg;
	[Property]
	public bool bread;

	//  // This will show in the inspector as a list of bools.
    // [Property]
    // public List<Item> itemProperties { get; set; } = new();

    // // This adds a clickable button in the inspector.
    // [Button]
    // public void AddBool()
    // {
    //      itemProperties.Add(new Item()
    //     {
    //         Name = "New Bool",
    //         Value = false
    //     });
    // }
	// public struct Item
	// {
    // 	[KeyProperty] public string Name;
    // 	[KeyProperty] public bool Value;
	// }

}
