using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Tahorg.RCJoyGUI
{
    public enum enLinkDirection
    {
        Input,
        Output
    }

    public enum enLinkType
    {
        Axle,
        Button,
        Value
    }

    public class LinkSelectArgumetns : EventArgs
    {
        public LinkPoint Link { get; private set; }
        public bool ToState { get; private set; }

        public LinkSelectArgumetns(LinkPoint link, bool toState)
        {
            Link = link;
            ToState = toState;
        }
    }

    public partial class LinkPoint : Control
    {
        private enLinkDirection _direction;
        private enLinkType _linkType;
        private bool _isSelected;
        private LinkPoint _linkedTo;

        private static readonly Brush AxleBrush = Brushes.Blue;
        private static readonly Brush ButtonBrush = Brushes.Red;
        private static readonly Brush ValueBrush = Brushes.CadetBlue;

        private static readonly Pen AxlePen = new Pen(AxleBrush, 2f);
        private static readonly Pen ButtonPen = new Pen(ButtonBrush, 2f);
        private static readonly Pen ValuePen = new Pen(ValueBrush, 2f);

        private static readonly Pen AxleSelectedPen = new Pen(Color.CornflowerBlue, 3f);
        private static readonly Pen ButtonSelectedPen = new Pen(Color.Pink, 3f);
        private static readonly Pen ValueSelectedPen = new Pen(Color.Cyan, 3f);

        private Brush CurrentBrush = AxleBrush;
        private Pen CurrentPen = AxlePen;
        private Pen SelectedPen = AxleSelectedPen;

        private Guid __LinkedToID;
        private string __LinkedTOLinkName;

        public enLinkDirection Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
                Invalidate();
            }
        }

        public enLinkType LinkType
        {
            get { return _linkType; }
            set
            {
                _linkType = value; Invalidate();

                CurrentBrush = _linkType == enLinkType.Axle ? AxleBrush : _linkType == enLinkType.Button ? ButtonBrush : ValueBrush;
                CurrentPen = _linkType == enLinkType.Axle ? AxlePen : _linkType == enLinkType.Button ? ButtonPen : ValuePen;
                SelectedPen = _linkType == enLinkType.Axle ? AxleSelectedPen : _linkType == enLinkType.Button ? ButtonSelectedPen : ValueSelectedPen;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)] 
        public string MappedValueName { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; Invalidate(); OnSelect(value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ILinkPanel HolderPanel { get; set; }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public LinkPoint LinkedTo
        {
            get { return _linkedTo; }
        }

        public short DataMapIdx
        {
            get; set; 
        }


        public event EventHandler<LinkSelectArgumetns> Selected;
        public event EventHandler LinkedToChanged;

        public LinkPoint()
        {
            InitializeComponent();
            Size = new Size(12, 12);
        }

        public void LinkTo(LinkPoint link)
        {
            if(Direction == enLinkDirection.Output) return;

            if (_linkedTo != null)
                _linkedTo.SetLink(null);

            SetLink(link);
            _linkedTo.SetLink(this);
        }

        public void Unlink()
        {
            if (_linkedTo != null)
                _linkedTo.SetLink(null);

            SetLink(null);
        }

        private void SetLink(LinkPoint link)
        {
            _linkedTo = link;

            if (_linkedTo != null)
            {
                __LinkedToID = _linkedTo.HolderPanel.ID;
                __LinkedTOLinkName = _linkedTo.Name;
            }
            else
            {
                __LinkedToID = Guid.Empty;
                __LinkedTOLinkName = null;
            }

            OnLinkedToChanged();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            //pe.Graphics.Clear(Color.Transparent);

            var rect = new Rectangle(0, 0, Size.Width - 1, Size.Height - 1);

            if (_direction == enLinkDirection.Input)
                pe.Graphics.DrawArc(SystemPens.WindowText, rect, 270, 180);
            else
                pe.Graphics.DrawArc(SystemPens.WindowText, rect, 85, 190);

            if (IsSelected)
            {
                //rect = new Rectangle(0, 0, Size.Width - 1, Size.Height - 1);
                pe.Graphics.FillEllipse(Brushes.Black, rect);
            }

            rect = new Rectangle(2, 2, Size.Width - 5, Size.Height - 5);
            pe.Graphics.FillEllipse(CurrentBrush, rect);
        }

        protected virtual void OnSelect(bool selected)
        {
            if (Selected != null) Selected(this, new LinkSelectArgumetns(this, selected));
        }

        protected virtual void OnLinkedToChanged()
        {
            EventHandler handler = LinkedToChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            IsSelected = !IsSelected;
        }

        public void DrawLink(Graphics canvas, Control coordBase)
        {
            if (_linkedTo == null) return;

//            var vector = new VectorF
//            if(
//                
//                coordBase.PointToClient(PointToScreen(new Point(0,0)) )

            var pts = new[]
            {
                coordBase.PointToClient(PointToScreen(new Point(0, 5))),
                coordBase.PointToClient(PointToScreen(new Point(-80, 5))),
                coordBase.PointToClient(_linkedTo.PointToScreen(new Point(80, 5))),
                coordBase.PointToClient(_linkedTo.PointToScreen(new Point(10, 5)))
            };

            canvas.DrawBeziers(_isSelected ? SelectedPen : CurrentPen, pts);
        }


        #region Serializaion

        public XElement Serialize()
        {
            
            var res = new XElement("Link",new XAttribute("Name", Name));

            if (LinkedTo != null)
            {
                res.Add(
                    new XAttribute("LinkedToID", LinkedTo.HolderPanel.ID),
                    new XAttribute("LinkedToName", LinkedTo.Name)
                );
            }

            return res;
        }

        public void Deserialize(XElement elem)
        {
            __LinkedToID = new Guid(elem.AttributeValue("LinkedToID") ?? Guid.Empty.ToString());
            __LinkedTOLinkName = elem.AttributeValue("LinkedToName");
        }

        public void Link(ILinkResolver root)
        {
            if(__LinkedToID == Guid.Empty) return;

            var lnk = root.GetLink(__LinkedToID, __LinkedTOLinkName);
            LinkTo(lnk);
        }

        #endregion

    }
}
