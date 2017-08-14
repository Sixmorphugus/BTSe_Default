function startTsunami() {
    // create water
    setWater("add-ons/water_default/default.water");
    
    setEnvironment("WaterHeight", 0);
    
    $tsunamiwaterheight = 0;
    $tsunamicontinuetime = 50;
    
	activatePackage(waterDamage);
	
    // start loop
    continueTsunami();
}

function continueTsunami() {
    // raise water
    //$tsunamicontinuetime += 1000;
    $tsunamiwaterheight += 0.05;
    
    setEnvironment("WaterHeight", $tsunamiwaterheight);
    
    $Disasters::CurrentDisasterLoop = schedule($tsunamicontinuetime, 0, continueTsunami);
}

function clearTsunami() {
    // remove water
    setEnvironment("WaterHeight", 0); // close enoff
    setEnvironment("WaterIdx", -1);
	
	deactivatePackage(waterDamage);
}

registerDisaster("Flood", 18, "startTsunami", "clearTsunami");
registerDisaster("Tsunami", 40, "startTsunami", "clearTsunami", 1);