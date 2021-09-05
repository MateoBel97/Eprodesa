public class MetConditions
{
   public float initialWindSpeed{ get; set; }
    public float finalWindSpeed { get; set; }
    public float initialTemperature { get; set; }
    public float finalTemperature { get; set; }
    public float initialHumidity { get; set; }
    public float finalHumidity { get; set; }
    public float initialAtmPressure { get; set; }
    public float finalAtmPressure { get; set; }

    public MetConditions (
        float newInitialWindSpeed, float newFinalWindSpeed,
        float newInitialTemperature, float newFinalTemperature,
        float newInitialHumidity, float newFinalHumidity,
        float newInitialAtmPressure, float newFinalAtmPressure
        )
    {
        initialWindSpeed = newInitialWindSpeed;
        finalWindSpeed = newFinalWindSpeed;
        initialTemperature = newInitialTemperature;
        finalTemperature = newFinalTemperature;
        initialHumidity = newInitialHumidity;
        finalHumidity = newFinalHumidity;
        initialAtmPressure = newInitialAtmPressure;
        finalAtmPressure = newFinalAtmPressure;
    }
    public MetConditions()
    {
        initialWindSpeed = 0.0f;
        finalWindSpeed = 0.0f;
        initialTemperature = 0.0f;
        finalTemperature = 0.0f;
        initialHumidity = 0.0f;
        finalHumidity = 0.0f;
        initialAtmPressure = 0.0f;
        finalAtmPressure = 0.0f;
    }
}
