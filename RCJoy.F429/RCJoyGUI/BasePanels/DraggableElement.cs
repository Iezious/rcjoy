using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI
{
    public partial class DraggableElement : ValidatePanel, ILinkPanel, ICalculationDataViewer, ICodeGenerator
    {
        private bool _selected;

        public bool IsSelected
        {
            get { return _selected; }

            set
            {
                _selected = value;

                if (_selected)
                {
                    labelHead.BackColor = SystemColors.GradientActiveCaption;
                    labelHead.Font = new Font(labelHead.Font, FontStyle.Bold);

                    OnSelected();
                }
                else
                {
                    labelHead.BackColor = SystemColors.GradientInactiveCaption;
                    labelHead.Font = new Font(labelHead.Font, FontStyle.Regular);
                    labelHead.DeselectAll();
                }
            }
        }

        public string Title
        {
            get { return labelHead.Text; }
            set { labelHead.Text = value; }
        }

        public Guid ID { get; protected set; }
        public string  MappedID { get; protected set; }

        private readonly List<LinkPoint> __Links = new List<LinkPoint>(16);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<LinkPoint> Links { get { return GetLinks(); } }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<LinkPoint> InputLinks { get { return GetLinks().Where(lnk => lnk.Direction == enLinkDirection.Input); } }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<LinkPoint> OutLinks { get { return GetLinks().Where(lnk => lnk.Direction == enLinkDirection.Output); } }


        public event EventHandler Selected;
        public event EventHandler<LinkSelectArgumetns> LinkSelected;

        public DraggableElement()
        {
            InitializeComponent();

            ID = Guid.NewGuid();
        }

        public virtual void Removed()
        {
            
        }

        public virtual void Initialized()
        {

        }


        protected virtual void OnSelected()
        {
            if (Selected != null) Selected(this, EventArgs.Empty);
        }

        protected void RegiseterLink(params LinkPoint[] links)
        {
            foreach (var link in links)
            {
                __Links.Add(link);
                link.HolderPanel = this;
                link.Selected += OnLinkSelected;
            }
        }

        protected void UnRegiseterLink(params LinkPoint[] links)
        {
            foreach (var link in links)
            {
                __Links.Remove(link);
            }
        }

        private void OnLinkSelected(object sender, LinkSelectArgumetns args)
        {
            if (LinkSelected != null) LinkSelected(this, args);
        }

        private void labelHead_Click(object sender, EventArgs e)
        {
            IsSelected = true;
        }

        private Point lastDragPos;
        private bool inDrag = false;

        private void labelHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            IsSelected = true;

            lastDragPos = MousePosition;
            inDrag = true;
        }

        private void labelHead_MouseUp(object sender, MouseEventArgs e)
        {
            labelHead.Capture = false;
            inDrag = false;
        }

        private void labelHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (!inDrag) return;

            var pos = MousePosition;

            var dx = pos.X - lastDragPos.X;
            var dy = pos.Y - lastDragPos.Y;

            lastDragPos = pos;

            if (Location.X + dx < 0) dx = 0;
            if (Location.Y + dy < 0) dy = 0;

            Location = Location + new Size(dx, dy);
        }

        public void DrawLinks(Graphics canvas, Control coordBase)
        {
            foreach (var inputLink in InputLinks)
            {
                inputLink.DrawLink(canvas, coordBase);
            }
        }

        public bool IsInChain(ILinkPanel panel)
        {
            return
                panel == this ||
                OutLinks.Any(lnk => lnk.LinkedTo != null && lnk.LinkedTo.HolderPanel.IsInChain(panel));
        }

        public void Unlink()
        {
            foreach (var link in GetLinks()) link.Unlink();
        }

        public virtual XElement CreatXMLSave()
        {
            throw new NotImplementedException();
        }

        public virtual void Serialize(XElement data)
        {
            data.Add(
                new XAttribute("ID", ID.ToString()),
                new XAttribute("Top", Top),
                new XAttribute("Left", Left),
                new XAttribute("Title", Title),
                SerializeLinks()
            );
        }

        public virtual void Deserialize(XElement data)
        {
            ID = Guid.Parse(data.AttributeValue("ID") ?? Guid.NewGuid().ToString());
            Top = int.Parse(data.AttributeValue("Top") ?? "120");
            Left = int.Parse(data.AttributeValue("Left") ?? "120");
            Title = data.AttributeValue("Title") ?? Title;

            DeserializeLinks(data);

        }

        public virtual void TakeData(int[] data)
        {
            
        }

        public virtual void LinkIndex(ref int Counter)
        {
            throw new NotImplementedException();
        }

        public virtual void PreGenerate()
        {
            Determined = false;
        }

        public virtual void Check(CodeGeneratorContext context)
        {
            throw new NotImplementedException();
        }

        public virtual void MapVariables(CodeGeneratorContext context)
        {
            MappedID = context.CurrentModel.CName + "_" + GetType().Name.ToUpper().Replace("DesignPanel", "") + "_" + ID.ToString("N");
            var basename = MappedID + "_";

            foreach (LinkPoint link in GetLinks())
            {
                link.MappedValueName = basename + link.Name.ToUpper();
            }
        }

        #region Code generator members
        
        public bool Determined { get; set; }
        public DesignPanel ModelPanel { get; set; }

        public virtual bool CheckDetermined()
        {
            return Determined = (InputLinks.All(link => link.LinkedTo == null || link.LinkedTo.HolderPanel.Determined));
        }

        public virtual void GenerateJoyFile(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            
        }

        public virtual void GenerateInit(CodeGeneratorContext context, TextWriter __CPP)
        {
            
        }

        public virtual void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel));
        }

        public virtual void GenerateJoystick(CodeGeneratorContext context, STMProgram prog)
        {
            
        }

        public virtual void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
