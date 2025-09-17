using PortalGalaxy.Common.Response;
using PortalGalaxy.WebApp.Proxy.Interfaces;

namespace PortalGalaxy.WebApp.Pages.Talleres;

public partial class TalleresListPage
{
    private readonly IJsonProxy _jsonProxy;
    private readonly ICategoriaProxy _categoriaProxy;

    public TalleresListPage(IJsonProxy jsonProxy, ICategoriaProxy categoriaProxy)
    {
        _jsonProxy = jsonProxy;
        _categoriaProxy = categoriaProxy;
    }

    public ICollection<TallerDtoResponse>? Lista { get; set; }

    public ICollection<CategoriaDtoResponse> Categorias { get; set; } = new List<CategoriaDtoResponse>();
    public ICollection<SituacionModel> Situaciones { get; set; } = new List<SituacionModel>();

    public bool IsLoading { get; set; }
    public string? Nombre { get; set; }
    public int? CategoriaId { get; set; }
    public int? Situacion { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await OnSearch();

        Categorias = await _categoriaProxy.ListAsync();
        Situaciones = await _jsonProxy.ListSituaciones();
    }

    private void OnLimpiar()
    {
        Nombre = null;
        CategoriaId = null;
        Situacion = null;
    }

    private async Task OnSearch()
    {
        try
        {
            IsLoading = true;
            var response = await Proxy.ListAsync(Nombre, CategoriaId, Situacion, 1, 15);
            if (response.Success)
            {
                Lista = response.Data;
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }
}