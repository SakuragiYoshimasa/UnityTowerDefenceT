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

}
