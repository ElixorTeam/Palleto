﻿using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Ws.Database.Core.Entities.Scales.TemplatesResources;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Common;

public static partial class LabelGenerator
{
    [GeneratedRegex(@"\[(?!@)([^[\]]+)]")]
    private static partial Regex RegexOfResources();

    [GeneratedRegex(@"\^FH\^FD\s*(.*?)\s*\^FS", RegexOptions.Singleline)]
    private static partial Regex RegexOfTextBlocks();

    public static LabelReadyDto GetZpl<TItem>(string template, PluEntity plu, TItem labelModel) where TItem :
        XmlLabelBaseModel, ISerializable
    {
        labelModel.PluFullName = labelModel.PluFullName.Replace("|", "");

        XmlDocument xmlLabelContext = XmlUtil.SerializeAsXmlDocument(labelModel);
        template = template.Replace("Context", labelModel.GetType().Name);

        string zpl = XmlUtil.XsltTransformation(template, xmlLabelContext.OuterXml);

        zpl = PrintCmdReplaceZplResources(zpl, plu);
        zpl = ReplaceValuesWithHex(zpl);

        return new(zpl, labelModel);
    }

    private static string PrintCmdReplaceZplResources(string zpl, PluEntity plu)
    {
        if (string.IsNullOrEmpty(zpl))
            throw new ArgumentException("Value must be fill!", nameof(zpl));

        MatchCollection matches = RegexOfResources().Matches(zpl);
        foreach (Match match in matches)
        {
            string word = match.Value;
            string replacement = new SqlTemplateResourceRepository().GetByName(word.Trim('[', ']')).Data.ValueUnicode;
            if (string.IsNullOrEmpty(replacement)) continue;
            zpl = zpl.Replace(word, replacement);
        }

        zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", plu.StorageMethod.Zpl, StringComparison.OrdinalIgnoreCase);

        return zpl;
    }

    private static string ReplaceValuesWithHex(string input)
    {
        return RegexOfTextBlocks().Replace(input, match => {
            string text = match.Groups[1].Value;
            string hexText = ConvertStringToHex(text);
            return $"\n\n^FH^FD\n{hexText}\n^FS\n\n";
        });
    }

    private static string ConvertStringToHex(string text)
    {
        StringBuilder zplBuilder = new();
        foreach (char i in text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(new[] { i });
            IEnumerable<string> hexBytes = bytes.Select(b => $"_{b:X2}");
            foreach (var hexByte in hexBytes)
                zplBuilder.Append(hexByte);
        }
        return zplBuilder.ToString();
    }
}