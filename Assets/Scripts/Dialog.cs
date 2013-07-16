using UnityEngine;
using System.Collections;

public class Dialog {
	public FSprite Background { get; set; }
	public FLabel Title { get; set; }
	public ArrayList Labels { get; set; }
	
	public Dialog(string title) {
		Background = new FSprite("Notepad_transparent");
		Labels = new ArrayList();
		
		Title = new FLabel("MorningCoffee_b", title);
		Title.color = Color.black;
		Title.anchorX = 0; // Anchor the label at the left edge
		Title.anchorY = 1.0f; // Anchor the label at the top edge
		Title.x = (-Background.width / 2) + 35;
		Title.y = Futile.screen.halfHeight - 120;
		
		for (int iLabel = 0; iLabel <= 6; iLabel++) {
			FLabel label = new FLabel("MorningCoffee_b", "Option " + iLabel.ToString());
			label.color = Color.black;
			label.anchorX = 0; // Anchor the label at the left edge
			label.anchorY = 1.0f; // Anchor the label at the top edge
			label.x = (-Background.width / 2) + 35;
			label.y = Futile.screen.halfHeight - 120 - Title.textRect.height - ((iLabel + 1) * label.textRect.height);
			Labels.Add(label);
		}
	}
}
