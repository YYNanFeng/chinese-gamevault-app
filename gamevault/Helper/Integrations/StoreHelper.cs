using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Windows.Services.Store;

namespace gamevault.Helper
{
    internal class StoreHelper
    {
        private StoreContext context = null;
        private IReadOnlyList<StorePackageUpdate> updates = null;
        internal StoreHelper()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
            }
        }
        public async Task<bool> UpdatesAvailable()
        {
            updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
            if (updates.Count != 0)
            {
                return true;
            }
            return false;
        }
        public async Task DownloadAndInstallAllUpdatesAsync(Window window)
        {
            var wih = new System.Windows.Interop.WindowInteropHelper(window);
            var handlePtr = wih.Handle;
            //var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);            
            WinRT.Interop.InitializeWithWindow.Initialize(context, handlePtr);
            IAsyncOperationWithProgress<StorePackageUpdateResult, StorePackageUpdateStatus> installOperation = this.context.RequestDownloadAndInstallStorePackageUpdatesAsync(updates);
            StorePackageUpdateResult result = await installOperation.AsTask();
            switch (result.OverallState)
            {
                case StorePackageUpdateState.Completed:
                    break;
                case StorePackageUpdateState.Canceled:
                    UpdateCanceledException();
                    break;
                default:
                    var failedUpdates = result.StorePackageUpdateStatuses.Where(status => status.PackageUpdateState != StorePackageUpdateState.Completed);

                    if (failedUpdates.Count() != 0)
                    {
                        PackageException();
                    }
                    break;
            }
        }
        public void NoInternetException()
        {
            MessageBox.Show("无法连接到微软服务", "连接失败", MessageBoxButton.OK, MessageBoxImage.Warning);          
        }
        private void PackageException()
        {
            MessageBox.Show("更新未能按预期安装。", "更新失败", MessageBoxButton.OK, MessageBoxImage.Warning);            
        }
        private void UpdateCanceledException()
        {
            MessageBox.Show("更新未能按预期安装。", "更新已取消", MessageBoxButton.OK, MessageBoxImage.Warning);
            //MessageBox.Show("GameVault can not start because the Updates were not installed.", "Updates not installed", MessageBoxButton.OK, MessageBoxImage.Information);           
        }
    }
}
