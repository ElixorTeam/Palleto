﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class HostDirect : BaseSerializeEntity<HostDirect>
    {
        #region Public and private fields and properties

        public int Id { get; set; }
        public int ScaleId { get; set; }
        public string? Name { get; set; }
        public string? Ip { get; set; }
        public string? Mac { get; set; }
        public Guid IdRRef { get; set; }
        public bool Marked { get; set; }
        [XmlIgnore]
        public XDocument? SettingsFile { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}." +
                $"{nameof(Ip)}: {Ip}." +
                $"{nameof(Mac)}: {Mac}.";
        }

        #endregion
    }
}
