using UnityEngine;
using System.Collections;

public class Tower : Token {

	public static TokenMgr<Tower> parent;

	public static Tower Add(float px,float py){
		Tower t = parent.Add(px,py);

		if(t == null){
			return null;
		}
		t.Init();
		return t;

	}

	float _range;

	const float SHOT_SPEED = 5.0f;

	float _firerate;

	float _tFirerate;

	int _power;

	int _lvPower;
	public int LvPower{
		get{
			return _lvPower;
		}
	}

	int _lvRange;
	public int LvRange{
		get{
			return _lvRange;
		}
	}

	int _lvFirerate;
	public int LvFirerate{
		get{
			return _lvFirerate;
		}
	}

	// Use this for initialization
	//void Start () {
	void Init(){
		_range = Field.GetChipSize() * 1.5f;
		_firerate = 2.0f;
		_tFirerate = 0.0f;
		_power = 1;

		_lvPower = 1;
		_lvFirerate = 1;
		_lvRange = 1;

		UpdateParam();
	}
	
	// Update is called once per frame
	void Update () {

		_tFirerate += Time.deltaTime;
	
		Enemy e = Enemy.parent.Nearest(this);
		if(e == null){
			return;
		}

		float dist = Util.DistanceBetween(this,e);
		if(dist > _range){
			return;
		}


		float targetAngle = Util.AngleBetween(this,e);

		float dAngle = Mathf.DeltaAngle(Angle,targetAngle);

		Angle += dAngle * 0.2f;

		float dAngle2 = Mathf.DeltaAngle(Angle,targetAngle);

		if(Mathf.Abs(dAngle2) > 16){
			return;
		}

		//Shoot!!!!
		if(_tFirerate < _firerate){
			return;
		}
		_tFirerate = 0f;

		Shot.Add(X,Y,Angle,SHOT_SPEED,_power);
	
	}

	void UpdateParam(){
		_range = TowerParam.Range(_lvRange);
		_power = TowerParam.Power(_lvPower);
		_tFirerate = TowerParam.Firerate(_lvFirerate);

	}

}
