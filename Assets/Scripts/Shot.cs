using UnityEngine;
using System.Collections;

public class Shot : Token {

	public static TokenMgr<Shot> parent;

	public static Shot Add(float px,float py,float direction,float speed,int power){
		Shot s = parent.Add(px,py,direction,speed);
		if(s == null){
			return null;
		}
		s.Init(power);
		return s;
	}

	int _power;

	public int Power{
		get{return _power;}
	}

	public void Init(int power){
		_power = power;
	}

	// Update is called once per frame
	void Update () {
	
		if(IsOutside()){
			{
				Vanish();
			}
		}
	}

	public override void Vanish(){
		for(int i = 0; i < 4; i ++){
			int timer = Random.Range(20,40);
			float dir = Direction - 180  + Random.Range(-60,60);
			float spd = Random.Range(1.0f,1.5f);

			Particle p = Particle.Add(Particle.eType.Ball,timer,X,Y,dir,spd);

			if(p){
				p.SetColor(1f,0f,0f);
				p.Scale = 0.6f;
			}
		}

		base.Vanish();
	}
}
