using UnityEngine;
using System.Collections;

public class HighClassHigh : MonoBehaviour {
	public Game game = null;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(800, 600, false);
		FutileParams fparams = new FutileParams(true, true, true, true);
		fparams.AddResolutionLevel(800f, 1.0f, 1.0f, ""); //iPhone
		fparams.origin = new Vector2(0.5f,0.5f);
		Futile.instance.Init(fparams);
			
	    Futile.atlasManager.LoadAtlas("Atlases/HighClassHigh");
	    Futile.atlasManager.LoadAtlas("Atlases/Entrance");
	    Futile.atlasManager.LoadAtlas("Atlases/Hallway");
	    Futile.atlasManager.LoadAtlas("Atlases/Classroom");
	    Futile.atlasManager.LoadAtlas("Atlases/Cafeteria");
	    Futile.atlasManager.LoadAtlas("Atlases/Bathroom");
	    Futile.atlasManager.LoadAtlas("Atlases/Outside");
	    Futile.atlasManager.LoadAtlas("Atlases/Titles");
		Futile.atlasManager.LoadFont("PressStart2P", "PressStart2P_0", "Atlases/PressStart2P", 0, 0);
		Futile.atlasManager.LoadFont("MorningCoffee", "MorningCoffee_0", "Atlases/MorningCoffee", 0, 0);
		Futile.atlasManager.LoadFont("MorningCoffee_b", "MorningCoffee_b_0", "Atlases/MorningCoffee_b", 0, 0);
		Futile.atlasManager.LoadFont("OldNewspaperTypes", "OldNewspaperTypes_0", "Atlases/OldNewspaperTypes", 0, 0);
		Futile.atlasManager.LoadFont("Skinny", "Skinny_0", "Atlases/Skinny", 0, 0);
		//game = new Game();
		
		// title screen
		FSoundManager.PlayMusic("Periscope");
		Futile.stage.RemoveAllChildren();
		FSprite background = new FSprite("TitleScreen");
		background.scale = 1f;
		background.x = 0;
		background.y = 0;
		Futile.stage.AddChild(background);
		/*
		FLabel gradeLabel = new FLabel("MorningCoffee_b", "Final Grade: " + hero.ReportCard.GetAverage());
		gradeLabel.color = Color.black;
		gradeLabel.anchorX = 0; // Anchor the label at the left edge
		gradeLabel.anchorY = 1.0f; // Anchor the label at the top edge
		gradeLabel.x = -Futile.screen.halfWidth + 325; // Move the label to the far left hand side of the screen
		gradeLabel.y = Futile.screen.halfHeight - 325; // Move the label to the bottom of the screen
		Futile.stage.AddChild(gradeLabel);
		*/
	}
	
	// Update is called once per frame
	void Update () {
		if (game == null) {
			if (Input.GetMouseButton(0) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter)) {
				game = new Game();
			}
		} else {
			game.Update(Time.deltaTime);
		}
	}
}
