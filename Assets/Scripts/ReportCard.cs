using UnityEngine;
using System.Collections;

public class ReportCard {
	public int _englishTotal = 5;
	public int _englishCorrect = 5;
	public int _historyTotal = 5;
	public int _historyCorrect = 5;
	public int _mathTotal = 5;
	public int _mathCorrect = 5;
	public int _scienceTotal = 5;
	public int _scienceCorrect = 5;
	public string[] Periods = { "Math", "History", "English", "Science" };
	
	public float English { get; set; }
	public float History { get; set; }
	public float Math { get; set; }
	public float Science { get; set; }
	
	public int Day { get; set; }
	public int TotalDays { get { return 10; } }
	public int Period { get; set; }
	public int TotalPeriods { get { return 4; } }
	public string CurrentPeriod { get { return Periods[Period]; } }
	
	public ReportCard() {
		English = 1.0f;
		History = 1.0f;
		Math = 1.0f;
		Science = 1.0f;
	}
	
	public bool EndDay() {
		Day++;
		_englishTotal++;
		_historyTotal++;
		_mathTotal++;
		_scienceTotal++;
		English = (float)_englishCorrect / (float)_englishTotal;
		History = (float)_historyCorrect / (float)_historyTotal;
		Math = (float)_mathCorrect / (float)_mathTotal;
		Science = (float)_scienceCorrect / (float)_scienceTotal;
		
		if (Day >= TotalDays) {
			return true;
		} else {
			return false;
		}
	}
	
	public void Score(string subject, bool isCorrect) {
		switch (subject) {
		case "English":
			ScoreEnglish(isCorrect);
			break;
		case "History":
			ScoreHistory(isCorrect);
			break;
		case "Math":
			ScoreMath (isCorrect);
			break;
		case "Science":
			ScoreScience(isCorrect);
			break;
		}
	}
	
	public void ScoreEnglish(bool isCorrect) {
		if (isCorrect) {
			_englishCorrect++;
		}
	}
	
	public void ScoreHistory(bool isCorrect) {
		if (isCorrect) {
			_historyCorrect++;
		}
	}
	
	public void ScoreMath(bool isCorrect) {
		if (isCorrect) {
			_mathCorrect++;
		}
	}
	
	public void ScoreScience(bool isCorrect) {
		if (isCorrect) {
			_scienceCorrect++;
		}
	}
	
	public string GetAverage() {
		return GetGrade((English + History + Math + Science) / 4.0f);
	}
	
	public string GetGrade(float score) {
		if (score >= 0.9f) {
			return "A";
		} else if (score >= 0.8f) {
			return "B";
		} else if (score >= 0.7f) {
			return "C";
		} else if (score >= 0.6f) {
			return "D";
		} else {
			return "F";
		}
	}
	
	public bool NextPeriod() {
		Period++;
		if (Period >= Periods.Length) {
			Period = 0;
			return true;
		} else {
			return false;
		}
	}
}
