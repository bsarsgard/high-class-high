using UnityEngine;
using System.Collections;

public class TeacherSprite: NpcSprite {
	public TeacherSprite(Room room, float x, float y): base("Teacher_standingbigger", room, x, y) {
		//scale = 1.25f;
		_walkingElements = new FAtlasElement[4];
		_punchingElements = new FAtlasElement[2];
		_jumpingElements = new FAtlasElement[4];
		
		FAtlasManager am = Futile.atlasManager;
		//of course there are way smarter ways to do this, but this is fast
		//it's a ping ponging animation, which is why I did it this way, it's not a straight loop
		_walkingElements[0] = am.GetElementWithName("Teacher1bigger");
		_walkingElements[1] = am.GetElementWithName("Teacher2bigger");
		_walkingElements[2] = am.GetElementWithName("Teacher3bigger");
		_walkingElements[3] = am.GetElementWithName("Teacher4bigger");
		
		_punchingElements[0] = am.GetElementWithName("Teacher1bigger");
		_punchingElements[1] = am.GetElementWithName("Teacher2bigger");
		
		_jumpingElements[0] = am.GetElementWithName("Teacher1bigger");
		_jumpingElements[1] = am.GetElementWithName("Teacher2bigger");
		_jumpingElements[2] = am.GetElementWithName("Teacher3bigger");
		_jumpingElements[2] = am.GetElementWithName("Teacher4bigger");
	}
}
