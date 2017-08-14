using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Linq;
using System;
using System.Windows.Input;
using SettingApp.Model;
using System.ComponentModel;

namespace SettingApp.ViewModel
{
    public class SettingViewModel : INotifyPropertyChanged
    {   
        private ObservableCollection<SettingModel.Setting> _settings = new ObservableCollection<SettingModel.Setting>();
        public ObservableCollection<SettingModel.Setting> Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                if (_settings != value)
                {
                    _settings = value;
                    RaiseItemChangedEvents("Settings");
                }                  
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseItemChangedEvents(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public ICommand LoadFileCommand
        {
            get { return new RelayCommand(LoadFile); }
        }
        public ICommand SaveFileCommand
        {
            get { return new RelayCommand(SaveFile); }
        }

        private void LoadFile()
        {
            string filename = null;
            ObservableCollection<SettingModel.Setting> settings = new ObservableCollection<SettingModel.Setting>();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Database Files (*" + View.WindowView.suffix + ") | *" + View.WindowView.suffix;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filename = dlg.FileName;
            }
            string[] lines = File.ReadAllLines(filename);
            settings.Clear();
            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    List<string> results = lines[i].Split(',').ToList();
                    if ((results.Count() != 2) || (results[0].Equals("")) || (results[1].Equals("")))
                    {
                        throw new System.Exception("File format is incorrect! line:" + i);
                    }
                    else
                    {
                        settings.Add(new SettingModel.Setting { SettingField = results[0], SettingValue = results[1] });
                    }
                }
                Settings = settings;
                MessageBox.Show("Finished loading file!");
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SaveFile()
        {
            List<string> values = new List<string>();
            try
            {
                string filename = null;
                if (Settings.Count() == 0)
                {
                    throw new System.Exception("A required setting is empty. Please load data!");
                }
                foreach (SettingModel.Setting set in Settings)
                {
                    if (set.SettingValue.Equals(""))
                    {
                        throw new System.Exception("All settings must have a value!");
                    }
                    values.Add(set.SettingField.ToString() + ',' + set.SettingValue.ToString());
                }
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "cameraSettings"; // Default
                dlg.Filter = "Database Files (*" + View.WindowView.suffix + ") | *" + View.WindowView.suffix;
                if (dlg.ShowDialog() == true)
                {
                    filename = dlg.FileName;
                }
                File.WriteAllLines(filename, values);
                MessageBox.Show("Finished saving file!");
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }        
    }
}
