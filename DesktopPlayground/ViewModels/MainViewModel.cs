using System.Windows.Input;
using DesktopPlayground.Commands;

namespace DesktopPlayground.ViewModels;

public class MainViewModel : Notifier
{
    private Notifier _currentView = new NotesViewModel();

    public Notifier CurrentView
    {
        get => _currentView;
        private set => SetProperty(ref _currentView, value);
    }
    
    public ICommand ShowNotesCommand { get; }
    public ICommand ShowDetailsCommand { get; }

    public MainViewModel()
    {
        CurrentView = new NotesViewModel();
        
        ShowNotesCommand = new RelayCommand(_ => CurrentView =  new NotesViewModel());
        ShowDetailsCommand = new RelayCommand(_ => CurrentView = new NoteDetailViewModel());
    }
}