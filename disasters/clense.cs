// functions
function startClensing() {
	$DefaultMinigame.fallingDamage = false;
	
    // create water
    setWater("add-ons/water_default/default.water");
	
    setEnvironment("WaterHeight", 1);
	
	activatePackage(waterSave);
	
    // start loop
    schedule(6000, 0, finishClensing);
}

function finishClensing() {
    // kill anyone who is not CLENSED
	%clientCount = ClientGroup.getCount();
    
    if(%clientCount > 0) {
        for(%i = 0; %i < %clientCount; %i++) {
            %player = ClientGroup.getObject(%i).player;
            
            if(!isObject(%player)) {
                continue;
            }
			
			if(!%player.clensed)
				%player.kill();
        }
    }
}

function clearClensing() {
    // remove water
    setEnvironment("WaterHeight", 0); // close enoff
    setEnvironment("WaterIdx", -1);
	
	deactivatePackage(waterSave);
}

package waterSave {
	function Armor::onEnterLiquid(%data,%obj,%coverage,%type)
	{
		parent::onEnterLiquid(%data,%obj,%coverage,%type);
        
		%obj.clensed = true;
	}
};

registerDisaster("Quick! Clense yourselves", 20, "startClensing", "clearClensing");