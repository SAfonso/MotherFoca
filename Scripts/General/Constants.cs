using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

	public static int coins = 0;
	public static int energy = 30;
	public static int maxEnergy = 30;
	public static float time = 0;
	public static int powerUps = 0;
	public static int currentLevel = 1;
	public static int actualPage = 0;

	public static List<GameObject> dataLevel = new List<GameObject>();

	public static bool firstTimePlay = true;

	public static string datetime;

	public static float acumulatedTime;

	public static bool reloaded = false;
}
