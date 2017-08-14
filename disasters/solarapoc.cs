// functions
function sApocS() {
	$Disasters::CurrentDisasterLoop = schedule(200, 0, sApoc);
}

function sApoc() {
	// kill players that are exposed
	%clientCount = ClientGroup.getCount();
	
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
	
    $Disasters::CurrentDisasterLoop = schedule(1000, 0, sApoc);
}

registerDisaster("Solar Apocalypse", 30, "sApocS");