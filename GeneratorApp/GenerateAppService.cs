using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GeneratorApp
{
    public class GenerateAppService
    {
        public void CreateAppService(string fileName, string modelName, IEnumerable<ModelProperty> modelProperties)
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
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("using System.Linq;");
                    sw.WriteLine("using HRIS.Extensions;");
                    sw.WriteLine("using HRIS.WebQueryModel;");
                    sw.WriteLine("using Microsoft.EntityFrameworkCore;");
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Linq.Expressions;");
                    sw.WriteLine("using System.Threading.Tasks;");
                    sw.WriteLine("");

                    sw.WriteLine("namespace HRIS.Setup." + modelName + "");
                    sw.WriteLine("{");

                    sw.WriteLine("[RemoteService(false)]");
                    sw.WriteLine("public class " + modelName + "AppService : ApplicationService, I" + modelName + "AppService");
                    sw.WriteLine("{");


                    sw.WriteLine("private readonly IObjectMapper _objectMapper;");
                    sw.WriteLine("private  readonly IRepository<Entities.Setup." + modelName + "> _" + modelName.ToLower() + "Repository;");
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
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(await _" + modelName.ToLower() + "Repository.InsertAsync(" + modelName.ToLower() + "Obj));");
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

                    sw.WriteLine("public async Task<QueryResult<" + modelName + "Dto>> GetAll(IQueryObject queryObject)");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = new QueryResult<" + modelName + "Dto>();");
                    sw.WriteLine("var query = _" + modelName.ToLower() + "Repository");
                    sw.WriteLine(".GetAll()");
                    sw.WriteLine(".Where(w => w.IsDeleted == false).AsQueryable();");
                    sw.WriteLine("");
                    sw.WriteLine("var columnsMap = new Dictionary<string, Expression<Func<Entities.Setup." + modelName + ", object>>>");
                    sw.WriteLine("{");
                    foreach (var item in modelProperties)
                    {
                        sw.WriteLine("[\"" + item.PropertyName + "\"] = v => v." + item.PropertyName + ",");
                    }
                    sw.WriteLine("};");

                    sw.WriteLine("query = query.ApplyOrdering(queryObject, columnsMap);");
                    sw.WriteLine("");
                    sw.WriteLine("result.TotalItems = await query.CountAsync();");
                    sw.WriteLine("");
                    sw.WriteLine("query = query.ApplyPaging(queryObject);");
                    sw.WriteLine("");
                    sw.WriteLine("result.Items = _objectMapper.Map<IEnumerable<" + modelName + "Dto>>(await query.ToListAsync());");
                    sw.WriteLine("");
                    sw.WriteLine("return result;");

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public async Task<" + modelName + "Dto> Update(" + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + ");");
                    sw.WriteLine("return _objectMapper.Map<" + modelName + "Dto>(await _" + modelName.ToLower() + "Repository.UpdateAsync(result));");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    //sw.WriteLine("public void Delete(" + modelName + "Dto " + modelName.ToLower() + ")");
                    //sw.WriteLine("{");
                    //sw.WriteLine("_" + modelName.ToLower() + "Repository.Delete(_objectMapper.Map<Entities.Setup." + modelName + ">(" + modelName.ToLower() + "));");
                    //sw.WriteLine("}");

                    sw.WriteLine("public async Task DeleteById(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("await _" + modelName.ToLower() + "Repository.DeleteAsync(id);");
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