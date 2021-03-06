using System.Collections.Generic;
public class ExternalEvent
{
    public string name { get; set; }
    public float level { get; set; }
    public string time { get; set; }
    public float length { get; set; }

    public ExternalEvent()
    {
        this.name = "";
        this.level = 0.0f;
        this.time = "";
        this.length = 0.0f;
    }
    public ExternalEvent(string name, float level, string time, float length)
    {
        this.name = name;
        this.level = level;
        this.time = time;
        this.length = length;
    }

    public ExternalEvent(string[] values)
    {
        this.name = values[0];
        this.level = float.Parse(values[1]);
        this.time = values[2];
        this.length = float.Parse(values[3]);
    }
}
