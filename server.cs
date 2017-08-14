if($Disasters::AllowExt $= "")
{
	%error = ForceRequiredAddOn("Script_Disasters");

	if(%error == $Error::AddOn_Disabled)
	{
	   // we just forced the disaster script to load, but the user had it disabled.
	   // for now, we'll just prevent it from functioning without console use by disabling the command package
	   // [todo]
	}
	
	if(%error == $Error::AddOn_NotFound)
	{
	   //we don't have disasters, so we're screwed
		error( "Build to survive Gamemode/Script not running!" );
		return;
	}
}

exec("./support.cs");

exec("./piratecannon/server.cs");

exec("./disasters/acidrain.cs");
exec("./disasters/clense.cs");
exec("./disasters/blackhole.cs");
exec("./disasters/earthquake.cs");
exec("./disasters/move.cs");
exec("./disasters/nuke.cs");
exec("./disasters/nomove.cs");
exec("./disasters/rainbullets.cs");
exec("./disasters/risinglava.cs");
exec("./disasters/rockets.cs");
exec("./disasters/sandstorm.cs");
exec("./disasters/solarapoc.cs");
exec("./disasters/strongwinds.cs");
exec("./disasters/fountain.cs");
exec("./disasters/explode.cs");