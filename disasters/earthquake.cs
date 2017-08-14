function startQuake() {
    // create water
    setWater("add-ons/water_default/default.water");
	
    setEnvironment("WaterHeight", 1);
    
    $qcontinuetime = 100;
    $qt = 0;
	
	activatePackage(waterDamage);
	
    // start loop
    continueQuake();
}

function continueQuake() {
    // break bricks randomly like black hole does
    $qt++;
	
	if($qt > 20) {
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
		
		$qt = 0;
	}
	
	// throw players around
	%clientCount = ClientGroup.getCount();
    
    if(%clientCount > 0) {
        for(%i = 0; %i < %clientCount; %i++) {
            %player = ClientGroup.getObject(%i).player;
            
            if(!isObject(%player)) {
                continue;
            }
			
			%vehicle = %player.getObjectMount();
			
			if(isObject(%vehicle)) {
				%player.Dismount();
			}

            %player.addVelocity(getRandom(-6, 6) SPC getRandom(-6, 6) SPC 0);
        }
    }
	
    $Disasters::CurrentDisasterLoop = schedule($qcontinuetime, 0, continueQuake);
}

function clearQuake() {
    // remove water
    setEnvironment("WaterHeight", 0); // close enoff
    setEnvironment("WaterIdx", -1);
	
	deactivatePackage(waterDamage);
}

registerDisaster("An Earthquake", 20, "startQuake", "clearQuake");
