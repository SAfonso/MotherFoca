using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSeria {

        public int level = 0;
	    public stars[] allStars = new stars[3];
	    public bool wasPlayed = false;
	    public bool beforeWasPlayed = false;

		[System.Serializable]
		public struct stars{
			public bool wasCatched;
			public bool wasCatchedBefore;
			public int ID;
		}
}
