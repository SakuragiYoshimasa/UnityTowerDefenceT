using UnityEngine;
using System.Collections;

public class TowerParam  {

	public static float Range(int lv){
		float size = Field.GetChipSize();
		return size + (0.5f * size * lv);
	}

	public static float Firerate(int lv){
		return 2.0f * (Mathf.Pow(0.9f,(lv -1)));

	}

	public static int Power(int lv){
		return 1 * lv;
	}
}
