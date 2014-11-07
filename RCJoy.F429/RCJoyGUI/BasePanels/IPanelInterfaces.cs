using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI
{
    public interface ILinkHolder
    {
        IEnumerable<LinkPoint> GetLinks();
        void Link(ILinkResolver root);
        LinkPoint GetLink(string name);
    }

    public interface ILinkPanel : ILinkHolder, IDetermined
    {
        Guid ID { get; }

        XElement CreatXMLSave();
        void Serialize(XElement data);
        void Deserialize(XElement data);
        bool IsInChain(ILinkPanel panel);
    }

    public interface ICalculationDataViewer
    {
        void LinkIndex(ref int IndexCounter);
        void TakeData(int[] data);
    }

    public interface ILinkResolver
    {
        LinkPoint GetLink(Guid objectID, string LinkName);
        ILinkPanel GetPanel(Guid objectID);
    }
}