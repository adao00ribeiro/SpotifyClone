using SpotifyClone.Components;

namespace SpotifyClone.Services;

public class AppDataService
{
    public string menuSelecionado { get; set; } = "Home";
    private int _numero;
    public int Numero
    {
        get
        {
            return _numero;
        }
        set
        {
            _numero = value;
            NotifyDataChanged();
        }
    }
    private string _cor;
    public string Cor
    {
        get
        {
            return _cor;
        }
        set
        {
            _cor = value;
            NotifyDataChanged();
        }
    }
    public event Action OnChange;
    private void NotifyDataChanged() => OnChange?.Invoke();

}
