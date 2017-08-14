function bulletDisaster2() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
	for(%i = 0; %i < 5; %i++) {
		%clientCount = ClientGroup.getCount();
		if(%clientCount > 0) {
			%client = ClientGroup.getObject(getRandom(0, %clientCount-1));
			
			%player = %client.player;
			
			if(isObject(%player)) {
				%playerPos = %player.getPosition();
				%rocketBaseSpawnPos = vectorAdd(%playerPos, getRandom(-20, 20) SPC getRandom(50, 140) SPC getRandom(-20, 20));
				
				%proj = new projectile() {
					datablock = gunProjectile;
					
					initialposition = %rocketBaseSpawnPos;
					initialvelocity = "0 -500 0";
					
					client = %client;
				};
				
				%scaleFac = 9;
				
				%proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
				missionCleanup.add(%proj);
			}
		}
	}
    
    $Disasters::CurrentDisasterLoop = schedule(50, 0, bulletDisaster2);
}

function bulletDisaster3() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
	for(%i = 0; %i < 5; %i++) {
		%clientCount = ClientGroup.getCount();
		if(%clientCount > 0) {
			%client = ClientGroup.getObject(getRandom(0, %clientCount-1));
			
			%player = %client.player;
			
			if(isObject(%player)) {
				%playerPos = %player.getPosition();
				%rocketBaseSpawnPos = vectorAdd(%playerPos, getRandom(-20, 20) SPC getRandom(50, 140) SPC getRandom(-20, 20));
				
				%proj = new projectile() {
					datablock = spearProjectile;
					
					initialposition = %rocketBaseSpawnPos;
					initialvelocity = "0 -500 0";
					
					client = %client;
				};
				
				%scaleFac = 9;
				
				%proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
				missionCleanup.add(%proj);
			}
		}
	}
    
    $Disasters::CurrentDisasterLoop = schedule(50, 0, bulletDisaster3);
}

registerDisaster("Bullet Storm", 60, "bulletDisaster2");
registerDisaster("Spear Storm", 10, "bulletDisaster3", "", 1);