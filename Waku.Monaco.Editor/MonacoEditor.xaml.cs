using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls;

using Microsoft.Web.WebView2.Core;

using Waku.Monaco.Editor.Model;

namespace Waku.Monaco.Editor;

public partial class MonacoEditor : UserControl
{
    public MonacoEditor()
    {
        InitializeComponent();
        CreateEditor();
    }
    private async void CreateEditor()
    {
        const string hostName = "editor.invalid";
        const string folderName = "Monaco";
        string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

        WebView2.CoreWebView2InitializationCompleted += WebView2_CoreWebView2InitializationCompleted;
        await WebView2.EnsureCoreWebView2Async();
        WebView2.CoreWebView2.SetVirtualHostNameToFolderMapping(hostName, folderPath, CoreWebView2HostResourceAccessKind.Allow);
        WebView2.CoreWebView2.Navigate(new Uri($"https://{hostName}/index.html").ToString());
    }

    public async Task Layout(double ActualWidth, double ActualHeight)
    {
        string layout = new JsonObject
        {
            ["width"] = ActualWidth,
            ["height"] = ActualHeight
        }.ToJsonString();
        await WebView2.ExecuteScriptAsync($"editor.layout({layout})");
    }
    public async Task<string> GetPosition()
    {
        return await WebView2.ExecuteScriptAsync("editor.getPosition()");
    }

    private void WebView2_CoreWebView2InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        if (WebView2?.CoreWebView2 != null)
        {
            WebView2.CoreWebView2.AddHostObjectToScript("bridge", new MonacoEditorInterop.Bridge());
            WebView2.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("let bridge = window.chrome.webview.hostObjects.bridge;");
        }
    }
}
