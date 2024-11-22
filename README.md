# Variant Gunner Deluxe - What, Why, and How
Variant Gunner stands as one of my proudest works. It is an excellent mix of my personal gaming tastes, combined with my programming proficiency.

With the Deluxe version, I wanted to revisit the code base and make changes that reflect my newer coding sensibilities.

For reference:
[Original](https://github.com/polygonalcube/variant-gunner)
[Deluxe](https://github.com/polygonalcube/variant-gunner-deluxe)
## Every Script Change in Detail
### AreaTwoBackground
This script concisely reflects many general changes I made to the code base.

*Before*
``` C#
public float scrollSpd;
public bool newSectionSpawned = false;
```
*After*
``` C#
[SerializeField] private float scrollSpeed = 15f;
private bool newSectionSpawned;
```

Where possible, I
- Made public variables private
- Renamed variables and methods for clarity 
	- Removed acronyms or shortenings

This was in an effort to make the code base more immediately understandable, as the scope and purpose of each variable and method would become more clear.

I also added documentation to every script, to make them quicker to understand for hypothetical future developers (or my future self).
### BarrelerEnemy
I used ```RequireComponent``` wherever possible to reduce the likelihood of bugs born from missing components.

``` C#
[RequireComponent(typeof(EnemyMethodsComponent))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(HurtComponent))]
[RequireComponent(typeof(ShootComponent))]
```

I also gave this enemy a more sophisticated, less error-prone state machine.

*Before*
``` C#
if (state == 0)
{
    // Do this.
}
if (state == 1)
{
	// Do that.
}
```
*After*
``` C#
switch (currentState)
{
	case States.Entering:
        // Do this.
        break;
	case States.Barrelling:
        // Do that.
		break;
}
```

The commented logic that took up nearly half the lines of code were also removed, to improve clarity.
### BeelineEnemy
This script now uses ```RequireComponent```.

Much of the “aimed shot” logic was moved to ```EnemyMethodsComponent```, making the entity code cleaner, and reducing code reuse, as many other entities performed aimed shots.

*Before*
``` C#
void ShootAtPlayer()
{
	if (player != null)
	{
		Vector3 currentPos = transform.position;
		Vector3 currentEuler = transform.eulerAngles;
		transform.position = new Vector3(gun.shotOrigin.position.x, gun.shotOrigin.position.y, 0f);
		transform.LookAt(player.transform, Vector3.up);
		Vector3 pointer = transform.eulerAngles;
		transform.position = currentPos;
		transform.eulerAngles = currentEuler;

		gun.shotAngle = pointer.x - 270f;
		if (player.position.x > gun.shotOrigin.position.x)
		{
			gun.shotAngle = -gun.shotAngle;
		}
		gun.ShootAng(gun.bulletSpeedAng);
	}
}
```
*After*
``` C#
void ShootAtPlayer()
{
	if (playerTransform == null)
	{
		playerTransform = enemyMethods.FindPlayer()?.transform;
	}
	Vector3 angleToPlayer = enemyMethods.AimAtPlayer(shooter.shotOrigin);

	shooter.shotAngle = angleToPlayer.x - 270f;
	if (playerTransform?.position.x > shooter.shotOrigin.position.x)
	{
		shooter.shotAngle = -shooter.shotAngle;
	}
	shooter.ShootAng(shooter.bulletSpeedAng);
}
```
### BombBullet
General code base changes.
### BulletBomb
To highlight another general change, ```Awake()``` is now universally used to find components. ```Start()``` sees use when setting component variables. In the original code base, ```
Start()``` saw much more usage for both tasks.

*Before*
``` C#
void Start()
{
	GameObject goPlayer = GameObject.Find("Player");
	if (goPlayer != null)
	{
		player = GameObject.Find("Player").transform;
	}
}
```
*After*
``` C#
void Awake()
{
	mover = GetComponent<MoveComponent>();
	
	GameObject playerGameObject = GameObject.Find("Player");
	if (playerGameObject != null)
	{
		playerTransform = GameObject.Find("Player").transform;
	}
}
```
### BulletBombUp → REMOVED
This script was merely a copy of ```BulletBomb.cs```, only the projectile move up instead of down. The existence of this script was a product of the final development rush for Variant Gunner, and was removed once I had more time to think about how I could utilize the ```
ShootComponent``` to dynamically set the direction of the projectile.
### DefaultBullet
General code base changes.
### DespawnOffScreen
Instead of 4 separate conditionals statements, I create one big conditional statement. My hope is that this will make the logic more clear, as if anything in the big statement is true, one result will happen.

*Before*
``` C#
void Update()
{
	if (transform.position.x < bounds[0])
	{
		Destroy(this.gameObject);
	}
	if (transform.position.x > bounds[1])
	{
		Destroy(this.gameObject);
	}
	if (transform.position.y < bounds[2])
	{
		Destroy(this.gameObject);
	}
	if (transform.position.y > bounds[3])
	{
		Destroy(this.gameObject);
	}
}
```
*After*
``` C#
void Update()
{
	if (transform.position.x < bounds[0] || transform.position.x > bounds[1] || transform.position.y < bounds[2] ||
		transform.position.y > bounds[3])
	{
		Destroy(gameObject);
	}
}
```
### DestroyOnContact
Utilizes a Unity extension method.
### DoomMecha
Added cleaner state machine logic; similar change to ```BarrelerEnemy```.

Much of the shooting logic was moved to ```EnemyMethodsComponent```, similar to ```BeelinerEnemy```.
### DoppleBuster
Added cleaner state machine logic; similar change to ```BarrelerEnemy``` and ```DoomMecha```.
### EnemyMethodsComponent → ADDED
This component was added to hold common enemy and boss methods.
### EnemyTest → REMOVED
Enemy Test.cs has been deleted. As a test script, there is no use for it in the final project.
### Explosion
For variables that need to be public, but aren’t intended to be edited in the inspector, I use ```HideInInspector``` to avoid unnecessary edits.
### GameManager
Enemy spawns were grouped into methods for increased clarity.

*Before*
``` C#
IEnumerator Level1()
{
	Instantiate(enemies[0], new Vector3(-6f, 6.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(-4f, 8.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(-2f, 10.5f, 0f), Quaternion.identity);
	yield return new WaitUntil(() => gameTime > 2f);
	Instantiate(enemies[0], new Vector3(6f, 6.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(4f, 8.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(2f, 10.5f, 0f), Quaternion.identity);
	yield return new WaitUntil(() => gameTime > 6f);
	Instantiate(bosses[0], new Vector3(-200f, 0f, 500f), Quaternion.identity);
}
```
*After*
``` C#
private void SpawnThreeBeelinersAtLeft()
{
	Instantiate(enemies[0], new Vector3(-6f, 6.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(-4f, 8.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(-2f, 10.5f, 0f), Quaternion.identity);
}
    
private void SpawnThreeBeelinersAtRight()
{
	Instantiate(enemies[0], new Vector3(6f, 6.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(4f, 8.5f, 0f), Quaternion.identity);
	Instantiate(enemies[0], new Vector3(2f, 10.5f, 0f), Quaternion.identity);
}

IEnumerator Level1()
{
	yield return new WaitUntil(() => timeElapsed > 0.04f);
	levelBackground = GameObject.Find("Background").GetComponent<LevelBackground>();

	SpawnThreeBeelinersAtLeft();
	yield return new WaitUntil(() => timeElapsed > 2f);
	SpawnThreeBeelinersAtRight();
	yield return new WaitUntil(() => timeElapsed > 6f);
	Instantiate(bosses[0], new Vector3(-200f, 0f, 500f), Quaternion.identity);
}
```
### GUI
Condensed a conditional.

*Before*
``` C#
if (GameManager.gm.playerHPTrack <= 0)
{
	gameOver.gameObject.SetActive(true);
}
else
{
	gameOver.gameObject.SetActive(false);
}
```
*After*
``` C#
gameOverImage.gameObject.SetActive(GameManager.gm.playerHPTrack <= 0);
```
### DamageComponent → HitComponent
This component saw a name change, to make it more clear what purpose the component fulfills.
### HomingBullet
Significantly, logic was added to have bullets face their targets, which they didn’t originally.

*Deluxe*
``` C#
if (nearestTarget != null && nearestTarget.gameObject.activeSelf)
{
	Vector3 pointVec = nearestTarget.position - transform.position;
	pointVec *= mover.maximumSpeed.x/pointVec.magnitude;

	mover.currentSpeedX = pointVec.x;
	mover.currentSpeedY = pointVec.y;
	mover.Move(Vector3.zero);

	transform.LookAt(nearestTarget);
}
```
### HealthComponent → HPComponent
General code base changes, and a name change.
### HurtboxComponent → HurtComponent
General code base changes, and a name change.
### LaserBullet
General code base changes.
### LevelBackground
Reduced some repeated code with a method.

*Before*
``` C#
if (transitionToThree == true)
{
	transform.position = Vector3.Lerp(levelPos[1], levelPos[2], levelDist);
	transform.eulerAngles = Vector3.Slerp(levelEul[1], levelEul[2], levelDist);
}
else
{
	transform.position = Vector3.Lerp(levelPos[0], levelPos[1], levelDist);
	transform.eulerAngles = Vector3.Slerp(levelEul[0], levelEul[1], levelDist);
}
```
*After*
``` C#
if (transitioningToLevelThree)
{
	TransitionToArea(levelPositions[1], levelPositions[2], levelEulerAngles[1], levelEulerAngles[2]);
}
else
{
	TransitionToArea(levelPositions[0], levelPositions[1], levelEulerAngles[0], levelEulerAngles[1]);
}

//...

private void TransitionToArea(Vector3 previousPosition, Vector3 nextPosition, Vector3 previousEulerAngles, Vector3 nextEulerAngles)
{
	transform.position = Vector3.Lerp(previousPosition, nextPosition, levelDistance);
	transform.eulerAngles = Vector3.Slerp(previousEulerAngles, nextEulerAngles, levelDistance);
}
```
### MoveComponent
The component received a complete overhaul. Methods like ```Move()```, ```MoveAng()```, and ```ResetZ()``` still see direct use, but other methods are only utilized internally.

*Before*
``` C#
public float Accelerate(float speedVar, bool isNegative)
{
	if (isNegative)
		return speedVar - accel;
	else
		return speedVar + accel;
}
```
*After*
``` C#
public void Accelerate(ref float speedVar, float direction)
{
	speedVar += acceleration * direction;
}
```
### PlayerController
General code base changes.
### ScoreComponent
General code base changes.
### ShootComponent
Created a method to hold the reused code between the two shoot methods.

*Before*
``` C#
public void Shoot(Vector2 speed)
{
	if (shotTimer <= 0f)
	{
		GameObject newBullet = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));
		newBullet.GetComponent<MoveComponent>().xSpeed = speed.x;
		newBullet.GetComponent<MoveComponent>().ySpeed = speed.y;
		newBullet.layer = LayerMask.NameToLayer(lay);
		newBullet.GetComponent<DestroyOnContact>().layers = destroyOnContactLays;
		shotTimer = shotTimerSet;
		Destroy(newBullet, destroyTimer);
	}
}

public void ShootAng(float speed)
{
	if (shotTimer <= 0f)
	{
		GameObject newBullet = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));
		newBullet.GetComponent<MoveComponent>().maxSpeed = speed;
		newBullet.layer = LayerMask.NameToLayer(lay);
		shotTimer = shotTimerSet;
		Destroy(newBullet, destroyTimer);
	}
}
```
*After*
``` C#
GameObject BaseShoot()
{
	GameObject newBullet = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(0f, 0f, shotAngle));
	newBullet.layer = LayerMask.NameToLayer(layer);
	if (newBullet.TryGetComponent<DestroyOnContact>(out DestroyOnContact destroyOnContact))
	{
		destroyOnContact.layers = destroyOnContactLayers;
	}
	shotTimer = shotTimerSet;
	Destroy(newBullet, destroyTimer);
	return newBullet;
}

public void Shoot(Vector2 speed)
{
	if (shotTimer <= 0f)
	{
		GameObject newBullet = BaseShoot();
		if (newBullet.TryGetComponent<MoveComponent>(out MoveComponent moveComponemt))
		{
			moveComponemt.currentSpeedX = speed.x;
			moveComponemt.currentSpeedY = speed.y;
		}
	}
}

public void ShootAng(float speed)
{
	if (shotTimer <= 0f)
	{
		GameObject newBullet = BaseShoot();
		if (newBullet.TryGetComponent<MoveComponent>(out MoveComponent moveComponemt))
		{
			moveComponemt.maximumSpeed.x = speed;
		}
	}
}
```
### TitleScreen
Added support for button input.

*Deluxe*
``` C#
private void Update()
{
	if (Input.anyKeyDown)
	{
		LoadNextScene();
	}
}
```
### VulcanBullet
General code base changes.
### VulcanShield
Added cleaner state machine logic; similar change to ```BarrelerEnemy```.
### YouWin
No major changes.
