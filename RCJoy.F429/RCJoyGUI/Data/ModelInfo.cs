using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Tahorg.RCJoyGUI.Data
{
    public interface IDetermined
    {
        bool Determined { get; set; }
    }

    public interface ICodeGenerator : IDetermined
    {
        void GenerateJoyFile(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP);
        void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP);
        void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP);
        void GeneratePreCalculator(CodeGeneratorContext context, TextWriter __CPP);
        void GenerateInit(CodeGeneratorContext context, TextWriter __CPP);

#if STM32
        void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog);
#endif
        void GenerateJoystick(CodeGeneratorContext context, STMProgram prog);
    }

    public class ModelInfo
    {
        private int _channels;
        private int _ppmMin;
        private int _ppmCenter;
        private int _ppmMax;
        private bool _isMenu = false;
        
        public string Name { get; set; }
        public string CName { get; set; }

        public int Channels { get { return _channels; } set { _channels = value; } }
        public int PPMMin { get { return _ppmMin; } set { _ppmMin = value; } }
        public int PPMCenter { get { return _ppmCenter; } set { _ppmCenter = value; } }
        public int PPMMax { get { return _ppmMax; } set { _ppmMax = value; } }

        public bool IsMenu { get { return _isMenu; } set { _isMenu = value; } }

        private readonly List<ICodeGenerator> __Generators = new List<ICodeGenerator>(32);
        public IList<ICodeGenerator> Generators { get { return __Generators; } }
        public ushort Index { get; set; }

        public int GeneratorChannels
        {
            get
            {
                return Channels <= 8 ? 8 : Channels <= 10 ? 10 : Channels;
            }
        }

        public ModelInfo()
        {

        }

        public ModelInfo(XElement data)
        {
            Name = data.Attribute("Name").Value;
            CName = data.Attribute("CName").Value;

            int.TryParse(data.AttributeValue("Channels"), out _channels);
            int.TryParse(data.AttributeValue("PPMMin"), out _ppmMin);
            int.TryParse(data.AttributeValue("PPMCenter"), out _ppmCenter);
            int.TryParse(data.AttributeValue("PPMMax"), out _ppmMax);
            bool.TryParse(data.AttributeValue("IsMenu"), out _isMenu);
        }

        public XElement Seriliaze()
        {
            return new XElement("Model",

                                new XAttribute("Name", Name),
                                new XAttribute("CName", CName),
                                new XAttribute("Channels", Channels.ToString(CultureInfo.InvariantCulture)),
                                new XAttribute("PPMMin", PPMMin.ToString(CultureInfo.InvariantCulture)),
                                new XAttribute("PPMCenter", PPMCenter.ToString(CultureInfo.InvariantCulture)),
                                new XAttribute("PPMMax", PPMMax.ToString(CultureInfo.InvariantCulture)),
                                new XAttribute("IsMenu", _isMenu.ToString())
                );
        }
    }

}
