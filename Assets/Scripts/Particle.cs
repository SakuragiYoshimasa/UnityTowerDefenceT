using UnityEngine;
using System.Collections;

public class Particle : Token {

	public Sprite spr0;
	public Sprite spr1;

	public enum eType{
		Ball,
		Ring,
		Ellipse
	}

	public static TokenMgr<Particle> parent;

	public static Particle Add(eType type,int timer,float px,float py,float direction,float speed){
		Particle p = parent.Add(px,py,direction,speed);
		if(p == null){
			return null;
		}

		p.Init(type,timer);

		return p;
	}

	eType _type;
	int _tDestroy;

	const float SCALE_MAX = 4.0f;
	float _tScale;

	void Init(eType type,int timer){

		switch(type){
			case eType.Ball:
				SetSprite(spr0);
				break;
			case eType.Ring:
			case eType.Ellipse:
				SetSprite(spr1);
				_tScale = SCALE_MAX;
				break;

		}

		_type = type;
		_tDestroy = timer;
		Scale = 1.0f;
		Alpha = 1.0f;
	}


	// Update is called once per frame
	void Update () {
	
		switch(_type){
			case eType.Ball:
				MulVelocity(0.9f);
				MulScale(0.93f);
				break;
			case eType.Ring:
				_tScale *= 0.9f;
				Scale = (SCALE_MAX -_tScale);
				Alpha -= 0.05f;
				break;
			case eType.Ellipse:
				_tScale *= 0.9f;
				ScaleX  = (SCALE_MAX - _tScale) * 2;
				ScaleY  = (SCALE_MAX - _tScale);
				Alpha -= 0.05f;
				break;
		}

		_tDestroy --;
		if(_tDestroy <= 0){
			Vanish();
		}

	}
}
