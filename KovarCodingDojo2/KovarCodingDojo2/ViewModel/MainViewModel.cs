using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.BaseModels;
using Shared.Interfaces;
using Simulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace KovarCodingDojo2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<DataGridViewModel> modelItems = new List<DataGridViewModel>();
        public ObservableCollection<DataGridViewModel> SensorList { get; set; }
        public ObservableCollection<DataGridViewModel> ActorList { get; set; }
        public RelayCommand SensorAddBtnClickCmd { get; set; }
        public RelayCommand SensorDelBtnCmd { get; set; }
        public RelayCommand ActuatorAddBtnClickCmd { get; set; }
        public RelayCommand ActuatorDelBtnClickCmd { get; set; }
        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();

        public ObservableCollection<string> ModeSelectionList { get; private set; }
        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            SensorList = new ObservableCollection<DataGridViewModel>();
            ActorList = new ObservableCollection<DataGridViewModel>();
            ModeSelectionList = new ObservableCollection<string>();

            //Fill ModeSelectionList
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelectionList.Add(item);
            }

            //for time /date update
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 40);
            timer.Tick += UpdateTime;

            if (!IsInDesignMode)
            {
                //load Data
                LoadData();

                //start timer for date/time update
                timer.Start();
            }

        }

        private void LoadData()
        {
            Simulator sim = new Simulator(modelItems);
            foreach (var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }

        }



        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }

    }
}