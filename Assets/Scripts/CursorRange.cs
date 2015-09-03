using UnityEngine;
using System.Collections;

public class CursorRange : Token {

	public void SetVisible(bool b,int lvRange){
		float range = TowerParam.Range(lvRange);

		Scale = range / (1.5f * Field.GetChipSize()) * 5.0f;

		Visible = b;
	}

}
