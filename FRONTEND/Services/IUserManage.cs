namespace FRONTEND.Services;

public interface IPadepokanManage
{
    PadepokanTransaksi Ready();
}
public class PadepokanManage : IPadepokanManage
{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _iConfig;

    public PadepokanManage(IHttpClientFactory httpClientFactory, IConfiguration iConfig)
    {
        _httpClientFactory = httpClientFactory;
        _iConfig = iConfig;
    }

    public PadepokanTransaksi Ready()
    {
        var baseurl = _iConfig.GetSection("Configs")["BackEndApi"];
        var httpClient = _httpClientFactory.CreateClient("BaseClient");
        return new PadepokanTransaksi(baseurl, httpClient);
    }
}