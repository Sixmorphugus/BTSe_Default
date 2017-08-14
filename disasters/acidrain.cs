// functions
function acidDisasterStart() {
	$skyDefault = $EnvGuiServer::SkyIdx;
	
	setEnvironment("SkyIdx", 6);
	schedule(200, 0, acidDisaster);
	
	//talk("Disabled for now, have a break instead!");
}

function acidDisasterStop() {
	setEnvironment("SkyIdx", $skyDefault);
}

function acidDisaster() {
    // bricks at the top of stacks must die
	if($at >= 5) {
		%mainBrickCount = MainBrickGroup.getCount();

		for(%i = 0; %i < %mainBrickCount; %i++) {
			%group = MainBrickGroup.getObject(%i);
			%count = %group.getCount();
			
			for(%j = 0; %j < %count; %j++) {
				%brick = %group.getObject(%j);
				
				if(!%brick.willCauseChainKill() && %brick.getNumUpBricks() <= 0 && getRandom(0, 2) && !isPointInShadow(%brick.getPosition(), %brick)) {
					%brick.fakeKillBrick("0 0 0", $DefaultMiniGame.brickRespawnTime);
				}
			}
			
			%scale = 4;
		}
		
		$at = 0;
	}
	
	$at++;
	
	%clientCount = ClientGroup.getCount();
	
	// kill players that are exposed
    if(%clientCount > 0) {
        for(%i = 0; %i < %clientCount; %i++) {
            %player = ClientGroup.getObject(%i).player;
			
			if(!isObject(%player))
				continue;
			
			// vehicle support
			%vehicle = %player.getobjectmount();
			
			if(isObject(%vehicle)) {
				if(!isPointInShadow(%vehicle.getPosition(), %vehicle)) {
					%vehicle.damage(%obj, %obj.getPosition(), 10000, $DamageType::Lava);
					%vehicle.finalExplosion();
				}
			}
			else {
				if(!isPointInShadow(%player.getHackPosition(), %player)) {
					%player.addHealth(-25);
				}
			}
		}
	}
	
    $Disasters::CurrentDisasterLoop = schedule(1000, 0, acidDisaster);
}


// REGISTRATION
registerDisaster("Acid Rain", 12, "acidDisasterStart", "acidDisasterStop");