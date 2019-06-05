using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace GCodeViewer.ViewModels
{
    public class TextEditorBase : ITextViewModel, INotifyCollectionChanged
    {
        public ITextBuffer FileBuffer { get; set; }

        public ObservableCollection<string> FileContent
        {
            get { return _FileContent; }
            set
            {
                if(value != _FileContent)
                {
                    _FileContent = value;
                    OnCollectionChanged();
                }
            }

        }
        private ObservableCollection<string> _FileContent;

        public TextEditorBase(ITextBuffer fileBuffer)
        {
            FileBuffer = fileBuffer;
            _FileContent = new ObservableCollection<string>();

            // TODO: Remove this line and add a way of parsing the actual text to this format
            FakeData();
        }

        public void ChangeLine(int lineIndex, string content)
        {
            throw new NotImplementedException();
        }
        public string[] GetCurrentContent()
        {
            var currentContent = new string[FileContent.Count];
            FileContent.CopyTo(currentContent, 0);

            return currentContent;
        }
        public void LoadFileContent()
        {
            FileContent = new ObservableCollection<string>(FileBuffer.GetContent());
        }
        public bool IsCurrentStateSaved()
        {
            // TODO implement a flag that is set to true once the file is saved and set to false when some text changes.
            return true;
        }
        public bool IsFileLoaded()
        {
            return (FileContent?.Count >= 0);
        }

        private void OnCollectionChanged()
        {
            // TODO: find out what exactly this is doing
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void FakeData()
        {
            ActualFileContent = new ObservableCollection<GCodeLine>();
            for(int i = 0; i < 10; i++)
            {
                ActualFileContent.Add(new GCodeLine(i + 1, "this is a test " + i));
            }
        }
        public ObservableCollection<GCodeLine> ActualFileContent
        {
            get { return _ActualFileContent; }
            set
            {
                if(value != _ActualFileContent)
                {
                    _ActualFileContent = value;
                    OnCollectionChanged();
                }
            }

        }
        private ObservableCollection<GCodeLine> _ActualFileContent;
    }

    public class GCodeLine
    {
        public int LineNumber { get; set; }
        public string LineContent { get; set; }

    public GCodeLine(int lineNumber, string lineContent)
    {
        LineNumber = lineNumber;
        LineContent = lineContent;
    }
}
}
