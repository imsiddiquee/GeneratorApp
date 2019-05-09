using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GeneratorApp
{
    public class GenerateDto
    {
        public void CreateDto(string fileName,string modelName,List<ModelProperty> modelProperties)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    MessageBox.Show("Check file already exists Dto file.");
                    return;
                }

                // Create a new file     
                using (var sw = File.CreateText(fileName))
                {
                    sw.WriteLine("using Abp.Application.Services.Dto;");
                    sw.WriteLine("using Abp.AutoMapper;");

                    sw.WriteLine("");
                    sw.WriteLine("");

                    sw.WriteLine("namespace HRIS.Setup."+modelName+".Dto");
                    sw.WriteLine("{");
                    sw.WriteLine("[AutoMap(typeof(Entities.Setup."+modelName+"))]");
                    sw.WriteLine("public class " + modelName + "Dto : EntityDto");
                    sw.WriteLine("{");
                    foreach (var item in modelProperties)
                    {
                        var propertyFormat = $"public {item.PropertyType} {item.PropertyName}";
                        sw.WriteLine(propertyFormat + "{ get; set; }");
                    }

                    sw.WriteLine("public bool? IsActive{ get; set; }");
                    sw.WriteLine("public bool? IsDefault{ get; set; }");

                    sw.WriteLine("public " + modelName + "Dto");
                    sw.WriteLine("{");
                    sw.WriteLine("IsActive = false");
                    sw.WriteLine("IsDefault = false");
                    sw.WriteLine("}");

                    sw.WriteLine("}");
                    sw.WriteLine("}");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}