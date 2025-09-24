using BlazorBootstrap;
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

    public Grid<TallerDtoResponse> Grilla { get; set; } = null!;

    public bool IsLoading { get; set; }
    public string? Nombre { get; set; }
    public int? CategoriaId { get; set; }
    public int? Situacion { get; set; }

    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 15;
    public int TotalCount { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Categorias = await _categoriaProxy.ListAsync();
        Situaciones = await _jsonProxy.ListSituaciones();
    }

    private async Task OnRefresh()
    {
        await Grilla.ResetPageNumber();
        await Grilla.RefreshDataAsync();
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
            var response = await Proxy.ListAsync(Nombre, CategoriaId, Situacion, CurrentPage, PageSize);
            if (response.Success)
            {
                Lista = response.Data;
                TotalCount = response.TotalCount;
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

    private async Task<GridDataProviderResult<TallerDtoResponse>> OnReadData(GridDataProviderRequest<TallerDtoResponse> request)
    {
        CurrentPage = request.PageNumber;
        PageSize = request.PageSize;

        await OnSearch();

        return await Task.FromResult(new GridDataProviderResult<TallerDtoResponse>
        {
            Data = Lista ?? new List<TallerDtoResponse>(),
            TotalCount = TotalCount
        });
    }
}