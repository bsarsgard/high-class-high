using UnityEngine;
using System.Collections;

public class NinjaSprite: DudeSprite {	
	public NinjaSprite(): base("ninja_full [www.imagesplitter.net]-0-0") {
		_walkingElements = new FAtlasElement[6];
		
		FAtlasManager am = Futile.atlasManager;
		//of course there are way smarter ways to do this, but this is fast
		//it's a ping ponging animation, which is why I did it this way, it's not a straight loop
		_walkingElements[0] = am.GetElementWithName("ninja_full [www.imagesplitter.net]-0-1");
		_walkingElements[1] = am.GetElementWithName("ninja_full [www.imagesplitter.net]-0-2");
		_walkingElements[2] = am.GetElementWithName("ninja_full [www.imagesplitter.net]-0-3");
		_walkingElements[3] = am.GetElementWithName("ninja_full [www.imagesplitter.net]-0-4");
		_walkingElements[4] = am.GetElementWithName("ninja_full [www.imagesplitter.net]-0-5");	
		_walkingElements[5] = am.GetElementWithName("ninja_full [www.imagesplitter.net]-0-6");	
	}
}