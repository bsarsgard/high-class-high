using UnityEngine;
using System.Collections;

public class DudeSprite: FSprite {
	public enum Poses {
		Walking,
		Punching,
		Jumping,
	};
	
	protected static float JUMP_SPEED = 12f;
	protected static float JUMP_DECAY = 1.2f;
	
	protected FAtlasElement _standingElement;
	protected FAtlasElement[] _walkingElements;
	protected FAtlasElement[] _punchingElements;
	protected FAtlasElement[] _jumpingElements;
	
	protected float _frameCount = 0;
	protected float _animationSpeed = 0.15f;
	protected float _jumpAnimationSpeed = 0.05f;
	protected float _jumpFrameCount = 0;
	protected int _frameIndex = 0;
	protected float _jumpXVelocity = 0;
	protected float _jumpZVelocity = 0;
	
	public bool facingRight = true;
	public float speed = 200.0f;
	public float realX = 0;
	public float realY = 0;
	public float realZ = 0;
	public float hp = 100;
	
	public Vector2 Destination { get; set; }
	
	protected Poses? _pose = null;
	public Poses? Pose {
		get {
			return _pose;
		}
		set {
			if (_pose != value) {
				if (value == Poses.Jumping) {
					_jumpZVelocity = JUMP_SPEED;
					if (facingRight) {
						_jumpXVelocity = 2f;
					} else {
						_jumpXVelocity = -2f;
					}
				}
				_frameCount = _animationSpeed;
				_frameIndex = 0;
				_pose = value;
			}
		}
	}
	public bool Busy {
		get {
			return Pose == Poses.Punching || Pose == Poses.Jumping;
		}
	}
	public Room Room { get; set; }
	
	public DudeSprite(string elementName, Room room, float x, float y): base(elementName) {
		Room = room;
		realX = x;
		realY = y;
		_standingElement = this.element;
		ProcessMoves(0, 0, 0);
	}
	
	public void ProcessMoves(float moveX, float moveY, float dt) {
		this.realX += moveX * dt * this.speed;
		this.realY += moveY * dt * this.speed / 2f;
		
		if (moveX != 0 || moveY != 0) {
			this.Pose = DudeSprite.Poses.Walking;
			if (moveX > 0) {
				this.facingRight = true;
			} else if (moveX < 0) {
				this.facingRight = false;
			}
		} else {
			this.Pose = null;
		}

		// check doors
		Door door = Room.GetDoor(this.realX, this.realY);
		if (door != null) {
			// change rooms
			this.Room = door.Destination;
			this.realX = door.DropOff.x;
			this.realY = door.DropOff.y;
		}
		
		if (this.realY < Room.Boundaries.yMin) {
			this.realY = Room.Boundaries.yMin;
		} else if (this.realY > Room.Boundaries.yMax) {
			this.realY = Room.Boundaries.yMax;
		}
		
		if (this.realX < Room.Boundaries.xMin) {
			this.realX = Room.Boundaries.xMin;
		} else if (this.realX > Room.Boundaries.xMax) {
			this.realX = Room.Boundaries.xMax;
		}
				
		// set background position
		this.x = this.realX;
		// set hero position
		this.y = this.realY + this.realZ;
	}
	
	override public void Redraw(bool shouldForceDirty, bool shouldUpdateDepth) {
		if (Pose == null) {
			this.element = _standingElement;
		} else if (Pose == Poses.Walking) {
			if(_frameCount > _animationSpeed)
			{
				_frameCount = 0;
				if (_frameIndex >= _walkingElements.Length) {
					_frameIndex = 0;
				}
				this.element = _walkingElements[_frameIndex++];
			}
		} else if (Pose == Poses.Punching) {
			if(_frameCount > _animationSpeed)
			{
				_frameCount = 0;
				if (_frameIndex >= _punchingElements.Length) {
					_frameIndex = 0;
					Pose = null;
				} else {
					this.element = _punchingElements[_frameIndex++];
				}
			}
		} else if (Pose == Poses.Jumping) {
			if(_frameCount > _animationSpeed)
			{
				_frameCount = 0;
				if (_frameIndex < _jumpingElements.Length) {
					// still winding up the jump
					this.element = _jumpingElements[_frameIndex++];
				}
			}
			if (_jumpFrameCount > _jumpAnimationSpeed) {
				// process the jump
				realZ += _jumpZVelocity;
				realX += _jumpXVelocity;
				_jumpZVelocity -= JUMP_DECAY;
				if (realZ <= 0) {
					_jumpXVelocity = 0;
					_jumpZVelocity = 0;
					realZ = 0;
					Pose = null;
				}
			}
		}
	
		if (facingRight) {
			this.scaleX = Mathf.Abs(this.scaleX);
		} else {
			this.scaleX = Mathf.Abs(this.scaleX) * -1;
		}
		
		float delta = Time.deltaTime;
		_frameCount += delta;
		_jumpFrameCount += delta;
		base.Redraw(shouldForceDirty, shouldUpdateDepth);
	}
}