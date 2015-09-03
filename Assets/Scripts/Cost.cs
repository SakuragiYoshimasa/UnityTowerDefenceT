using UnityEngine;
using System.Collections;

public class Cost {

	public static int TowerProduction(){
		int num =  Tower.parent.Count();

		int basic = 8;
		float ratio = Mathf.Pow(1.3f,num);
		return (int)(basic * ratio );
	}

	public static bool isSatisfyMoney(){
		return Global.Monney > TowerProduction(); 
	}

	public static int TowerUpgrade(Tower.eUpgrade type, int lv){
		float cost = 0;
			switch(type){
			case Tower.eUpgrade.Range:
				cost = 10 * Mathf.Pow(1.5f,(lv -1));
				break;

			case Tower.eUpgrade.Firerate:
				cost = 15 * Mathf.Pow(1.5f,(lv -1));
				break;
			case Tower.eUpgrade.Power:
				cost = 20 * Mathf.Pow(1.5f,(lv -1));
				break;
		}

		return (int)cost;
	}

}
