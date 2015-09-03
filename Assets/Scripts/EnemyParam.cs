using UnityEngine;
using System.Collections;

public class EnemyParam  {

	public static int Hp(){
		return 1 + (Global.Wave / 3); 
	}

	public static float Speed(){
		return 3.0f + (Global.Wave / 3);
	}

	public static int Money(){
		if(Global.Wave < 5){
			return 2;
		}

		return 1;
	}

	public static int GenerationNumber(){
		return 5 + Global.Wave;
	}

	public static float GenerationInterval(){
		return 1.5f;
	}
}
