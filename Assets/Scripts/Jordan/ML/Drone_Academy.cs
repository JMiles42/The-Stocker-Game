using UnityEngine;

public class Drone_Academy: Academy
{
	public Camera Cam;
	public MapCreator MapCreator;

	public override void InitializeAcademy()
	{
		MapCreator.GenerateMap();
		Cam.transform.position = new Vector3(MapCreator.MapSettings.Data.columns / 2f, Cam.transform.position.y, MapCreator.MapSettings.Data.rows / 2f);
	}

	public override void AcademyStep()
	{ }

	public override void AcademyReset()
	{
		MapCreator.GenerateMap();
	}
}