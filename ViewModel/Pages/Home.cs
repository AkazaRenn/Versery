using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Access;
using System.Collections.ObjectModel;

namespace ViewModel.Pages; 
public sealed partial class Home {
    private readonly Client client = Utilities.Services.Get<Client>();

    public ObservableCollection<Controls.Status> Statuses { get; } = [];

    //[RelayCommand]
    //void Load() {
    //    if (!client.Ready)
    //}
}
