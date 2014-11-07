using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.JoyDialog
{
    public partial class BaseControlPanel : ValidatePanel
    {
        public BaseControlPanel()
        {
            InitializeComponent();
        }

        public event EventHandler RemoveClicked;

        public event EventHandler UpClicked;
        public event EventHandler DownClicked;


        protected virtual void OnUpClicked()
        {
            EventHandler handler = UpClicked;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnDownClicked()
        {
            EventHandler handler = DownClicked;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnRemoveClicked()
        {
            EventHandler handler = RemoveClicked;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnRemoveClicked();

        }

        private void linkUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnUpClicked();
        }

        private void linkDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnDownClicked();
        }

        public virtual bool Check()
        {
            throw new NotImplementedException();
        }

        public virtual void FillJoyInfo(JoystickConfig joy)
        {
            throw new NotImplementedException();
        }

        public virtual void FillFromJoyInfo(IJoystickControl control)
        {
            throw new NotImplementedException();
        }

      
    }
}
