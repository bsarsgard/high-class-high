using UnityEngine;
using System.Collections;

public class TriviaDialog: Dialog {
	public int Answer { get; set; }
	
	public TriviaDialog(string title): base("PaperTexture2", "OldNewspaperTypes") {
		TextColor = new Color(93f/255f, 8f/255f, 230f/255f);
		HilightColor = new Color(168f/255f, 83f/255f, 255f/255f);
		SetTitle(title);
	}
}

public class DirtbagDialog: NoteDialog {
	public DirtbagDialog(string title): base(title) {
	}
}

public class NoteDialog: Dialog {
	public NoteDialog(string title): base("Notepad_transparent", "MorningCoffee_b") {
		TextColor = Color.black;
		HilightColor = Color.blue;
		SetTitle(title);
	}
}

public class Dialog {
	public FSprite Background { get; set; }
	public FLabel Title { get; set; }
	public ArrayList Labels { get; set; }
	public Color TextColor { get; set; }
	public Color HilightColor { get; set; }
	public string TextFont { get; set; }
	
	public Dialog(string background, string font) {
		Background = new FSprite(background);
		TextFont = font;
		Labels = new ArrayList();
	}
	
	public void SetTitle(string title) {
		Title = new FLabel(TextFont, title);
		Title.color = TextColor;
		Title.anchorX = 0; // Anchor the label at the left edge
		Title.anchorY = 1.0f; // Anchor the label at the top edge
		Title.x = (-Background.width / 2) + 25;
		Title.y = Futile.screen.halfHeight - 120;
	}
	
	public void SetOptions(ArrayList options) {
		for (int iLabel = 0; iLabel < options.Count; iLabel++) {
			FLabel label = new FLabel(TextFont, options[iLabel].ToString());
			label.color = TextColor;
			label.anchorX = 0; // Anchor the label at the left edge
			label.anchorY = 1.0f; // Anchor the label at the top edge
			label.x = (-Background.width / 2) + 25;
			label.y = Futile.screen.halfHeight - 120 - Title.textRect.height - ((iLabel + 1) * label.textRect.height);
			Labels.Add(label);
		}
	}
	
	public void SetOK() {
		FLabel label = new FLabel(TextFont, "OK");
		label.color = TextColor;
		label.anchorX = 0; // Anchor the label at the left edge
		label.anchorY = 1.0f; // Anchor the label at the top edge
		label.x = (-Background.width / 2) + 25;
		label.y = Futile.screen.halfHeight - 120 - Title.textRect.height - (1 * label.textRect.height);
		Labels.Add(label);
	}
}
