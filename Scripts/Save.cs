using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemiumGames.Saveable;

public class Save {

public static Save instance;
public SaveGame save;

[System.Serializable]
public class SaveGame{
	public int coins = 0;
	public int energy = 30;
	public int maxEnergy = 30;
	public float time = 0;
	public int powerUps = 0;
	public int currentLevel = 1;
	public int actualPage = 0;

	public List<DataSeria> dataLevel;

	public bool firstTimePlay = true;

	public string datetime;

	public float acumulatedTime;
}

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
void Awake()
{
	instance = this;
}

public void SaveData(Save pepe){
	JsonFormatterHelper.Save(pepe.save, Application.persistentDataPath, "data.json", true);
}

public void LoadData()
{
	save = JsonFormatterHelper.Load<SaveGame>(Application.persistentDataPath, "data.json", true); 
}


}