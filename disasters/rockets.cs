function rocketDisaster() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            %rocketBaseSpawnPos = vectorAdd(%playerPos, getRandom(-100, 100) SPC getRandom(-100, 100) SPC getRandom(50, 140));
            
            %proj = new projectile() {
                datablock = CannonBallProjectile;
                
                initialposition = %rocketBaseSpawnPos;
                initialvelocity = getRandom(-10, 10) SPC getRandom(-10, 10) SPC -40;
                
                client = %client;
            };
            
            %scaleFac = getRandomF(0, 3.5);
            
            %proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
            missionCleanup.add(%proj);	
        }
    }
    
    $Disasters::CurrentDisasterLoop = schedule(1000, 0, rocketDisaster);
}

registerDisaster("A Barrage Of Meteorites", 40, "rocketDisaster");