using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator  {

	List<Vec2D> _pathList;

	float _interval;
	float _tInterval;

	int _number;
	public int Number{
		get{return _number;}
	}

	public EnemyGenerator(List<Vec2D> path){
		_pathList = path;

	}

	public void Start(int nWave){
		_interval = EnemyParam.GenerationInterval();
		_tInterval = 0f;
		_number = EnemyParam.GenerationNumber();
	}

	public void Update(){
		if(_number <= 0){
			return;
		}

		_tInterval += Time.deltaTime;

		if(_tInterval > _interval){
			_tInterval -= _interval;

			Enemy.Add(_pathList);
			_number--;
		}

	}


}
