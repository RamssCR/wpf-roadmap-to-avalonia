# Routing
Routing in WPF allows you to move among different views in an application.

## Creating a router
When creating a router in WPF, you must keep in mind how the `DataContext` tree works:

- A "View" is an `UserControl`, just like a View in React Router is a React component.
- Views aren't persisted due to its local environment, so navigation is handled by state.
- An `UserControl` is unaware of the `DataContext` up in the `MainWindow`.

### Creating all ViewModels for all views
Say we have - for this example - 2 views:

- `NotesView`
- `NoteDetailView`

We create their respective ViewModels...

```csharp
// `NotesView`
public class NotesViewModel : BaseViewModel {}

// `NoteDetailView`
public class NoteDetailViewModel : BaseViewModel {}
```

### Creating the main ViewModel

```csharp
public class MainViewModel : BaseViewModel 
{
    private BaseViewModel _currentView = new BaseViewModel();
    
    public BaseViewModel CurrentView 
    {
        get => _currentView;
        private set => SetProperty(ref _currentView, value);
    }
    
    public ICommand ShowNotesCommand { get; }
    public ICommand ShowNoteDetailCommand { get; }
    
    public MainViewModel() 
    {
        CurrentView = new NotesViewModel();
        
        ShowNotesCommand = new RelayCommand(_ => CurrentView = new NotesViewModel());
        ShowNoteDetailCommand = new RelayCommand(_ => CurrentView = new NoteDetailViewModel());
    }
}
```

### Configuring the main view

```xaml
<Window x:Class="DesktopPlayground.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:vm="clr-namespace:DesktopPlayground.ViewModels"
        xmlns:views="clr-namespace:DesktopPlayground.ContentControl"
        mc:Ignorable="d"
        Title="WPF Playground" Height="450" Width="800"
        WindowStartupLocation="CenterScreen">
    <Window.DataContext>
        <vm:MainViewModel />
    </Window.DataContext>
    <Window.Resources>
        <DataTemplate DataType="{x:Type vm:NotesViewModel}">
            <views:NotesView />
        </DataTemplate>
        <DataTemplate DataType="{x:Type vm:NoteDetailViewModel}">
            <views:NoteDetailView />
        </DataTemplate>
    </Window.Resources>
    <Grid>
        <ContentControl Content="{Binding CurrentView}" />
    </Grid>
</Window>
```