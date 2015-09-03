using UnityEngine;
using System.Collections;

public class Cursor : Token {

	public Sprite sprRect;
	public Sprite sprCross;

	GameObject _selObj = null;
	public GameObject SelObj{
		get{
			return _selObj;
		}
	}



	bool _bPlacable = true;

	public bool Placable{
		get{return _bPlacable;}
		set{
			if(value){
				SetSprite(sprRect);
			}else{
				SetSprite(sprCross);
			}
			_bPlacable = value;
		}
	}


	public void Proc(Layer2D lCollision){
		Vector3 posScreen = Input.mousePosition;

		Vector2 posWorld = Camera.main.ScreenToWorldPoint(posScreen);

		int i = Field.ToChipX(posWorld.x);
		int j = Field.ToChipY(posWorld.y);


		X = Field.ToWorldX(i);
		Y = Field.ToWorldY(j);

		//Debug.Log("X:" + i.ToString());
		//Debug.Log("Y:" + j.ToString());

		Placable = (lCollision.Get(i,j) == 0);
		Visible =  (lCollision.IsOutOfRange(i,j) == false);

		SetSelObj();

	}


	void SetSelObj(){

		int mask = 1 << LayerMask.NameToLayer("Tower");
		Collider2D col = Physics2D.OverlapPoint(GetPosition(),mask);
		_selObj = null;
		if(col != null){
			_selObj = col.gameObject;
		}
	}

}
