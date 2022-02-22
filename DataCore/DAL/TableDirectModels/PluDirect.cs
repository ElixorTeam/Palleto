﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class PluDirect : BaseSerializeEntity<PluDirect>
    {
        #region Public and private fields and properties

        public ScaleDirect Scale { get; set; } = new ScaleDirect();
        public long Id { get; set; } = default;
        public int PLU { get; set; } = default;
        public string RRefGoods { get; set; } = string.Empty;
        public string GoodsName { get; set; } = string.Empty;
        public string GoodsDescription { get; set; } = string.Empty;
        public string GoodsFullName { get; set; } = string.Empty;
        public string GTIN { get; set; } = string.Empty;
        public string EAN13 { get; set; } = string.Empty;
        public string ITF14 { get; set; } = string.Empty;
        public int? GoodsShelfLifeDays { get; set; }
        public decimal GoodsTareWeight { get; set; }
        public decimal GoodsFixWeight { get; set; }
        public int? GoodsBoxQuantly { get; set; }
        /// <summary>
        /// Верхнее значение веса короба.
        /// </summary>
        public decimal UpperWeightThreshold { get; set; }
        /// <summary>
        /// Номинальный вес короба.
        /// </summary>
        public decimal NominalWeight { get; set; }
        /// <summary>
        /// Нижнее значение веса короба.
        /// </summary>
        public decimal LowerWeightThreshold { get; set; }
        /// <summary>
        /// Весовая продукция.
        /// </summary>
        public bool? CheckWeight { get; set; }
        /// <summary>
        /// ID шаблона.
        /// </summary>
        public long? TemplateID { get; set; }
        //  [XmlIgnoreAttribute]
        public TemplateDirect Template { get; set; }
        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Constructor and destructor

        public PluDirect()
        {
            Load();
        }

        public PluDirect(ScaleDirect scale, int plu)
        {
            Scale = scale;
            PLU = plu;
            Load();
        }

        #endregion

        #region Public and private methods

        public override bool Equals(object obj)
        {
            if (obj is not PluDirect item)
            {
                return false;
            }

            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public XDocument GetBtXmlNamedSubString()
        {
            IDictionary<string, object> dict = ObjectToDictionary(this);

            XDocument doc = new(
                new XElement("XMLScript", new XAttribute("Version", "2.0"),
                new XElement("Command",
                new XElement("Print",
                    new XElement("Format", new XAttribute("TemplateID", TemplateID)),

                    dict.Select(x => new XElement("NameSubString",
                        new XAttribute("Key", x.Key),
                        new XElement("Value", x.Value)))

                ))));

            return doc;
        }

        public IDictionary<string, object> ObjectToDictionary<T>(T item)
            where T : class
        {
            Type myObjectType = item.GetType();
            IDictionary<string, object> dict = new Dictionary<string, object>();
            object[] indexer = new object[0];
            PropertyInfo[] properties = myObjectType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                object value = info.GetValue(item, indexer);
                dict.Add(info.Name, value);
            }
            return dict;
        }

        public T ObjectFromDictionary<T>(IDictionary<string, object> dict)
            where T : class
        {
            Type type = typeof(T);
            T result = (T)Activator.CreateInstance(type);
            foreach (KeyValuePair<string, object> item in dict)
            {
                type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"({PLU})");
            sb.Append($" {GoodsName}");
            //sb.Append($"GoodsDescription : {this.GoodsDescription};\n");
            //sb.Append($"GoodsFullName : {this.GoodsFullName};\n");
            //sb.Append($"GTIN : {this.GTIN};\n");
            //sb.Append($"GLN : {this.GLN};\n");
            //sb.Append($"GoodsShelfLifeDays : {this.GoodsShelfLifeDays};\n");
            //sb.Append($"GoodsTareWeight : {this.GoodsTareWeight};\n");
            //sb.Append($"GoodsBoxQuantly : {this.GoodsBoxQuantly};\n");
            //sb.Append($"TemplateName : {this.TemplateID};\n");
            return sb.ToString();
        }

        public void LoadTemplate()
        {
            if (TemplateID != null)
            {
                Template = new TemplateDirect((long)TemplateID);
            }
            else
            {
                //Scale.Load();
                Template = new TemplateDirect(Scale.TemplateIdDefault);
            }

        }

        public SqlCommand GetLoadCmd(SqlConnection con)
        {
            if (con == null || Scale == null)
                return null;
            string query = @"
select
	 [Id]
	,[GoodsName]
	,[GoodsFullName]
	,[GoodsDescription]
	,[TemplateID]
	,[GTIN]
	,[EAN13]
	,[ITF14]
	,[GoodsShelfLifeDays]
	,[GoodsTareWeight]
	,[GoodsBoxQuantly]
	,[RRefGoods]
	,[PLU]
	,[UpperWeightThreshold]
	,[NominalWeight]			
	,[LowerWeightThreshold]
	,[CheckWeight]

from [db_scales].[GetPLUByID] (@ScaleID, @PLU)
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            SqlCommand cmd = new(query, con);
            cmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.BigInt) { Value = Scale.Id });
            cmd.Parameters.Add(new SqlParameter("@PLU", SqlDbType.Int) { Value = PLU });
            cmd.Prepare();
            return cmd;
        }

        public void Load()
        {
            if (Id == default) return;
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            SqlCommand cmd = GetLoadCmd(con);
            if (cmd != null)
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id");
                        GoodsName = SqlConnect.GetValueAsString(reader, "GoodsName");
                        GoodsFullName = SqlConnect.GetValueAsString(reader, "GoodsFullName");
                        GoodsDescription = SqlConnect.GetValueAsString(reader, "GoodsDescription");
                        TemplateID = SqlConnect.GetValueAsNotNullable<long>(reader, "TemplateID");
                        GTIN = SqlConnect.GetValueAsString(reader, "GTIN");
                        EAN13 = SqlConnect.GetValueAsString(reader, "EAN13");
                        ITF14 = SqlConnect.GetValueAsString(reader, "ITF14");
                        GoodsShelfLifeDays = SqlConnect.GetValueAsNotNullable<byte>(reader, "GoodsShelfLifeDays");
                        GoodsTareWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "GoodsTareWeight");
                        GoodsBoxQuantly = SqlConnect.GetValueAsNotNullable<int>(reader, "GoodsBoxQuantly");
                        //RRefGoods = SqlConnect.GetValueAsString(reader, "RRefGoods");
                        //PLU = SqlConnect.GetValueAsNotNullable<int>(reader, "PLU");
                        UpperWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "UpperWeightThreshold");
                        NominalWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "NominalWeight");
                        LowerWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "LowerWeightThreshold");
                        CheckWeight = SqlConnect.GetValueAsNotNullable<bool>(reader, "CheckWeight");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public SqlCommand GetPluListCmd(SqlConnection con, long scaleId)
        {
            if (con == null)
                return null;
            string query = @"
select
     [Id]
    ,[GoodsName]
    ,[GoodsFullName]
    ,[GoodsDescription]
    ,[TemplateID]
    ,[GTIN]
    ,[EAN13]
    ,[ITF14]
    ,[GoodsShelfLifeDays]
    ,[GoodsTareWeight]
    ,[GoodsBoxQuantly]
    ,[RRefGoods]
	,[PLU]
	,[UpperWeightThreshold]
	,[NominalWeight]			
	,[LowerWeightThreshold]
	,[CheckWeight]
from [db_scales].[GetPLU] (@ScaleID)
order by [PLU]
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            SqlCommand cmd = new(query, con);
            cmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.BigInt) { Value = scaleId });
            cmd.Prepare();
            return cmd;
        }

        public List<PluDirect> GetPluList(ScaleDirect scale)
        {
            List<PluDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                if (scale != null)
                {
                    SqlCommand cmd = GetPluListCmd(con, scale.Id);
                    if (cmd != null)
                    {
                        using SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PluDirect pluEntity = new()
                                {
                                    Scale = scale,
                                    //ScaleId = SqlConnect.GetValueAsString(reader, "GoodsName");
                                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
                                    GoodsName = SqlConnect.GetValueAsString(reader, "GoodsName"),
                                    GoodsFullName = SqlConnect.GetValueAsString(reader, "GoodsFullName"),
                                    GoodsDescription = SqlConnect.GetValueAsString(reader, "GoodsDescription"),
                                    TemplateID = SqlConnect.GetValueAsNotNullable<long>(reader, "TemplateID"),
                                    GTIN = SqlConnect.GetValueAsString(reader, "GTIN"),
                                    EAN13 = SqlConnect.GetValueAsString(reader, "EAN13"),
                                    ITF14 = SqlConnect.GetValueAsString(reader, "ITF14"),
                                    GoodsShelfLifeDays = SqlConnect.GetValueAsNotNullable<byte>(reader, "GoodsShelfLifeDays"),
                                    GoodsTareWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "GoodsTareWeight"),
                                    GoodsBoxQuantly = SqlConnect.GetValueAsNotNullable<int>(reader, "GoodsBoxQuantly"),
                                    //RRefGoods = SqlConnect.GetValueAsString(reader, "RRefGoods"),
                                    PLU = SqlConnect.GetValueAsNotNullable<int>(reader, "PLU"),
                                    UpperWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "UpperWeightThreshold"),
                                    NominalWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "NominalWeight"),
                                    LowerWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "LowerWeightThreshold"),
                                    CheckWeight = SqlConnect.GetValueAsNotNullable<bool>(reader, "CheckWeight")
                                };
                                result.Add(pluEntity);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}
