namespace Projekt
{
    public class Options: ObservableObject
    {
        private bool _autoSave;
        public bool AutoSave
        {
            get { return _autoSave; }
            set
            {
                _autoSave = value;
                OnPropertyChanged();
            }
        }
        public Options()
        {
            AutoSave = true;
        }
    }
}
