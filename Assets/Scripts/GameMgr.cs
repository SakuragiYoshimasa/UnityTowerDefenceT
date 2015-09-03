using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {

	enum eState{
		Wait,
		Main,
		Gameover
	}

	eState _state = eState.Wait;



	int _tAppear = 0;

	List<Vec2D> _path;

	Cursor _cursor;

	Layer2D _lCollision;

	Gui _gui;

	EnemyGenerator _enemyGenerator;

	WaveStart _waveStart;

	const float TIMER_WAIT = 2.0f;
	float _tWait = TIMER_WAIT;

	// Use this for initialization
	void Start () {

		Enemy.parent = new TokenMgr<Enemy>("Enemy",128);

		Shot.parent = new TokenMgr<Shot>("Shot",128);

		Particle.parent = new TokenMgr<Particle>("Particle",256);

		Tower.parent = new TokenMgr<Tower>("Tower",64);

		GameObject prefab = null;
		prefab = Util.GetPrefab(prefab,"Field");

		Field field = Field.CreateInstance2<Field>(prefab,0,0);

		field.Load();
		_path = field.Path;
		_lCollision = field.lCollision;
		_cursor = GameObject.Find("Cursor").GetComponent<Cursor>();

		Global.Init();

		_gui = new Gui();

		_enemyGenerator = new EnemyGenerator(_path);

		_waveStart = MyCanvas.Find<WaveStart>("TextWaveStart");


	}

	void Update(){
		_gui.Update(_selMode,_selTower);
		_cursor.Proc(_lCollision);

		switch(_state){
			case eState.Wait:
				_tWait -= Time.deltaTime;
				if(_tWait <= 0){
					_enemyGenerator.Start(Global.Wave);
					_waveStart.Begin(Global.Wave);
					_state = eState.Main;
				}
				break;
			case eState.Main:
				UpdateMain();
				if(Global.Life <= 0){
					_state = eState.Gameover;
					MyCanvas.SetActive("TextGameover",true);

				}
				if(IsWaveClear()){
					Global.NextWave();
					_tWait = TIMER_WAIT;
					_state = eState.Wait;
				}

				break;
			case eState.Gameover:
				if(Input.GetMouseButtonDown(0)){
					Application.LoadLevel("main");
				}
				break;
			}
	}

	// Update is called once per frame
	void UpdateMain () {

		_enemyGenerator.Update();

		/*_tAppear++;
		if(_tAppear % 120 == 0){
			Enemy.Add(_path);
		}*/

		if( _cursor.Placable == false){
			return;
		}

		int mask = 1 << LayerMask.NameToLayer("Tower");
		Collider2D col = Physics2D.OverlapPoint(_cursor.GetPosition(),mask);

		_selObj = null;
		if(col != null){
			_selObj = col.gameObject;
		}

		if(Input.GetMouseButtonDown(0)){

			/*Vector3 posScreen = Input.mousePosition;

			Vector2 posWorld = Camera.main.ScreenToWorldPoint(posScreen);

			Tower.Add(posWorld.x,posWorld.y);*/
			switch(_selMode){
				case eSelmode.Buy:
					if(_cursor.SelObj == null){

						Global.UseMoney(Cost.TowerProduction());
						Tower.Add(_cursor.X,_cursor.Y);
						if(!Cost.isSatisfyMoney()){
							
							ChangeSelMode(eSelmode.None);
						}
					}
					break;
			}


			if(_selObj != null){
				_selTower = _selObj.GetComponent<Tower>();
				ChangeSelMode(eSelmode.Upgrade);
			}
		}


	}

	public enum eSelmode{
		None,
		Buy,
		Upgrade
	}

	eSelmode _selMode = eSelmode.None;

	GameObject _selObj = null;
	Tower _selTower = null;

	public void OnClickBuy(){ 
		//Debug.Log("Buy");
		/*_selMode = eSelmode.Buy;
		MyCanvas.SetActive("ButtonBuy",false);*/

		ChangeSelMode(eSelmode.Buy);
	}

	void ChangeSelMode(eSelmode mode){
		switch(mode){
		case eSelmode.None:
			MyCanvas.SetActive("ButtonBuy",true);
			MyCanvas.SetActive("TextTowerInfo",false);
			break;
		case eSelmode.Buy:
			MyCanvas.SetActive("ButtonBuy",false);
			MyCanvas.SetActive("TextTowerInfo",false);
			break;
		case eSelmode.Upgrade:
			MyCanvas.SetActive("ButtonBuy",true);
			MyCanvas.SetActive("TextTowerInfo",true);
		}
		_selMode = mode;
	}

	bool IsWaveClear(){
		return _enemyGenerator.Number <= 0 && Enemy.parent.Count() <= 0;
	}
}
