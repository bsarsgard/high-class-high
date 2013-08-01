using UnityEngine;
using System.Collections;

public class ClassRoom: Room {
	public string Subject;
	
	public ClassRoom(string subject): base("Classroom", false, new Rect(0, 0, 0, 0)) {
		Subject = subject;
		SetQuestion();
	}
	
	public void SetQuestion() {
		TextAsset csvFile = Resources.Load(Subject) as TextAsset;
		string[] lines = csvFile.text.Replace("\r", "").Split("\n"[0]);
		int q = Mathf.RoundToInt(Random.Range(0, lines.Length));
		string[] question = lines[q].Split("\t"[0]);
		Dialog = new TriviaDialog(question[0].Replace("`", "\n"));
		
		ArrayList answers = new ArrayList();
		for (int iQuestion = 1; iQuestion < question.Length; iQuestion++) {
			if (question[iQuestion].ToString().Substring(0, 1) == "*") {
				((TriviaDialog)Dialog).Answer = iQuestion - 1;
				answers.Add(question[iQuestion].ToString().Substring(1));
			} else {
				answers.Add(question[iQuestion].ToString());
			}
		}
		Dialog.SetOptions(answers);
	}
}
