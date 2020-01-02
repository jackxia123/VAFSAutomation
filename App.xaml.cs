using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CANTxGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private bool unsavedData = false;
        public bool UnsavedData
        {
            get { return unsavedData; }
            set { unsavedData = value; }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            UnsavedData = true;
        }
        /// <summary>
        /// When user log off or shutdown PC
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);

            if (UnsavedData)
            {
                e.Cancel = true;
                MessageBox.Show("The application attempted to be closed as a result of " + e.ReasonSessionEnding.ToString() + ". This is not allowed,as you have unsaved data.");
            }
        }
    }
}