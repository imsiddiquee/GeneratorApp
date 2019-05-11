using System;
using System.IO;
using System.Windows.Forms;

namespace GeneratorApp
{
    public class GenerateIAppService
    {
        public void CreateIAppService(string fileName,string modelName)
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
                    sw.WriteLine("using Abp.Application.Services;");
                    //sw.WriteLine("using HRIS.Setup.Country.Dto;");
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("namespace HRIS.Setup."+modelName+"");
                    sw.WriteLine("{");

                    sw.WriteLine("[RemoteService(false)]");
                    sw.WriteLine("public interface I"+modelName+"AppService : IApplicationService");
                    sw.WriteLine("{");

                    sw.WriteLine("Task<"+modelName+"Dto> GetById(int id);");
                    sw.WriteLine("Task<IEnumerable<" + modelName+ "Dto>> GetAll(IQueryObject queryObject);");
                    sw.WriteLine("Task<"+modelName+"Dto> Create("+modelName+"Dto "+modelName.ToLower()+");");
                    sw.WriteLine("Task<"+modelName+"Dto> Update("+modelName+"Dto "+modelName.ToLower()+");");
                    //sw.WriteLine("void Delete("+modelName+"Dto "+ modelName + ");");
                    sw.WriteLine("Task DeleteById(int id);");

                    sw.WriteLine("}"); //end interface

                    sw.WriteLine("}"); //end namespace

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}