using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopPrices", menuName = "SO/ShopPrices", order = 1)]
public class SO_ShopPrices : ScriptableObject {
	[System.Serializable]
    public struct Item
	{
		public int amount;
		public int price;
	}

	public List<Item> PU = new List<Item>();
	public List<Item> Energy = new List<Item>();

	[System.Serializable]
	public struct DiamondItem
	{
		public int amount;
		public float price;
	}

	public List<DiamondItem> Diamond = new List<DiamondItem>();

}
