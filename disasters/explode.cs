function EDisaster() {
    %clientCount = ClientGroup.getCount();

	// detonate random bricks
	%count = MainBrickGroup.getCount();
	%group = MainBrickGroup.getObject(getRandom(0, %count-1));
	%count = %group.getCount();
	
	%brickToDet = getRandom(0, %count-1);
	
	%brickToDetObj = %group.getObject(%brickToDet);
	
	%clientCount = ClientGroup.getCount();
	if(%clientCount > 0) {
		%client = ClientGroup.getObject(getRandom(0, %clientCount-1));
	
		%obj = new Projectile()
		{
		   dataBlock = CannonBallProjectile;
		   initialPosition = %brickToDetObj.getPosition();
		   
		   client = %client;
		};

		MissionCleanup.add(%obj);

		%obj.setScale("2 2 2"); // for now
		%obj.explode();
	}
    
    // continue
    $Disasters::CurrentDisasterLoop = schedule(1000, 0, EDisaster);
}

registerDisaster("Random Bricks Explode", 20, "EDisaster", "", 1);