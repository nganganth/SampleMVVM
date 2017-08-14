using System.ComponentModel;

namespace SettingApp.Model
{
    public class SettingModel
    {
        public class Setting : INotifyPropertyChanged
        {
            private string settingField;
            private string settingValue;


            public string SettingField
            {
                get
                {
                    return settingField;
                }
                set
                {
                    if (settingField != value)
                    {
                        settingField = value;
                        RaiseItemChangedEvents("SettingField");
                    }                       
                }
            }

            public string SettingValue
            {
                get
                {
                    return settingValue;
                }
                set
                {
                    if (settingValue != value)
                    {
                        settingValue = value;
                        RaiseItemChangedEvents("SettingValue");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void RaiseItemChangedEvents(string property)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
