using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : Token {

	public const int CHIP_PATH_START = 26;
	public const int CHIP_NONE = 0;


	List<Vec2D> _path;

	public List<Vec2D> Path{
	
		get{
			return _path;
		}
	}

	public Layer2D _lCollision;

	public Layer2D lCollision{
		get{return _lCollision;}
	}

	public static float GetChipSize(){
		var spr = GetChipSprite();
		return spr.bounds.size.x;
	}

	static Sprite GetChipSprite(){

		return Util.GetSprite("Levels/tileset","tileset_0"); 
	}

	public static float ToWorldX(int i){ //ChipData to world position
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		var spr = GetChipSprite();

		var sprW = spr.bounds.size.x;

		return min.x + (sprW * i) + sprW/2;
	}

	public static float ToWorldY(int i){
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		var spr = GetChipSprite();

		var sprH = spr.bounds.size.y;

		return max.y - (sprH * i) - sprH/2;
	}


	public static int ToChipX(float x){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		var spr = GetChipSprite();
		var sprW = spr.bounds.size.x;

		return (int)((x - min.x)/sprW);
	}
	 
	
	public static int ToChipY(float y){
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		var spr = GetChipSprite();
		var sprH = spr.bounds.size.y;
		
		return (int)((y - max.y)/ - sprH);
	}

	// Use this for initialization
	//void Start () {
	public void Load(){
		TMXLoader tmx = new TMXLoader();
		tmx.Load("Levels/map"); //load from resources
	
		Layer2D lPath = tmx.GetLayer("path"); //
		//lPath.Dump(); //Degug output

		Vec2D pos = lPath.Search(CHIP_PATH_START);  //startPositon
		//pos.Dump();

		//Enemy enemy = GameObject.Find("Enemy").GetComponent<Enemy>();

		//enemy.X = ToWorldX(pos.X); //upper scale is int
		//enemy.Y = ToWorldY(pos.Y);


		_path = new List<Vec2D>();
		_path.Add(new Vec2D(pos.X,pos.Y));
		lPath.Set(pos.X,pos.Y,CHIP_NONE);

		CreatePath(lPath,pos.X,pos.Y,_path);

		/*foreach(Vec2D p in _path){
			p.Dump();
		}*/
		//enemy.Init(_path);

		_lCollision = tmx.GetLayer("collision");

	}

	void CreatePath(Layer2D layer,int x , int y , List<Vec2D> path){
		int[] xTbl = {-1,0,1,0};
		int[] yTbl = {0,-1,0,1};

		for(var i = 0; i < xTbl.Length; i ++){

			int x2 = x + xTbl[i];
			int y2 = y + yTbl[i];
			int val = layer.Get(x2,y2);

			if(val > CHIP_NONE){
				layer.Set(x2,y2,CHIP_NONE);
				path.Add(new Vec2D(x2,y2));
				CreatePath(layer,x2,y2,path);
			}
		}
	}
}