//            throw new NotImplementedException();
        }

        public virtual void GeneratePreCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
//            throw new NotImplementedException();
        }

        public virtual void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
//            throw new NotImplementedException();
        }

        #endregion

        public virtual IEnumerable<LinkPoint> GetLinks()
        {
            return __Links;
        }   

        public virtual void Link(ILinkResolver root)
        {
            foreach (var link in GetLinks().Where(lnk => lnk.Direction == enLinkDirection.Input))
                link.Link(root);
        }

        public virtual LinkPoint GetLink(string name)
        {
            return GetLinks().FirstOrDefault(lnk => lnk.Name == name);
        }

        protected void SaveLinks(XElement elem)
        {
            elem.Add(GetLinks()
                .Where(lnk => lnk.Direction == enLinkDirection.Input && lnk.LinkedTo != null)
                .Select(lnk => lnk.Serialize()));
        }

        private void DraggableElement_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 5, labelHead.Top + labelHead.Height, 5, Height);
            e.Graphics.DrawLine(Pens.Black, Width - 4, labelHead.Top + labelHead.Height, Width - 4, Height);
            e.Graphics.DrawLine(Pens.Black, 5, Height - 1, Width - 4, Height - 1);
        }

        public virtual void ModelsUpdated()
        {
            
        }

        public virtual void ModesUpdated()
        {
            
        }

        protected XElement SerializeLinks()
        {
            return new XElement("Inputs", GetLinks()
                    .Where(point => point.Direction == enLinkDirection.Input)
                    .Select(lnk => lnk.Serialize())
                );
        }

        protected void DeserializeLinks(XElement data)
        {
            var xinputs = data.Element("Inputs");
            if (xinputs == null) return;

            foreach (XElement xlink in xinputs.Elements())
            {
                var name = xlink.AttributeValue("Name");
                var lnk = GetLinks().FirstOrDefault(point => point.Direction == enLinkDirection.Input && point.Name == name);

                if (lnk != null) lnk.Deserialize(xlink);
            }
        }


        public virtual void TakeJoyData(byte[] jdata)
        {
            
        }

        public virtual void TakeDataPPM(int[] intdata)
        {
            
        }

        public virtual bool CheckJoystickInUse(JoystickConfig joystickConfig)
        {
            return false;
        }
    }
}
