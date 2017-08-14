function startFountainDisaster() {
	%clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            $FPos = vectorAdd(%playerPos, getRandom(-20, 20) SPC getRandom(-20, 20) SPC 0);
        }
    } else {
        $FPos = "0 0 0";
    }
	
	if(!isPointInShadow($FPos)) {
		fountainDisaster();
	} else {
		startFountainDisaster();
	}
}

function fountainDisaster() {
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            %scale = 5;
			
            %proj = new projectile() {
                datablock = spearProjectile;
                
                initialposition = $FPos;
                initialvelocity = vectorScale(vectorSub(%player.position,$FPos),%scale);
                
                client = %client;
            };
            
            %scaleFac = getRandomF(1, 2);
            
            %proj.setScale(%scaleFac SPC %scaleFac SPC %scaleFac);
            missionCleanup.add(%proj);	
        }
    }
    
    $Disasters::CurrentDisasterLoop = schedule(200, 0, fountainDisaster);
}

registerDisaster("A Fountain Of Spears", 20, "startFountainDisaster");