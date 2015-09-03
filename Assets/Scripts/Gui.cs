using UnityEngine;
using System.Collections;

public class Gui  {

	TextObj _txtMoney;
	ButtonObj _btnBuy;
	TextObj _txtCost;
	TextObj _txtWave;
	TextObj _txtTowerInfo;

	ButtonObj _btnRange;
	ButtonObj _btnFirerate;
	ButtonObj _btnPower;

	public Gui(){
		_txtMoney = MyCanvas.Find<TextObj>("TextMoney");
		_btnBuy = MyCanvas.Find<ButtonObj>("ButtonBuy");
		_txtCost = MyCanvas.Find<TextObj>("TextCost");
		_txtCost.Label = "";
		_txtWave = MyCanvas.Find<TextObj>("TextWave");
		_txtTowerInfo = MyCanvas.Find<TextObj>("TextTowerInfo");

		_btnRange = MyCanvas.Find<ButtonObj>("ButtonRange");
		_btnFirerate = MyCanvas.Find<ButtonObj>("ButtonFirerate");
		_btnPower = MyCanvas.Find<ButtonObj>("ButtonPower");

	}

	public void Update(GameMgr.eSelmode selMode,Tower tower){
		_txtMoney.SetLabelFormat("Money: ${0}",Global.Monney);

		_btnBuy.Enabled = Cost.isSatisfyMoney();
		_btnBuy.FormatLabel("Buy: ${0}",Cost.TowerProduction());

		_txtCost.Label = "";
		if(selMode == GameMgr.eSelmode.Buy){
			_txtCost.SetLabelFormat("(cost ${0})",Cost.TowerProduction().ToString());
		}

		for(int i = 0; i < Global.LIFE_MAX; i++){
			MyCanvas.SetActive("ImageLife" + i.ToString(),Global.Life > i);
		}

		_txtWave.SetLabelFormat("Wave:{0}",Global.Wave);

		if(selMode == GameMgr.eSelmode.Upgrade){
			_txtTowerInfo.SetLabelFormat(
				"<<Tower Info>>\n" +
				"Range:Lv{0}\n" +
				"Firerate:Lv{1}\n" +
				"Power:Lv{2}",
				tower.LvRange,tower.LvFirerate,tower.LvPower);
		}

		if(tower == null){
			return;
		}

		_btnRange.Enabled = Global.Monney >= tower.CostRange;
		_btnRange.FormatLabel("Range (${0})",tower.CostRange);
		
		_btnFirerate.Enabled = Global.Monney >= tower.CostFirerate;
		_btnFirerate.FormatLabel("Firerate (${0})",tower.CostFirerate);
		
		_btnPower.Enabled = Global.Monney >= tower.CostPower;
		_btnPower.FormatLabel("Power (${0})",tower.CostPower);
	}
}
