using GCodeViewer.WPF.Abstractions.FileAccess;
using GCodeViewer.WPF.Abstractions.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace GCodeViewer.WPF.Views.ViewModels
{
    public class TextEditorBase : ITextViewModel, INotifyCollectionChanged
    {
        public ITextBuffer FileBuffer { get; set; }

        public ObservableCollection<string> FileContent
        {
            get { return _FileContent; }
            set
            {
                if (value != _FileContent)
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
        public void LoadFileContent()
        {
            var content = FileBuffer.GetContent();
            var list = new List<string>();
            foreach (var line in content)
                list.Add(line);

            var collection = new ObservableCollection<string>(list);

            FileContent = collection;
        }
        private ObservableCollection<string> GetContent()
        {
            var content = new ObservableCollection<string>();

            var bufferContent = FileBuffer.GetContent();

            foreach (var line in bufferContent)
                content.Add(line);

            return content;
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
            //TODO: find out what exactly this is doing
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
            }
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
