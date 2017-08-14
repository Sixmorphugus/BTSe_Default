function mnukeDisaster() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            %rocketBaseSpawnPos = vectorAdd(%playerPos, getRandom(-20, 20) SPC getRandom(-20, 20) SPC getRandom(50, 140));
            
            %proj = new projectile() {
                datablock = CannonBallProjectile;
                
                initialposition = %rocketBaseSpawnPos;
                initialvelocity = "0 0 -40";
                
                client = %client;
            };
            
            %scaleFac = 9;
            
            %proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
            missionCleanup.add(%proj);
        }
    }
}

function nukeDisaster() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            %rocketBaseSpawnPos = vectorAdd(%playerPos, getRandom(-20, 20) SPC getRandom(-20, 20) SPC getRandom(50, 140));
            
            %proj = new projectile() {
                datablock = CannonBallProjectile;
                
                initialposition = %rocketBaseSpawnPos;
                initialvelocity = "0 0 -40";
                
                client = %client;
            };
            
            %scaleFac = 9;
            
            %proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
            missionCleanup.add(%proj);
        }
    }
}

registerDisaster("Mini-Nuke", 10, "mnukeDisaster", "");
registerDisaster("Nuke", 10, "nukeDisaster", "", 1);