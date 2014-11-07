using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.STM;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class PPMMapperExChannerPanel : ValidatePanel, IFRAMUser
    {
        private ushort _MinEAddr = 0;
        private ushort _CenterEAddr = 0;
        private ushort _MaxEAddr = 0;

        private ushort _Channel;
        private int _DataIndex;

        private bool _UseFRAM = false;

        public PPMMapperExChannerPanel()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
        }


        public bool EEPStore {
            get
            {
                return _UseFRAM;
            }
            set
            {
                if(_UseFRAM == value) return;
                cbFRAM.Checked = _UseFRAM = value;
                _MinEAddr = _MaxEAddr = _CenterEAddr = 0;
            } 
        }
        
        public string CName { get { return tbName.Text; }  }

        public ushort Min { get { return ushort.Parse(tbMin.Text); } }

        public ushort Center { get { return ushort.Parse(tbMiddle.Text); } }

        public ushort Max { get { return ushort.Parse(tbMax.Text); } }

        public void Initialized()
        {
            FRAMMapper.Register(this);
        }

        public void Removed()
        {
            FRAMMapper.UnRegisterUser(this);
        }


        #region FRAM

        public ushort MinEAddr { get { return _MinEAddr; } }

        public ushort CenterEAddr { get { return _CenterEAddr; } }

        public ushort MaxEAddr { get { return _MaxEAddr; } }


        public uint[] FRAMAddresses
        {
            get
            {
                return EEPStore ? new[] { (uint)_MinEAddr, _CenterEAddr, _MaxEAddr } : new uint[0];
            }
            set
            {
                if(value == null || value.Length == 0) return;

                _MinEAddr = (ushort) value[0];
                _CenterEAddr = (ushort) value[1];
                _MaxEAddr = (ushort) value[2];
            }
        }

        public string[] Names
        {
            get
            {
                var prefix = tbName.Text;
                if (!prefix.EndsWith("_")) prefix += "_";

                return EEPStore
                    ? new[] {prefix + "MIN", prefix + "CNT", prefix + "MAX"}
                    : new string[0];
            }
        }

        public uint SlotsUsed
        {
            get
            {
                return (uint) (EEPStore ? 3 : 0);
            }
        }

        public int[] DefaultValues
        {
            get
            {
                return EEPStore
                    ? new[] { int.Parse(tbMin.Text), int.Parse(tbMiddle.Text), int.Parse(tbMax.Text)}
                    : new int[0];
            }
        }

        public Guid ID { get; private set; }

        #endregion

        public ushort Channel
        {
            get { return _Channel; }
            set
            {
                _Channel = value;

                lnkIn.Name = "CH_IN_" + (_Channel+1);
                lnkOut.Name = "CH_OUT_" + (_Channel + 1);

                tbName.Text = "CH_" + (_Channel + 1);
            }
        }

        public LinkPoint[] GetLinks()
        {
            return new[] { lnkIn, lnkOut };
        }

        public void UnLink()
        {
            lnkIn.Unlink();
            lnkOut.Unlink();

        }

        public  XElement Serialize()
        {
            return new XElement("Channel",
                    new XAttribute("ID",ID.ToString()),
                    new XAttribute("Number", _Channel+1),
                    new XAttribute("Name", CName),
                    new XAttribute("EEPRom", EEPStore.ToString()),
                    new XAttribute("Min", tbMin.Text),
                    new XAttribute("Center", tbMiddle.Text),
                    new XAttribute("Max", tbMax.Text),
                    new XAttribute("MinAddr", _MinEAddr),
                    new XAttribute("CenterAddr", _CenterEAddr),
                    new XAttribute("MaxAddr", _MaxEAddr),
                    new XAttribute("DataIndex", _DataIndex)
                );
        }

        public void Deserialize(XElement data)
        {
            ID = Guid.Parse(data.AttributeValue("ID"));

            Channel = (ushort) (ushort.Parse(data.AttributeValue("Number")) - 1);
            tbName.Text = data.AttributeValue("Name");
            tbMin.Text = data.AttributeValue("Min");
            tbMiddle.Text = data.AttributeValue("Center");
            tbMax.Text = data.AttributeValue("Max");
            cbFRAM.Checked = _UseFRAM = bool.Parse(data.AttributeValue("EEPRom"));

            _DataIndex = int.Parse(data.AttributeValue("DataIndex"));
            _MinEAddr = ushort.Parse(data.AttributeValue("MinAddr"));
            _CenterEAddr = ushort.Parse(data.AttributeValue("CenterAddr"));
            _MaxEAddr = ushort.Parse(data.AttributeValue("MaxAddr"));
        }

        private void cbFRAM_CheckedChanged(object sender, EventArgs e)
        {
            if (EEPStore != cbFRAM.Checked)
            {
                EEPStore = cbFRAM.Checked;
                FRAMMapper.ReAssign(this);
            }
        }

        public void Check(CodeGeneratorContext context, Guid ParentID)
        {
            if (EEPStore)
            {
                context.RegiterEEPVariable(_MinEAddr, string.Format("{0:N}_{1}_MIN", ParentID, _Channel));
                context.RegiterEEPVariable(_CenterEAddr, string.Format("{0:N}_{1}_CEN", ParentID, _Channel));
                context.RegiterEEPVariable(_MaxEAddr, string.Format("{0:N}_{1}_MAX", ParentID, _Channel));

                if (!ValidateControl(tbName, () => ValidateName(tbName.Text)))
                    throw new CompilationCheckException("Varialble value is not valid", CompilationCheckException.CompileIteration.PreCheck);
            }

            if(!ValidateControl(tbMin, () => ValidateAxis(tbMin.Text)))
                throw new CompilationCheckException("Wrong minimum axis value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbMiddle, () => ValidateAxis(tbMiddle.Text)))
                throw new CompilationCheckException("Wrong center axis value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbMax, () => ValidateAxis(tbMax.Text)))
                throw new CompilationCheckException("Wrong max axis value", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input axis is not linked", CompilationCheckException.CompileIteration.PreCheck);
        }

        public void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += EEPStore ? 4 : 1;
        }

        public void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short)_DataIndex;
        }

        public void TakeData(int[] data)
        {
            if (_DataIndex >= 0)
                lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }


        public void GenerageSMTCode(CodeGeneratorContext context, STMProgram prog, List<STMVariable> varCollector )
        {
            if (EEPStore)
            {
                var names = Names;

                var vmin = prog.Variables.Add((ushort) (_DataIndex+1), _MinEAddr, names[0],
                    context.CurrentModel.Index, 10, 0, 1000, short.Parse(tbMin.Text));

                var vcnt = prog.Variables.Add((ushort) (_DataIndex+2), _CenterEAddr, names[1],
                    context.CurrentModel.Index, 10, 0, 1000, short.Parse(tbMiddle.Text));

                var vmax = prog.Variables.Add((ushort) (_DataIndex+3), _MaxEAddr, names[2],
                    context.CurrentModel.Index, 10, 0, 1000, short.Parse(tbMax.Text));

                varCollector.Add(prog.Variables[vmin]);
                varCollector.Add(prog.Variables[vcnt]);
                varCollector.Add(prog.Variables[vmax]);

                prog.StartupCode.Add(0x0F, vmin, int.Parse(tbMin.Text));
                prog.StartupCode.Add(0x0F, vcnt, int.Parse(tbMiddle.Text));
                prog.StartupCode.Add(0x0F, vmax, int.Parse(tbMax.Text));

                context.ModelCode.Add(0x1F, lnkIn.LinkedTo.DataMapIdx,
                        vmin, vcnt, vmax, _DataIndex);

            }
            else
            {
                context.ModelCode.Add(0x1E,
                        lnkIn.LinkedTo.DataMapIdx,
                        ushort.Parse(tbMin.Text),
                        ushort.Parse(tbMiddle.Text),
                        ushort.Parse(tbMax.Text),
                        _DataIndex
                    );
            }
        }
    }
}
