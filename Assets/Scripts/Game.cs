using UnityEngine;
using System.Collections;

public class Game {
	public Room background;
	public Dialog dialog;
	public HeroSprite hero;
	public FLabel timerLabel;
	public ArrayList npcs;
	public Room entrance;
	public BathRoom bathroom;
	public int? mouseDestX;
	public int? mouseDestY;
	public bool gameOver;
	
	public Game() {
		StartGame();
	}
	private void StartGame() {
		gameOver = false;
		mouseDestX = null;
		mouseDestY = null;
		Futile.stage.shouldSortByZ = true;
		// Set up rooms
		entrance = new Room("SchoolFront_test", true, new Rect(-275, -80, 650, 45));
		Room mainHall = new Room("Hallway_test2", true, new Rect(-275, -200, 650, 165));
		Room secondHall = new Room("Hallway2_boy", true, new Rect(-275, -200, 650, 165));
		ClassRoom history = new ClassRoom("History");
		ClassRoom math = new ClassRoom("Math");
		ClassRoom science = new ClassRoom("Science");
		ClassRoom english = new ClassRoom("English");
		Room cafeteria = new Room("Cafeteria", true, new Rect(-275, -200, 650, 100));
		bathroom = new BathRoom();
		Room outside = new Room("Outside", true, new Rect(-275, -200, 650, 100));
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
			new Door(new Rect(-275, -200, 5, 175), mainHall, new Vector2(370, -35)),
			new Door(new Rect(-200, -34.99f, 100, 10), science, new Vector2(0, 0)),
			new Door(new Rect(-75, -34.99f, 100, 10), english, new Vector2(0, 0)),
			new Door(new Rect(275, -34.99f, 100, 10), bathroom, new Vector2(0, 0)),
			new Door(new Rect(375, -200, 10, 175), outside, new Vector2(-265, -35))
		};
		history.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), mainHall, new Vector2(-255, -35))
		};
		math.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), mainHall, new Vector2(200, -35))
		};
		science.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), secondHall, new Vector2(-175, -35))
		};
		english.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), secondHall, new Vector2(-25, -35))
		};
		bathroom.Doors = new Door[] {
			new Door(new Rect(0, -1, 1, 1), secondHall, new Vector2(350, -35))
		};
		cafeteria.Doors = new Door[] {
			new Door(new Rect(375, -200, 10, 175), mainHall, new Vector2(-265, -100)),
		};
		outside.Doors = new Door[] {
			new Door(new Rect(-275, -200, 5, 175), secondHall, new Vector2(370, -35))
		};
		background = entrance;
		
		// set up NPCs
		npcs = new ArrayList();
		npcs.Add(new TeacherSprite(mainHall, 110, -110));
		npcs.Add(new GirlSprite(mainHall, 90, -90));
		hero = new HeroSprite(entrance, 0, 0);
		hero.ReportCard = new ReportCard();
		hero.Moneys = 10;
		
		npcs.Add(hero);
		
		bathroom.Hero = hero; 
		bathroom.SetDialog(true);
		
		dialog = new DittoDialog("Welcome to High Class High! This is a fast-track high school,\n" +
								 "so you only have " + hero.ReportCard.TotalDays + " days to get a straight-A report card,\n" +
								 "or your parents are sending you to military school.\n" +
								 "You can see the current day, period, and what class you\n" +
								 "should be in at the top of the screen. The bathroom can be\n" +
								 "found in the East hall, but you'll miss a period to use it.\n\n" +
								 "Move with arrow or WASD keys, ESC or Enter to dismiss\n" +
								 "dialogs, and select dialog options with the mouse or number\n" +
								 "keys in multiple choice screens.");
		dialog.SetOK();
		timerLabel = new FLabel("MorningCoffee_b", "Time:\n" + ((int)hero.ReportCard.CurrentTimer).ToString());
		timerLabel.color = Color.black;
		timerLabel.anchorX = 0;
		timerLabel.anchorY = 1.0f;
		timerLabel.x = -Futile.screen.halfWidth + 5;
		timerLabel.y = Futile.screen.halfHeight - 10;
		
		ResetScene();
		
		// now music
		Game.PlayRandomMusic();
	}
	
	public static void PlayRandomMusic() {
		int track = Mathf.RoundToInt(Random.Range(0, 6));
		if (track == 0) {
			FSoundManager.PlayMusic("GreenDaze");
		} else if (track == 1) {
			FSoundManager.PlayMusic("ModernRockBoy");
		} else if (track == 2) {
			FSoundManager.PlayMusic("Periscope");
		} else if (track == 3) {
			FSoundManager.PlayMusic("SmellsLikeGrunge");
		} else if (track == 4) {
			FSoundManager.PlayMusic("WhatsItToYaPunk");
		} else if (track == 5) {
			FSoundManager.PlayMusic("Pentagram");
		}
	}
	
	public void GameOver() {
		gameOver = true;
		Futile.stage.RemoveAllChildren();
		FSprite background = new FSprite("Cover_blank");
		background.scale = 1f;
		background.x = 0;
		background.y = 0;
		Futile.stage.AddChild(background);
		FLabel gradeLabel = new FLabel("MorningCoffee_b", "Final Grade: " + hero.ReportCard.GetAverage() + ", click to play again");
		gradeLabel.color = Color.black;
		gradeLabel.anchorX = 0; // Anchor the label at the left edge
		gradeLabel.anchorY = 1.0f; // Anchor the label at the top edge
		gradeLabel.x = -Futile.screen.halfWidth + 290; // Move the label to the far left hand side of the screen
		gradeLabel.y = Futile.screen.halfHeight - 180; // Move the label to the bottom of the screen
		Futile.stage.AddChild(gradeLabel);
		FLabel creditsLabel = new FLabel("MorningCoffee_b", "SlightlyMadman: Programming\nArtemisia: Art/Animation\nmike fictitious: Producer");
		creditsLabel.color = Color.black;
		creditsLabel.anchorX = 0; // Anchor the label at the left edge
		creditsLabel.anchorY = 1.0f; // Anchor the label at the top edge
		creditsLabel.x = -Futile.screen.halfWidth + 300; // Move the label to the far left hand side of the screen
		creditsLabel.y = Futile.screen.halfHeight - 315; // Move the label to the bottom of the screen
		Futile.stage.AddChild(creditsLabel);
	}
	
	public void ResetScene() {
		if (!gameOver) {
			// clear it
			Futile.stage.RemoveAllChildren();
			// draw background
			Futile.stage.AddChild(background.Background);
			// draw hero
			if (background.ShowHero) {
				//Futile.stage.AddChild(hero);
				// draw NPCs
				foreach (DudeSprite npc in npcs) {
					if (npc == hero || npc.Room == background) {
						Futile.stage.AddChild(npc);
					}
				}
			}
			// draw text
			Futile.stage.AddChild(timerLabel);
			FLabel gradeLabel = new FLabel("MorningCoffee_b", "Day " + (hero.ReportCard.Day + 1) + " of " + hero.ReportCard.TotalDays + ", Period " + (hero.ReportCard.Period + 1) + " of " + hero.ReportCard.TotalPeriods + " (" + hero.ReportCard.CurrentPeriod + ")");
			gradeLabel.color = Color.black;
			gradeLabel.anchorX = 0; // Anchor the label at the left edge
			gradeLabel.anchorY = 1.0f; // Anchor the label at the top edge
			gradeLabel.x = -Futile.screen.halfWidth + 125; // Move the label to the far left hand side of the screen
			gradeLabel.y = Futile.screen.halfHeight - 30; // Move the label to the bottom of the screen
			Futile.stage.AddChild(gradeLabel);
			FLabel englishLabel = new FLabel("MorningCoffee_b", "Eng: " + hero.ReportCard.GetGrade(hero.ReportCard.English));
			englishLabel.color = hero.ReportCard.English >= 0.8 ? Color.blue : Color.black;
			englishLabel.anchorX = 0;
			englishLabel.anchorY = 1.0f;
			englishLabel.x = -Futile.screen.halfWidth + 5;
			englishLabel.y = Futile.screen.halfHeight - 90;
			Futile.stage.AddChild(englishLabel);
			FLabel historyLabel = new FLabel("MorningCoffee_b", "His: " + hero.ReportCard.GetGrade(hero.ReportCard.History));
			historyLabel.color = hero.ReportCard.History >= 0.8 ? Color.blue : Color.black;
			historyLabel.anchorX = 0;
			historyLabel.anchorY = 1.0f;
			historyLabel.x = -Futile.screen.halfWidth + 5;
			historyLabel.y = Futile.screen.halfHeight - 130;
			Futile.stage.AddChild(historyLabel);
			FLabel mathLabel = new FLabel("MorningCoffee_b", "Mat: " + hero.ReportCard.GetGrade(hero.ReportCard.Math));
			mathLabel.color = hero.ReportCard.Math >= 0.8 ? Color.blue : Color.black;
			mathLabel.anchorX = 0;
			mathLabel.anchorY = 1.0f;
			mathLabel.x = -Futile.screen.halfWidth + 5;
			mathLabel.y = Futile.screen.halfHeight - 170;
			Futile.stage.AddChild(mathLabel);
			FLabel scienceLabel = new FLabel("MorningCoffee_b", "Sci: " + hero.ReportCard.GetGrade(hero.ReportCard.Science));
			scienceLabel.color = hero.ReportCard.Science >= 0.8 ? Color.blue : Color.black;
			scienceLabel.anchorX = 0;
			scienceLabel.anchorY = 1.0f;
			scienceLabel.x = -Futile.screen.halfWidth + 5;
			scienceLabel.y = Futile.screen.halfHeight - 210;
			Futile.stage.AddChild(scienceLabel);
			FLabel moneysLabel = new FLabel("MorningCoffee_b", "$" + hero.Moneys);
			moneysLabel.color = Color.black;
			moneysLabel.anchorX = 0;
			moneysLabel.anchorY = 1.0f;
			moneysLabel.x = -Futile.screen.halfWidth + 5;
			moneysLabel.y = Futile.screen.halfHeight - 290;
			Futile.stage.AddChild(moneysLabel);
			// draw dialog
			if (dialog != null) {
				dialog.Background.alpha = 0;
				Futile.stage.AddChild(dialog.Background);
				Futile.stage.AddChild(dialog.Title);
				foreach (FLabel label in dialog.Labels) {
					Futile.stage.AddChild(label);
				}
			}
		}
	}
	
	public bool SetNextPeriod() {
		FSoundManager.PlaySound("Bell");
		hero.ReportCard.CurrentTimer = hero.ReportCard.HallTime;
		bathroom.SetDialog(true);
		if (hero.Room is ClassRoom) {
			((ClassRoom)hero.Room).SetQuestion();
		}
		if (hero.ReportCard.NextPeriod()) {
			Game.PlayRandomMusic();
			// it's another day
			if (hero.ReportCard.EndDay()) {
				// end the game
				GameOver();
				return false;
			} else {
				hero.Pose = null;
				hero.realX = 0;
				hero.realY = 0;
				hero.x = 0;
				hero.y = 0;
				hero.Room = entrance;
				background = entrance;
				return true;
			}
		} else {
			ResetScene();
			return false;
		}
	}
	
	public void Update (float dt) {
		if (gameOver) {
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter)) {
				StartGame();
			}
		} else {
			foreach (DudeSprite ds in npcs) {
				ds.sortZ = 1000 - ds.y;
				if (ds is NpcSprite) {
					if (ds.destX == null || ds.destY == null) {
						// pick a destination
						int d = -1;
						while (d == -1) {
							d = Random.Range(0, ds.Room.Doors.Length);
							if (!ds.Room.Doors[d].Destination.ShowHero) {
								d = -1;
							}
						}
						ds.destX = (int)ds.Room.Doors[d].Portal.center.x;
						ds.destY = (int)ds.Room.Doors[d].Portal.center.y;
						ds.wait = 10;
					} else {
						if (ds.wait <= 0 || ds.Room == background) {
							float mx = 0;
							float my = 0;
							if (ds.destX < ds.x - 1) {
								mx -= 1;
							} else if (ds.destX > ds.x + 1) {
								mx += 1;
							}
							if (ds.destY < ds.y - 1) {
								my -= 1;
							} else if (ds.destY > ds.y + 1) {
								my += 1;
							}
							ds.ProcessMoves(mx, my, dt);
							if (ds.Room == background) {
								Futile.stage.AddChild(ds);
							} else {
								Futile.stage.RemoveChild(ds);
							}
						} else {
							ds.wait -= dt;
						}
					}
				}
			}
			hero.ReportCard.CurrentTimer -= dt;
			timerLabel.text = "Time:\n" + ((int)hero.ReportCard.CurrentTimer).ToString();
			if (hero.ReportCard.CurrentTimer <= 0) {
				if (hero.Room is ClassRoom) {
					hero.ReportCard.Score(hero.ReportCard.CurrentPeriod, false);
					if (SetNextPeriod()) {
						dialog = new NoteDialog("Out of time!\n\n" +
							"The school day is over.\n" +
							"After a night at home,\n" +
							"the next day begins.\n\n" +
							"Average Grade: " + hero.ReportCard.GetAverage());
						dialog.SetOK();
					} else {
						dialog = new NoteDialog("Out of time!");
						dialog.SetOK();
					}
				} else if (hero.Room is BathRoom) {
					if (SetNextPeriod()) {
						dialog = new NoteDialog("A teacher came in!\nGotta run!\n\n" +
							"The school day is over.\n" +
							"After a night at home,\n" +
							"the next day begins.\n\n" +
							"Average Grade: " + hero.ReportCard.GetAverage());
						dialog.SetOK();
					} else {
						dialog = new NoteDialog("A teacher came in!\nGotta run!");
						dialog.SetOK();
					}
				} else {
					if (SetNextPeriod()) {
						dialog = new NoteDialog(
							"The school day is over.\n" +
							"After a night at home,\n" +
							"the next day begins.\n\n" +
							"Average Grade: " + hero.ReportCard.GetAverage());
						dialog.SetOK();
					}
				}
				ResetScene();
			}
			
			float moveX = 0;
			float moveY = 0;
			
			if (dialog != null) {
				if (dialog.Background.alpha < 1.0f) {
					dialog.Background.alpha += dt * 2f;
				}
				if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return)) {
					// hide it
					if (!background.ShowHero) {
						if (hero.Room is BathRoom) {
							bathroom.SetDialog(false);
						}
						if (!(dialog is TriviaDialog)) {
							// exit the room
							hero.Room = background.Doors[0].Destination;
							hero.realX = background.Doors[0].DropOff.x;
							hero.x = hero.realX;
							hero.realY = background.Doors[0].DropOff.y;
							hero.y = hero.realY;
							background = background.Doors[0].Destination;
							dialog = null;
						}
					} else {
						dialog = null;
					}
					ResetScene();
				} else {
					// check option clicks
					foreach (FLabel label in dialog.Labels) {
						bool keyChoice = false;
						switch (label.text.Substring(0, 2)) {
						case "1)":
							keyChoice = Input.GetKeyUp("1") || Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1);
							break;
						case "2)":
							keyChoice = Input.GetKeyUp("2") || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2);
							break;
						case "3)":
							keyChoice = Input.GetKeyUp("3") || Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3);
							break;
						case "4)":
							keyChoice = Input.GetKeyUp("4") || Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4);
							break;
						case "5)":
							keyChoice = Input.GetKeyUp("5") || Input.GetKeyUp(KeyCode.Alpha5) || Input.GetKeyUp(KeyCode.Keypad5);
							break;
						case "6)":
							keyChoice = Input.GetKeyUp("6") || Input.GetKeyUp(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.Keypad6);
							break;
						case "7)":
							keyChoice = Input.GetKeyUp("7") || Input.GetKeyUp(KeyCode.Alpha7) || Input.GetKeyUp(KeyCode.Keypad7);
							break;
						case "8)":
							keyChoice = Input.GetKeyUp("8") || Input.GetKeyUp(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.Keypad8);
							break;
						case "9)":
							keyChoice = Input.GetKeyUp("9") || Input.GetKeyUp(KeyCode.Alpha9) || Input.GetKeyUp(KeyCode.Keypad9);
							break;
						default:
							keyChoice = false;
							break;
						}
						if (
							keyChoice ||
							(
								label != null &&
								label.GetLocalMousePosition().y < 0 && 
								label.GetLocalMousePosition().y > (0 - label.textRect.height) && 
								label.GetLocalMousePosition().x > 0 &&
								label.GetLocalMousePosition().x < (label.textRect.width)
							)
						) {
							// mouse over
							label.color = dialog.HilightColor;
							if (Input.GetMouseButtonUp(0) || keyChoice) {
								// click
								if (dialog is TriviaDialog) {
									if (label == dialog.Labels[((TriviaDialog)dialog).Answer]) {
										hero.ReportCard.Score(hero.ReportCard.CurrentPeriod, true);
										if (SetNextPeriod()) {
											dialog = new NoteDialog("Correct!\n\n" +
												"The school day is over.\n" +
												"After a night at home,\n" +
												"the next day begins.\n\n" +
												"Average Grade: " + hero.ReportCard.GetAverage());
											dialog.SetOK();
										} else {
											dialog = new NoteDialog("Correct!");
											dialog.SetOK();
										}
									} else {
										hero.ReportCard.Score(hero.ReportCard.CurrentPeriod, false);
										if (SetNextPeriod()) {
											dialog = new NoteDialog("Wrong!\n\n" +
												"The school day is over.\n" +
												"After a night at home,\n" +
												"the next day begins.\n\n" +
												"Average Grade: " + hero.ReportCard.GetAverage());
											dialog.SetOK();
										} else {
											dialog = new NoteDialog("Wrong!");
											dialog.SetOK();
										}
									}
									ResetScene();
									break;
								} else if (dialog is DirtbagDialog) {
									((BathRoom)hero.Room).Action(label.text.Length < 3 ? label.text : label.text.Substring(3));
									dialog = hero.Room.Dialog;
									if (dialog == null) {
										if (!background.ShowHero) {
											hero.Room = background.Doors[0].Destination;
											hero.realX = background.Doors[0].DropOff.x;
											hero.x = hero.realX;
											hero.realY = background.Doors[0].DropOff.y;
											hero.y = hero.realY;
											background = background.Doors[0].Destination;
										}
										/*
										if (SetNextPeriod()) {
											dialog = new NoteDialog("Another day wasted!\n\n" +
												"The school day is over.\n" +
												"After a night at home,\n" +
												"the next day begins.\n\n" +
												"Average Grade: " + hero.ReportCard.GetAverage());
											dialog.SetOK();
										}
										*/
										bathroom.SetDialog(false);
									}
									ResetScene();
									break;
								} else if (label.text == "OK") {
									dialog = null;
									if (!background.ShowHero) {
										hero.Room = background.Doors[0].Destination;
										hero.realX = background.Doors[0].DropOff.x;
										hero.x = hero.realX;
										hero.realY = background.Doors[0].DropOff.y;
										hero.y = hero.realY;
										background = background.Doors[0].Destination;
									}
									ResetScene();
									break;
								}
							}
						} else if (label != null && dialog != null && label.color == dialog.HilightColor) {
							// remove mouse over
							label.color = dialog.TextColor;
						}
					}
				}
			} else {
				if (Input.GetKeyUp(KeyCode.Escape)) {
					if (background.Dialog != null) {
						dialog = background.Dialog;
						ResetScene();
					}
				} else {
				    // Handle Input
			        if (Input.GetMouseButtonUp(0)) {
						mouseDestX = (int)Input.mousePosition.x - Screen.width / 2;
						mouseDestY = (int)Input.mousePosition.y - Screen.height / 2;
					}
					if (mouseDestX != null && mouseDestY != null) {
						// 0 = left click
						if (mouseDestX - (int)hero.realX < -10) {
							moveX -= 1;
						} else if (mouseDestX - (int)hero.realX > 10) {
							moveX += 1;
						}
						if (mouseDestY - (int)hero.realY < -10) {
							moveY -= 1;
						} else if (mouseDestY - (int)hero.realY > 10) {
							moveY += 1;
						}
						if (moveX == 0 && moveY == 0) {
							mouseDestX = null;
							mouseDestY = null;
						}
					}
				    if (Input.GetKey("up") || Input.GetKey(KeyCode.W)) { moveY += 1; mouseDestX = null; mouseDestY = null; }
				    if (Input.GetKey("down") || Input.GetKey(KeyCode.S)) { moveY -= 1; mouseDestX = null; mouseDestY = null; }
				    if (Input.GetKey("right") || Input.GetKey(KeyCode.D)) { moveX += 1; mouseDestX = null; mouseDestY = null; }
				    if (Input.GetKey("left") || Input.GetKey(KeyCode.A)) { moveX -= 1; mouseDestX = null; mouseDestY = null; }
			     
					hero.ProcessMoves(moveX, moveY, dt);
					if (hero.Room != background) {
						mouseDestX = null;
						mouseDestY = null;
						
						background = hero.Room;
						if (hero.Room is ClassRoom) {
							FSoundManager.PlaySound("DoorClose");
							if (((ClassRoom)hero.Room).Subject == hero.ReportCard.CurrentPeriod) {
								hero.ReportCard.CurrentTimer = hero.ReportCard.ClassTime;
							} else {
								// wrong class, kick him out
								background = hero.Room;
								hero.Room = background.Doors[0].Destination;
								hero.realX = background.Doors[0].DropOff.x;
								hero.x = hero.realX;
								hero.realY = background.Doors[0].DropOff.y;
								hero.y = hero.realY;
								background = background.Doors[0].Destination;
							}
						} else if (hero.Room is BathRoom) {
							FSoundManager.PlaySound("DoorClose");
						}
						if (!background.ShowHero && background.Dialog != null) {
							dialog = background.Dialog;
						}
						ResetScene();
					}
				}
			}
		}
	}
}
