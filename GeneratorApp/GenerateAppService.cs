using System;
using System.IO;
using System.Windows.Forms;

namespace GeneratorApp
{
    public class GenerateAppService
    {
        public void CreateAppService(string fileName,string modelName)
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
                    sw.WriteLine("using Abp.Domain.Repositories;");
                    sw.WriteLine("using Abp.ObjectMapping;");
                    sw.WriteLine("using HRIS.Setup.Country.Dto;");
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("using System.Linq;");
                    sw.WriteLine("");
                    sw.WriteLine("namespace HRIS.Setup."+modelName+"");
                    sw.WriteLine("{");
                    sw.WriteLine("public class "+ modelName + "AppService : ApplicationService, I"+ modelName + "AppService");
                    sw.WriteLine("{");
                    sw.WriteLine("private readonly IObjectMapper _objectMapper;");
                    sw.WriteLine("private  readonly IRepository<Entities.Setup."+ modelName + "> _" + modelName.ToLower() + "Repository;");
                    sw.WriteLine("");
                    sw.WriteLine("public " + modelName + "AppService(IObjectMapper objectMapper,IRepository<Entities.Setup." + modelName + "> " + modelName.ToLower() + "Repository)");
                    sw.WriteLine("{");
                    sw.WriteLine("_objectMapper = objectMapper;");
                    sw.WriteLine("_" + modelName.ToLower() + "Repository = " + modelName.ToLower() + "Repository;");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("public " + modelName + "Dto Create(" + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var " + modelName.ToLower() + "Obj = _objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + ");");
                    sw.WriteLine("" + modelName.ToLower() + "Obj.TenantId = 4;");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(_" + modelName.ToLower() + "Repository.Insert(" + modelName.ToLower() + "Obj));");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("public " + modelName + "Dto GetById(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _" + modelName.ToLower() + "Repository");
                    sw.WriteLine(".GetAllList()");
                    sw.WriteLine(".FirstOrDefault(w => w.Id == id && w.IsActive == true && w.IsDeleted == false);");
                    sw.WriteLine("");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(result);");
                    sw.WriteLine("}");

                    sw.WriteLine("public List<" + modelName + "Dto> GetAll()");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _" + modelName.ToLower() + "Repository");
                    sw.WriteLine(".GetAll()");
                    sw.WriteLine(".Where(w => w.IsActive == true && w.IsDeleted == false).ToList();");
                    sw.WriteLine("");
                    sw.WriteLine("return _objectMapper.Map<List<" + modelName + "Dto>>(result);");
                    sw.WriteLine("}");

                    sw.WriteLine("public " + modelName + "Dto Update(" + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + ");");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(_" + modelName.ToLower() + "Repository.Update(result));");
                    sw.WriteLine("}");

                    sw.WriteLine("public void Delete(" + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("_" + modelName.ToLower() + "Repository.Delete(_objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + "));");
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