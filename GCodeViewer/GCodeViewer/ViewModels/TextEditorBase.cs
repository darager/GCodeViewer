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
        public void LoadBufferContent()
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
            return (FileContent != null);
        }

        private void OnCollectionChanged()
        {
            // TODO: find out what exactly this is doing
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
