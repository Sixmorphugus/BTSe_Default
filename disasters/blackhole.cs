$bht = 0;

function endBHDisaster() {
	// ...
}

function startBHDisaster() {
	$DefaultMinigame.fallingDamage = false;
	
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            $BHPos = vectorAdd(%playerPos, getRandom(-10, 10) SPC getRandom(-10, 10) SPC getRandom(50, 140));
        }
    } else {
        $BHPos = "0 0 100";
    }
    
    schedule(200, 0, continueBHDisaster);
}

function continueBHDisaster() {
    %clientCount = ClientGroup.getCount();
    
    if(%clientCount > 0) {
        for(%i = 0; %i < %clientCount; %i++) {
            %player = ClientGroup.getObject(%i).player;
            
            if(!isObject(%player)) {
                continue;
            }
            
            %scale = 1;
			
			// vehicle support
			%vehicle = %player.getobjectmount();
			
			if(isObject(%vehicle)) {
				%player.dismount();
			}
			else {
				%player.setVelocity(vectorAdd(%player.getVelocity(),vectorScale(vectorSub($BHPos,%player.position),%scale)));
			}
        }
    }
    
	$bht++;
	
	if($bht > 20) {
		// pull in random bricks
		%mainBrickCount = MainBrickGroup.getCount();

		for(%i = 0; %i < %mainBrickCount; %i++) {
			%group = MainBrickGroup.getObject(%i);
			%count = %group.getCount();
			
			%brickToPull = getRandom(0, %count-1);
			
			%brickToPullObj = %group.getObject(%brickToPull);
			
			%scale = 4;
			
			%brick = %brickToPullObj;
			
			//if(!%brick.willCauseChainKill()) {
				%brick.fakeKillBrick(vectorSub($BHPos,%player.position), $DefaultMiniGame.brickRespawnTime);
			//}
		}
		
		$bht = 0;
	}

    // check if anyone's in range of the black hole
    initContainerRadiusSearch($BHPos, 15, $TypeMasks::PlayerObjectType);
    
    while(%target = containerSearchNext()) {
        %target.kill();
    }
    
    // continue
    $Disasters::CurrentDisasterLoop = schedule(100, 0, continueBHDisaster);
}

function endWHDisaster() {
	setEnvironment("SkyIdx", $skyDefault);
}

function startWHDisaster() {
	$skyDefault = $EnvGuiServer::SkyIdx;
	
	setEnvironment("SkyIdx", 6);
	
	$DefaultMinigame.fallingDamage = false;
	
    // spawn rocket in relation to a player 100 TUs above them with random offset
    %clientCount = ClientGroup.getCount();
    if(%clientCount > 0) {
        %client = ClientGroup.getObject(getRandom(0, %clientCount-1));
        
        %player = %client.player;
        
        if(isObject(%player)) {
            %playerPos = %player.getPosition();
            $BHPos = vectorAdd(%playerPos, getRandom(-10, 10) SPC getRandom(-10, 10) SPC getRandom(50, 140));
        }
    } else {
        $BHPos = "0 0 100";
    }
    
    schedule(200, 0, continueWHDisaster);
}

function continueWHDisaster() {
    %clientCount = ClientGroup.getCount();
    
    if(%clientCount > 0) {
        for(%i = 0; %i < %clientCount; %i++) {
            %player = ClientGroup.getObject(%i).player;
            
            if(!isObject(%player)) {
                continue;
            }
            
            %scale = 1;
			
			// vehicle support
			%vehicle = %player.getobjectmount();
			
			if(isObject(%vehicle)) {
				%player.dismount();
			}
			else {
				%player.setVelocity(vectorAdd(%player.getVelocity(),vectorScale(vectorSub($BHPos, %player.position),%scale)));
			}
        }
    }
    
	$bht++;
	
	if($bht > 20) {
		// pull in random bricks
		%mainBrickCount = MainBrickGroup.getCount();

		for(%i = 0; %i < %mainBrickCount; %i++) {
			%group = MainBrickGroup.getObject(%i);
			%count = %group.getCount();
			
			%brickToPull = getRandom(0, %count-1);
			
			%brickToPullObj = %group.getObject(%brickToPull);
			
			%scale = 4;
			
			%brick = %brickToPullObj;
			
			if(!%brick.willCauseChainKill()) {
				%brick.fakeKillBrick(vectorSub($BHPos, %brick.position), $DefaultMiniGame.brickRespawnTime);
			}
		}
		
		$bht = 0;
	}

    // check if anyone's in range of the black hole
    initContainerRadiusSearch($BHPos, 15, $TypeMasks::PlayerObjectType);
    
    while(%target = containerSearchNext()) {
        %target.kill();
    }
    
    // continue
    $Disasters::CurrentDisasterLoop = schedule(100, 0, continueWHDisaster);
}

registerDisaster("Hurricane", 12, "startWHDisaster", "endWHDisaster");
registerDisaster("A Black Hole", 12, "startBHDisaster", "endBHDisaster", 1);