using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Token {

	public Sprite spr0;
	public Sprite spr1;

	int _tAnim = 0;

	float _speed = 0f;
	float _tSpeed = 0f;

	List<Vec2D> _path;
	int _pathIdx;

	Vec2D _prev;
	Vec2D _next;

	int _hp;

	int _money;

	public static TokenMgr<Enemy> parent = null;

	public static Enemy Add(List<Vec2D> path){
		Enemy e = parent.Add(0,0);
		if(e == null){
			return null;
		}
		e.Init(path);
		return e;
	}


	public void Init(List<Vec2D> path){
		_path = path;
		_pathIdx = 0;
		_speed = EnemyParam.Speed();
		_tSpeed = 0;

		MoveNext();
		_prev.Copy(_next);
		_prev.x -= Field.GetChipSize();
		_hp = EnemyParam.Hp();
		_money = EnemyParam.Money();
		FixedUpdate();
	}

	void FixedUpdate(){

		//Animation
		_tAnim ++;
		if(_tAnim % 32 > 16){
			SetSprite(spr0);  //Renderer.sprite = sprite
		}else{
			SetSprite(spr1);
		}


		//movement
		_tSpeed += _speed;
		if(_tSpeed > 100.0f){
			_tSpeed -= 100.0f;
			MoveNext();
		}

		X = Mathf.Lerp(_prev.x,_next.x, _tSpeed /100.0f); //this _tSpeed is not used to speed but position
		Y = Mathf.Lerp(_prev.y,_next.y, _tSpeed /100.0f);

		UpdateAngle();

	}

	void MoveNext(){
		if(_pathIdx >= _path.Count){
			_tSpeed = 100.0f;

			Global.Damege();
			Vanish();

			return;
		}

		_prev.Copy(_next);

		Vec2D v = _path[_pathIdx];
		_next.x = Field.ToWorldX(v.X);
		_next.y = Field.ToWorldY(v.Y);

		_pathIdx++;
	}

	void UpdateAngle(){

		float dx = _next.x - _prev.x;
		float dy = _next.y - _prev.y;

		Angle = Mathf.Atan2(dy, dx)* Mathf.Rad2Deg;
	}

	void OnTriggerEnter2D(Collider2D other){
		string name = LayerMask.LayerToName(other.gameObject.layer);

		if(name == "Shot"){

			Shot s = other.gameObject.GetComponent<Shot>();
			s.Vanish();

			Damage(s.Power);

			if(Exists == false){
				Global.AddMoney(_money);
			}
		}
	}

	void Damage(int val){
		_hp -= val;
		if(_hp <= 0){
			Vanish();
		}
	}

	public override void Vanish(){

		{
			Particle p = Particle.Add(Particle.eType.Ring, 30 ,X,Y,0,0);
			if(p){
				p.SetColor(0.7f,1f,0.7f);
			}
		}

		float dir = Random.Range(35,55);
		for(int i = 0; i < 8; i++){

			int timer = Random.Range(20,40);
			float spd = Random.Range(0.5f,2.5f);

			Particle p = Particle.Add(Particle.eType.Ball, timer ,X ,Y ,dir,spd);
			dir += Random.Range(35,55);
			if(p){
				p.SetColor(0,1f,0);
				p.Scale = 0.8f;
			}
		}

		base.Vanish();

	}

}
