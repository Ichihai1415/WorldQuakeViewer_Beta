﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using WorldQuakeViewer.Properties;

namespace WorldQuakeViewer
{
    public partial class UpdateDialog : Form
    {
        public UpdateDialog()
        {
            InitializeComponent();
        }
        private void Dialog_Load(object sender, EventArgs e)
        {
            Main.Text = $"新しいバージョンがリリースされています。\n現在バージョン:v{Settings.Default.NowVersion}　最新バージョン:v{Settings.Default.NewVersion}\n更新する場合、下のボタンを押してください。";
            if(Convert.ToDouble(Settings.Default.NowVersion) > Convert.ToDouble(Settings.Default.NewVersion))
            {
                Main.Text = $"このバージョンはリリースされていません。\n現在バージョン:v{Settings.Default.NowVersion} 公開最新バージョン:v{Settings.Default.NewVersion}";
                DLStart.Size = new Size(0, 0);
                Text = "WorldQuakeViewer：アップデート通知(開発中画面)";
            }
        }
        private void DLStart_Click(object sender, EventArgs e)
        {
            try
            {
                DLStart.Size = new Size(0, 0);
                WebClient WC = new WebClient();
                Main.Text = $"ダウンロードバージョン:v{Settings.Default.NewVersion}\nダウンロード中…";
                if (System.IO.Directory.Exists($"Update") == false)
                {
                    System.IO.Directory.CreateDirectory($"Update");
                }
                if (File.Exists($"Update\\_temp.zip"))
                {
                    File.Delete($"Update\\_temp.zip");
                }
                if (System.IO.Directory.Exists($"Update\\v{Settings.Default.NewVersion}"))
                {
                    System.IO.Directory.Delete($"Update\\v{Settings.Default.NewVersion}");
                }
                WC.DownloadFile($"https://github.com/Project-S-31415/WorldQuakeViewer/releases/download/WorldQuakeViewer{Settings.Default.NewVersion}/WorldQuakeViewer.v{Settings.Default.NewVersion}.zip", $"Update\\_temp.zip");
                Main.Text += "\nダウンロード終了\n展開中…:";
                ZipFile.ExtractToDirectory("Update\\_temp.zip", $"Update\\v{Settings.Default.NewVersion}");
                Main.Text += "\n展開終了";
                File.Delete("Update\\_temp.zip");
                Main.Text = $"DL・解凍が完了しました。\n\"v{Settings.Default.NewVersion}\"の中を現在の実行フォルダに\n上書きしてください。\n「終了」を押すと終了します。\n元ファイルの削除も忘れずに行ってください。";
                string Directory = Path.GetFullPath("WorldQuakeViewerUpdater.exe").Replace("\\WorldQuakeViewerUpdater.exe", "");
                Process.Start("explorer.exe", Directory);
                Process.Start("explorer.exe", $"{Directory}\\Update\\v{Settings.Default.NewVersion}");
            }
            catch (WebException)
            {
                Main.Text = $"ネットワークに接続できません。";
            }
            catch (Exception ex)
            {
                Main.Text = $"エラーが発生しました。\n手動ダウンロードをお試しください。\n\"Log/ErrorLog/Updater {DateTime.Now:yyyy/MM/dd}.txt\"の\n内容を製作者に報告してください。";
                try
                {
                    string ErrorText = File.ReadAllText($"Log\\ErrorLog\\Updater {DateTime.Now:yyyyMMdd}.txt") + "\n--------------------------------------------------\n" + ex;
                    File.WriteAllText($"Log\\ErrorLog\\Updater {DateTime.Now:yyyy/MM/dd}.txt", ErrorText);
                }
                catch
                {
                    File.WriteAllText($"Log\\ErrorLog\\Updater {DateTime.Now:yyyyMMdd}.txt", $"{ex}");
                }
                Process.Start("notepad.exe", $"Log\\ErrorLog\\Updater {DateTime.Now:yyyyMMdd}.txt");
            }
        }
    }
}
