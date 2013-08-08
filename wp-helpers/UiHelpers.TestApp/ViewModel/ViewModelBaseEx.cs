using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;

namespace UiHelpers.TestApp.ViewModel
{
    public class ViewModelBaseEx : ViewModelBase
    {
        protected void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            Set(propertyName, ref field, newValue);
        }
    }
}
