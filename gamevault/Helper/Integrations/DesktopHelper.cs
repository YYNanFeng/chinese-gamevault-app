using gamevault.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Threading.Tasks;
using gamevault.Models;

namespace gamevault.Helper
{
    public static class DesktopHelper
    {
        public static async Task CreateShortcut(Game game, string iconPath, bool ask)
        {
            try
            {
                string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string shortcutPath = desktopDir + @"\\" + game.Title + ".url";
                if (File.Exists(shortcutPath))
                {
                    MainWindowViewModel.Instance.AppBarText = "桌面快捷方式已存在";
                    return;
                }
                if (ask)
                {
                    MessageDialogResult result = await ((MetroWindow)App.Current.MainWindow).ShowMessageAsync($"要为 {game.Title} 创建桌面快捷方式吗？", "",
                    MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "是", NegativeButtonText = "否", AnimateHide = false });

                    if (result != MessageDialogResult.Affirmative)
                        return;
                }
                using (StreamWriter writer = new StreamWriter(shortcutPath))
                {
                    writer.Write("[InternetShortcut]\r\n");
                    writer.Write($"URL=gamevault://start?gameid={game.ID}" + "\r\n");
                    writer.Write("IconIndex=0\r\n");
                    writer.Write("IconFile=" + iconPath.Replace('\\', '/') + "\r\n");
                    //writer.WriteLine($"WorkingDirectory={Path.GetDirectoryName(SavedExecutable).Replace('\\', '/')}");
                    writer.Flush();
                }

            }
            catch { }
        }
        public static void RemoveShotcut(Game game)
        {
            try
            {
                string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string shortcutPath = desktopDir + @"\\" + game.Title + ".url";
                if (File.Exists(shortcutPath))
                {
                    File.Delete(shortcutPath);
                }
            }
            catch { }
        }
        public static bool ShortcutExists(Game game)
        {
            string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = desktopDir + @"\\" + game.Title + ".url";
            return File.Exists(shortcutPath);
        }
    }
}
