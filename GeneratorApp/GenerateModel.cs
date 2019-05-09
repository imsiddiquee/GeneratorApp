using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GeneratorApp
{
    public class GenerateModel
    {
        public void CreateModel(string fileName,string modelName,IEnumerable<ModelProperty> modelProperties)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    MessageBox.Show("Check file already exists");
                    return;
                }

                // Create a new file     
                using (var sw = File.CreateText(fileName))
                {
                    sw.WriteLine("using Abp.Domain.Entities;");
                    sw.WriteLine("using Abp.Domain.Entities.Auditing;");
                    sw.WriteLine("using HRIS.Entities.BaseEntity;");
                    sw.WriteLine("using System.ComponentModel.DataAnnotations;");
                    sw.WriteLine("using System.ComponentModel.DataAnnotations.Schema;");
                    sw.WriteLine("using System.Text;");
                    sw.WriteLine("");
                    sw.WriteLine("namespace HRIS.Entities.Setup");
                    sw.WriteLine("{");
                    sw.WriteLine("");

                    sw.WriteLine("public class " + modelName + " : FullAuditedEntity<int>, IMustHaveTenant, IHasIsActive");
                    sw.WriteLine("{");                    

                    foreach (var item in modelProperties)
                    {
                        sw.WriteLine("public "+item.PropertyType+" "+item.PropertyName+" { get; set; }");
                    }

                    sw.WriteLine("public int TenantId { get; set; }");
                    sw.WriteLine("public bool? IsActive { get; set; }");
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