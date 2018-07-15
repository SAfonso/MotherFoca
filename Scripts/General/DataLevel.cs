using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevel : MonoBehaviour {

        public int level = 0;
	    public stars[] allStars = new stars[3];
	    public bool wasPlayed = false;
	    public bool beforeWasPlayed = false;

		public struct stars{
			public bool wasCatched;
			public bool wasCatchedBefore;
			public int ID;
		}

		public void SetDefault(int index){
			this.allStars[index].wasCatched = false;
			this.allStars[index].wasCatchedBefore = false;
			this.allStars[index].ID = 0;
		}

		public void Default(){
			this.wasPlayed = false;
		}

		public void SetStarPos(int index){
			allStars[index].ID = index;
		}

		public void GotCha(int index){
			allStars[index].wasCatched = true;
		}

		public void SaveData(DataLevel value){
			//Debug.Log("Estamos en " + this.level);

			this.allStars = value.allStars;
			this.wasPlayed = true;
			//Debug.Log("Vamos a guardar " + value.beforeWasPlayed);
			this.beforeWasPlayed = value.beforeWasPlayed;
		}

		public DataLevel GetData(){
			DataLevel aux = new DataLevel();

			aux.level = this.level;
			aux.allStars = this.allStars;
			aux.wasPlayed = this.wasPlayed;
			aux.beforeWasPlayed = this.beforeWasPlayed;

			return aux;
		}

		public stars[] GetStars(){
			return allStars;
		}
}
