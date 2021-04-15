using System.ComponentModel;

namespace LoginApp.ViewModel.Core
{
    /// <summary>
    /// 모든 ViewModel의 기저 클래스
    /// ( 실시간 속성 변화 반영 )
    /// </summary>
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        private object data_;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReceivedData(object data)
        {
            data_ = data;
        }
    }
}
