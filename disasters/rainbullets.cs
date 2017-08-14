function bulletDisaster() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            %rocketBaseSpawnPos = vectorAdd(%playerPos, getRandom(-20, 20) SPC getRandom(-20, 20) SPC getRandom(140, 200));
            
            %proj = new projectile() {
                datablock = spearProjectile;
                
                initialposition = %rocketBaseSpawnPos;
                initialvelocity = getRandom(-10, 10) SPC getRandom(-10, 10) SPC -40;
                
                client = %client;
            };
            
			%scaleFac = getRandomF(0, 2.5);
			
			%proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
            missionCleanup.add(%proj);
        }
    }
    
    $Disasters::CurrentDisasterLoop = schedule(50, 0, bulletDisaster);
}

registerDisaster("It's Raining Spears", 45, "bulletDisaster");