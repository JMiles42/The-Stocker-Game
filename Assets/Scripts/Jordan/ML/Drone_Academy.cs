public class Drone_Academy: Academy
{
	public MapCreator MapCreator;

	public override void InitializeAcademy()
	{
		MapCreator.GenerateMap();
	}

	public override void AcademyStep()
	{ }

	public override void AcademyReset()
	{
		MapCreator.GenerateMap();
	}
}