namespace SmartTech.Marketing.Core.AppSetting;

public class RedisAppSetting
{
    public static string SectionName { get; set; } = "Redis";
    public ElasticCache ElasticCache { get; set; }
    public OnPrem OnPrem { get; set; }
    public bool Enable { get; set; }
    public string RedisClientType { get; set; }
}
public class ElasticCache
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string PrimaryEndPoint { get; set; }
    public string ReplicaEndPoint { get; set; }
}

public class OnPrem
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
    public string InstanceName { get; set; }

}

public class RedisClientType
{
    public const string ElasticCache = "ElasticCache";
    public const string OnPrem = "OnPrem";
}

