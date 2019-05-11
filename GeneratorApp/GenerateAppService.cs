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

                    sw.WriteLine("[RemoteService(false)]");
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

                    sw.WriteLine("public async Task<" + modelName + "Dto> Create(" + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var " + modelName.ToLower() + "Obj = _objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + ");");
                    sw.WriteLine("" + modelName.ToLower() + "Obj.TenantId = 2;");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(await _" + modelName.ToLower() + "Repository.Insert(" + modelName.ToLower() + "Obj));");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public async Task<" + modelName + "Dto> GetById(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = await _" + modelName.ToLower() + "Repository");
                    //sw.WriteLine(".GetAllList()");
                    sw.WriteLine(".FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);");
                    sw.WriteLine("");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(result);");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public async Task<IEnumerable<" + modelName + "Dto>> GetAll(IQueryObject queryObject)");
                    sw.WriteLine("{");
                    sw.WriteLine("var query = _" + modelName.ToLower() + "Repository");
                    sw.WriteLine(".GetAll()");
                    sw.WriteLine(".Where(w => w.IsDeleted == false).AsQueryable();");
                    sw.WriteLine("");
                    sw.WriteLine("var columnsMap = new Dictionary<string, Expression<Func<Entities.Setup." + modelName + ", object>>>");
                    sw.WriteLine("{");
                    sw.WriteLine("[\"Name\"] = v => v.Name,");
                    sw.WriteLine("[\"Code\"] = v => v.Code,");
                    sw.WriteLine("};");
                    sw.WriteLine("query = query.ApplyOrdering(queryObject, columnsMap);");
                    sw.WriteLine("var result = await query.ApplyPaging(queryObject).ToListAsync();");
                    sw.WriteLine("return _objectMapper.Map<IEnumerable<" + modelName + "Dto>>(result);");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public async Task<" + modelName + "Dto> Update(" + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + ");");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(await _" + modelName.ToLower() + "Repository.Update(result));");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    //sw.WriteLine("public void Delete(" + modelName + "Dto " + modelName.ToLower() + ")");
                    //sw.WriteLine("{");
                    //sw.WriteLine("_" + modelName.ToLower() + "Repository.Delete(_objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + "));");
                    //sw.WriteLine("}");

                    sw.WriteLine("public async Task DeleteById(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("await _" + modelName.ToLower() + "Repository.Delete(id);");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("}");// end class

                    sw.WriteLine("}");//end namespace


                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}