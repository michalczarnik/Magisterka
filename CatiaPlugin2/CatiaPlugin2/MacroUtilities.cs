using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatiaPlugin2
{
    static class MacroUtilities
    {
        public static List<Macro> GetMacrosInFolder(string path)
        {
            List<Macro> macroList = new List<Macro>();
            var files = Directory.GetFiles(path);
            var filePattern = @"([^\\]*\.CATScript$)";
            var fileRegex = new Regex(filePattern);
            var scriptPattern = @"Sub CATMain\((.*)\)";
            var scripRegex = new Regex(scriptPattern);
            foreach (var file in files)
            {
                Match m = fileRegex.Match(file);
                if (m.Success)
                {
                    Macro macro = new Macro();
                    macro.FileName= m.Value;
                    macro.DirectoryName = path; 
                    var parametersList = new List<Parameter>();
                    using (var sr = new StreamReader(file))
                    {
                        string line;
                        var foundSubmethod = false;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (!foundSubmethod)
                            {
                                var m2 = scripRegex.Match(line);
                                if (m2.Success && m2.Groups.Count > 1 && !string.IsNullOrWhiteSpace(m2.Groups[1].Value))
                                {
                                    var parameters = m2.Groups[1].Value.Split(',');
                                    foreach (var parameter in parameters)
                                    {
                                        var tempPar = parameter.Trim();
                                        var parameterDesc = tempPar.Split(' ');
                                        var p = new Parameter();
                                        p.ParameterName = parameterDesc[0];
                                        p.Type = parameterDesc[2];
                                        parametersList.Add(p);
                                        foundSubmethod = true;
                                    }
                                }
                            }
                            else if (line.StartsWith("'"))
                            {
                                if (line.StartsWith("'Description:"))
                                {
                                    macro.Description = line.Substring(13);
                                }
                                else if (line.StartsWith("'Image;"))
                                {
                                    var obj = line.Split(';');
                                    macro.Images.Add(obj[1], obj[2]);
                                }
                                else
                                {
                                    foreach (var parameter in parametersList)
                                    {
                                        if (line.StartsWith("'" + parameter.ParameterName + ":"))
                                        {
                                            var parameterInfo = line.Substring(parameter.ParameterName.Length + 2).Split(';');
                                            if (parameterInfo.Length > 0)
                                            {
                                                parameter.DisplayName = parameterInfo[0];
                                                if (parameterInfo.Length > 1)
                                                {
                                                    parameter.DefaultValue = parameterInfo[1];
                                                }
                                            }
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    macro.Parameters = parametersList;
                    macroList.Add(macro);
                }
            }
            return macroList;
        }
    }
}