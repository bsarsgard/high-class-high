using UnityEngine;
using System.Collections;

public class Game {
	public Room background;
	public Dialog dialog;
	public HeroSprite hero;
	public FLabel gradeLabel;
	
	public Game() {
		hero = new HeroSprite();
		hero.realX = 0;
		hero.realY = 0;
		
		// Add tilemap
		Room entrance = new Room("SchoolFront_test", true, new Rect(-275, -200, 650, 165));
		Room mainHall = new Room("Hallway_test2", true, new Rect(-275, -200, 650, 165));
		Room secondHall = new Room("Hallway2_boy", true, new Rect(-275, -200, 650, 165));
		Room history = new Room("Classroom", false, new Rect(0, 0, 0, 0));
		Room math = new Room("Classroom", false, new Rect(0, 0, 0, 0));
		Room cafeteria = new Room("Cafeteria", true, new Rect(-275, -200, 650, 100));
		entrance.Doors = new Door[] {
			new Door(new Rect(100, -34.99f, 170, 10), mainHall, new Vector2(0, -190))
		};
		mainHall.Doors = new Door[] {
			new Door(new Rect(-275, -200, 650, 5), entrance, new Vector2(185, -35)),
			new Door(new Rect(-275, -34.99f, 100, 10), history, new Vector2(0, 0)),
			new Door(new Rect(175, -34.99f, 100, 10), math, new Vector2(0, 0)),
			new Door(new Rect(375, -200, 10, 175), secondHall, new Vector2(-265, -35)),
			new Door(new Rect(-275, -200, 5, 175), cafeteria, new Vector2(370, -100))
		};
		secondHall.Doors = new Door[] {
			new Door(new Rect(-275, -200, 5, 175), mainHall, new Vector2(370, -35))
		};
		history.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), mainHall, new Vector2(-255, -35))
		};
		math.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), mainHall, new Vector2(200, -35))
		};
		cafeteria.Doors = new Door[] {
			new Door(new Rect(375, -200, 10, 175), mainHall, new Vector2(-265, -100)),
		};
		background = entrance;
		
		gradeLabel = new FLabel("MorningCoffee_b", "Freshman Year");
		gradeLabel.color = Color.black;
		gradeLabel.anchorX = 0; // Anchor the label at the left edge
		gradeLabel.anchorY = 1.0f; // Anchor the label at the top edge
		gradeLabel.x = -Futile.screen.halfWidth + 25; // Move the label to the far left hand side of the screen
		gradeLabel.y = Futile.screen.halfHeight - 40; // Move the label to the bottom of the screen
		
		ResetScene();
		
		// now music
		FSoundManager.PlayMusic("Periscope");
	}
	
	public void ResetScene() {
		Futile.stage.RemoveAllChildren();
		Futile.stage.AddChild(background.Background);
		if (background.ShowHero) {
			Futile.stage.AddChild(hero);
		}
		Futile.stage.AddChild(gradeLabel);
		if (dialog != null) {
			Futile.stage.AddChild(dialog.Background);
			Futile.stage.AddChild(dialog.Title);
			foreach (FLabel label in dialog.Labels) {
				Futile.stage.AddChild(label);
			}
		}
	}
	
	public void Update (float dt) {
		float moveX = 0;
		float moveY = 0;
		
		if (dialog != null) {
			if (Input.GetKeyUp(KeyCode.Escape)) {
				dialog = null;
				ResetScene();
			} else {
				foreach (FLabel label in dialog.Labels) {
					if (
						label.GetLocalMousePosition().y < 0 && 
						label.GetLocalMousePosition().y > (0 - label.textRect.height) && 
						label.GetLocalMousePosition().x > 0 &&
						label.GetLocalMousePosition().x < (label.textRect.width)
					) {
						label.color = Color.blue;
						if (Input.GetMouseButton(0)) {
							Debug.Log(label.text);
						}
					} else if (label.color == Color.blue) {
						label.color = Color.black;
					}
				}
			}
		} else {
			if (Input.GetKeyUp(KeyCode.Escape)) {
				if (dialog == null) {
					dialog = new Dialog("test test test test test\ntest test test test test\ntest test test test test\ntest test test test test");
					ResetScene();
				}
			} else {
				if (!hero.Busy) {
					if (Input.GetKey(KeyCode.LeftControl)) {
						hero.Pose = DudeSprite.Poses.Punching;
					} else if (Input.GetKey(KeyCode.Space)) {
						hero.Pose = DudeSprite.Poses.Jumping;
					} else {
					    // Handle Input
				        if (Input.GetMouseButton(0)) {
							// 0 = left click
							Debug.Log (Input.mousePosition.ToString());
							Vector2 mousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
							
							if (mousePosition.x - hero.realX < -10) {
								moveX -= 1;
							} else if (mousePosition.x - hero.realX > 10) {
								moveX += 1;
							}
							if (mousePosition.y - hero.realY < -10) {
								moveY -= 1;
							} else if (mousePosition.y - hero.realY > 10) {
								moveY += 1;
							}
						}
					    if (Input.GetKey("up")) { moveY += 1; }
					    if (Input.GetKey("down")) { moveY -= 1; }
					    if (Input.GetKey("right")) { moveX += 1; }
					    if (Input.GetKey("left")) { moveX -= 1; }
				     
						hero.realX += moveX * dt * hero.speed;
						hero.realY += moveY * dt * hero.speed / 2f;
						
						if (moveX != 0 || moveY != 0) {
							hero.Pose = DudeSprite.Poses.Walking;
							if (moveX > 0) {
								hero.facingRight = true;
							} else if (moveX < 0) {
								hero.facingRight = false;
							}
						} else {
							hero.Pose = null;
						}
				
						// check doors
						Door door = background.GetDoor(hero.realX, hero.realY);
						if (door != null) {
							background = door.Destination;
							hero.realX = door.DropOff.x;
							hero.realY = door.DropOff.y;
							ResetScene ();
						}
						
						/*
						if (hero.realY < -200) {
							hero.realY = -200;
						} else if (hero.realY > -35) {
							hero.realY = -35;
						}
						
						if (hero.realX < -275) {
							hero.realX = -275;
						} else if (hero.realX > 375) {
							hero.realX = 375;
						}
						*/
						if (hero.realY < background.Boundaries.yMin) {
							hero.realY = background.Boundaries.yMin;
						} else if (hero.realY > background.Boundaries.yMax) {
							hero.realY = background.Boundaries.yMax;
						}
						
						if (hero.realX < background.Boundaries.xMin) {
							hero.realX = background.Boundaries.xMin;
						} else if (hero.realX > background.Boundaries.xMax) {
							hero.realX = background.Boundaries.xMax;
						}
					}
				}
						
				// set background position
				hero.x = hero.realX;
				// set hero position
				hero.y = hero.realY + hero.realZ;
			}
		}
	}
}
