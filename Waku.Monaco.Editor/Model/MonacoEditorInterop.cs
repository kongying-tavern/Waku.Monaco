using System.Runtime.InteropServices;

namespace Waku.Monaco.Editor.Model;

public static class MonacoEditorInterop
{
    [ComVisible(true)]
    public class OnDidChangeCursorPosition
    {
        public string Prop { get; set; } = "Example";
    }
    [ComVisible(true)]
    public class Bridge
    {
        public OnDidChangeCursorPosition OnDidChangeCursorPosition { get; set; } = new OnDidChangeCursorPosition();
        // js call this func and get message.
        public string GetMessageInfo() => "C# to JS";
        // js call this func and return message.
        public string ShowMessage(string msg) => msg;
    }
}
