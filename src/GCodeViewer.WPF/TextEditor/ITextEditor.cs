using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;
using Microsoft.Win32;

namespace GCodeViewer.WPF.TextEditor
{

    public interface ITextEditor
    {
        void SetText(string text);
        string GetText();
        event EventHandler TextChanged;
    }
}
