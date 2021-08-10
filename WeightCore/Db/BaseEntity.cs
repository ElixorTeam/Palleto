﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Xml.Serialization;

namespace WeightCore.Db
{
    [Serializable]
    public class BaseEntity<T>
    {
        #region Public and private fields and properties

        #endregion

        #region Constructor and destructor

        public BaseEntity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        #endregion
    }
}