// functions
function startWinds() {
    // create water
    setWater("add-ons/water_default/default.water");
	
    setEnvironment("WaterHeight", 1);
    
	$skyDefault = $EnvGuiServer::SkyIdx;
	setEnvironment("SkyIdx", 1);
	
    $windcontinuetime = 100;
	$windVel = getRandom(-20, 20);
	$windDir = getRandom(0, 1);
    $wt = 0;
	
	activatePackage(waterDamage);
	
    // start loop
    continueWinds();
}

function continueWinds() {
    // break bricks randomly like black hole does
    $wt++;
	
	if($wt > 20) {
		%mainBrickCount = MainBrickGroup.getCount();

		for(%i = 0; %i < %mainBrickCount; %i++) {
			%group = MainBrickGroup.getObject(%i);
			%count = %group.getCount();
			
			%brickToPull = getRandom(0, %count-1);
			
			%brickToPullObj = %group.getObject(%brickToPull);
			
			%scale = 4;
			if(!%brickToPullObj.willCauseChainKill()) {
				%brickToPullObj.fakeKillBrick("0 0 0", $DefaultMiniGame.brickRespawnTime);
			}
		}
		
		$wt = 0;
	}
	
	// throw players around
	%clientCount = ClientGroup.getCount();
    
    if(%clientCount > 0) {
        for(%i = 0; %i < %clientCount; %i++) {
            %player = ClientGroup.getObject(%i).player;
            
            if(!isObject(%player)) {
                continue;
            }
			
			// vehicle support
			%vehicle = %player.getobjectmount();
			
			if(isObject(%vehicle)) {
				%player.Dismount();
			}
			else {
				if(!isPointInShadow(%player.getHackPosition(), %player)) {
					if($WindDir)
						%player.addVelocity($windVel SPC 0 SPC 0);
					else
						%player.addVelocity(0 SPC $windVel SPC 0);
				}
			}
        }
    }
	
    $Disasters::CurrentDisasterLoop = schedule($windcontinuetime, 0, continueWinds);
}

function clearWinds() {
    // remove water
    setEnvironment("WaterHeight", 0); // close enoff
    setEnvironment("WaterIdx", -1);
	setEnvironment("SkyIdx", $skyDefault);
	
	deactivatePackage(waterDamage);
}

registerDisaster("Strong Winds! Stay indoors", 20, "startWinds", "clearWinds");