using Windows.ApplicationModel.DataTransfer;

namespace View;

// https://learn.microsoft.com/en-us/windows/apps/develop/windows-integration/integrate-sharesheet-send#implement-share-for-desktop-apps-winui-3-wpf-winforms

[System.Runtime.InteropServices.ComImport]
[System.Runtime.InteropServices.Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
[System.Runtime.InteropServices.InterfaceType(
    System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
interface IDataTransferManagerInterop {
    IntPtr GetForWindow([System.Runtime.InteropServices.In] IntPtr appWindow,
        [System.Runtime.InteropServices.In] ref Guid riid);
    void ShowShareUIForWindow(IntPtr appWindow);
}

public sealed partial class MainWindow // WinUI 3 Window, WPF Window, or WinForms Form
{
    // IID of DataTransferManager, passed as the riid to GetForWindow:
    static readonly Guid _dtm_iid = new(0xa5caee9b, 0x8708, 0x49d1, 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c);

    private DataTransferManager? _dtm;

    // Call this from your window or form constructor (or load handler):
    private void InitializeShare() {
        // Retrieve the window handle (HWND) for the current window:
        //   WinUI 3:  IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        //   WPF:      IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        //   WinForms: IntPtr hWnd = this.Handle;
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        IDataTransferManagerInterop interop =
            DataTransferManager.As<IDataTransferManagerInterop>();
        _dtm = WinRT.MarshalInterface<DataTransferManager>.FromAbi(
            interop.GetForWindow(hWnd, _dtm_iid));
    }

    private void ShowShareUI() {
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        IDataTransferManagerInterop interop = DataTransferManager.As<IDataTransferManagerInterop>();
        interop.ShowShareUIForWindow(hWnd);
    }

    private void Share(DataPackage data) {
        void handler(DataTransferManager sender, DataRequestedEventArgs args) {
            args.Request.Data = data;
        }

        if (_dtm is not null) {
            _dtm.DataRequested += handler;
            ShowShareUI();
            _dtm.DataRequested -= handler;
        }
    }
}