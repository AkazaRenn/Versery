using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Api;
using System.Collections.ObjectModel;

namespace ViewModel.Pages; 
public partial class Home(Client client) {
    public ObservableCollection<Controls.Status> Statuses { get; } = [];

    //[RelayCommand]
    //void Load() {
    //    if (!client.Ready)
    //}
}
