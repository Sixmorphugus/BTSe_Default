function snMoveDisaster() {
	$Disasters::CurrentDisasterLoop = schedule(200, 0, nMoveDisaster);
}

function nMoveDisaster() {
	%clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
			// vehicle support
			%vehicle = %player.getobjectmount();
			
			if(isObject(%vehicle))
				%moving = vectorLen(setWord(%player.getVelocity(), 2, 0)) > 0.1;
			else
				%moving = vectorLen(setWord(%vehicle.getVelocity(), 2, 0)) > 0.1;
			
			if(%moving) {
				%player.kill();
				
				//%obj = new Projectile()
				//{
				//   dataBlock = CannonBallProjectile;
				//   initialPosition = %player.getHackPosition();
				//   
				//   client = %player.client;
				//};

				//MissionCleanup.add(%obj);

				//%obj.setScale("1 1 1"); // for now
				//%obj.explode();
			}
		}
	}
	
	$Disasters::CurrentDisasterLoop = schedule(50, 0, nMoveDisaster);
}

registerDisaster("Don't Move", 10, "snMoveDisaster");