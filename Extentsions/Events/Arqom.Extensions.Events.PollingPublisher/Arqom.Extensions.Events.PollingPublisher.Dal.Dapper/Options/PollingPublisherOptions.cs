namespace Arqom.Extensions.Events.PollingPublisher.Dal.Dapper.Options
{
    public class PollingPublisherDalRedisOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public string SelectCommand { get; set; } = "Select top (@Count) * from Arqom.OutBoxEventItems where IsProcessed = 0";
        public string UpdateCommand { get; set; } = "Update Arqom.OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in @Ids";
    }
}
