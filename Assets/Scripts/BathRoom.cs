using UnityEngine;
using System.Collections;

public class BathRoom: Room {
	public HeroSprite Hero { get; set; }
	
	public int PotPrice { get; set; }
	public int BoozePrice { get; set; }
	public int PornPrice { get; set; }
	public int TestPrice { get; set; }
	
	public BathRoom(): base("Bathroom", false, new Rect(0, 0, 0, 0)) {
		PotPrice = 10;
		BoozePrice = 5;
		PornPrice = 2;
		TestPrice = 10;
	}
	
	public void SetDialog(bool resetPrices) {
		if (resetPrices) {
			PotPrice = Mathf.Max(1, PotPrice + Random.Range(-4, 5));
			BoozePrice = Mathf.Max(1, BoozePrice + Random.Range(-3, 4));
			PornPrice = Mathf.Max(1, PornPrice + Random.Range(-2, 3));
			TestPrice = Mathf.Max(1, TestPrice + Random.Range(-4, 5));
		}
		Dialog = new DirtbagDialog("Bathroom\nPot (" + Hero.Pots + "): $" + PotPrice + "\nBooze (" + Hero.Boozes + "): $" + BoozePrice + "\nPorn(" + Hero.Porns + "): $" + PornPrice + "\nTest Answers: $" + TestPrice);
		
		ArrayList answers = new ArrayList();
		answers.Add("Pee");
		answers.Add("Buy Pot");
		answers.Add("Buy Booze");
		answers.Add("Buy Porn");
		answers.Add("Sell Pot");
		answers.Add("Sell Booze");
		answers.Add("Sell Porn");
		answers.Add("Buy Test Answers");
		Dialog.SetOptions(answers);
	}
	
	public void Action(string action) {
		//ArrayList answers;
		switch (action) {
		case "OK": 
			Dialog = null;
			break;
		case "Pee":
			Dialog = new DirtbagDialog("What a relief!");
			Dialog.SetOK();
			break;
		case "Buy Pot":
			if (Hero.Moneys >= PotPrice) {
				Hero.Moneys -= PotPrice;
				Hero.Pots++;
				Dialog = null;
			} else {
				Dialog = new DirtbagDialog("Not enough moneys!");
				Dialog.SetOK();
			}
			break;
		case "Buy Booze":
			if (Hero.Moneys >= BoozePrice) {
				Hero.Moneys -= BoozePrice;
				Hero.Boozes++;
				Dialog = null;
			} else {
				Dialog = new DirtbagDialog("Not enough moneys!");
				Dialog.SetOK();
			}
			break;
		case "Buy Porn":
			if (Hero.Moneys >= PornPrice) {
				Hero.Moneys -= PornPrice;
				Hero.Porns++;
				Dialog = null;
			} else {
				Dialog = new DirtbagDialog("Not enough moneys!");
				Dialog.SetOK();
			}
			break;
		case "Sell Pot":
			if (Hero.Pots > 0) {
				Hero.Moneys += PotPrice;
				Hero.Pots--;
				Dialog = null;
			} else {
				Dialog = new DirtbagDialog("Not enough pots!");
				Dialog.SetOK();
			}
			break;
		case "Sell Booze":
			if (Hero.Boozes > 0) {
				Hero.Moneys += BoozePrice;
				Hero.Boozes--;
				Dialog = null;
			} else {
				Dialog = new DirtbagDialog("Not enough boozes!");
				Dialog.SetOK();
			}
			break;
		case "Sell Porn":
			if (Hero.Porns > 0) {
				Hero.Moneys += PornPrice;
				Hero.Porns--;
				Dialog = null;
			} else {
				Dialog = new DirtbagDialog("Not enough porns!");
				Dialog.SetOK();
			}
			break;
		case "Buy Test Answers":
			if (Hero.Moneys >= TestPrice) {
				Hero.Moneys -= TestPrice;
				Hero.ReportCard.ScoreEnglish(true);
				Hero.ReportCard.ScoreMath(true);
				Hero.ReportCard.ScoreScience(true);
				Hero.ReportCard.ScoreHistory(true);
				Hero.ReportCard.RefreshGrades();
				Dialog = new DirtbagDialog("Grades increased!");
				Dialog.SetOK();
			} else {
				Dialog = new DirtbagDialog("Not enough moneys!");
				Dialog.SetOK();
			}
			break;
		}
	}
}
