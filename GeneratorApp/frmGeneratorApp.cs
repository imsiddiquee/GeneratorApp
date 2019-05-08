using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneratorApp
{
    public partial class frmGeneratorApp : Form
    {
        public List<ModelProperty> _modelProperties { get; set; }

        public frmGeneratorApp()
        {
            InitializeComponent();
            _modelProperties=new List<ModelProperty>();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = string.Empty;
            txtModelName.Text = string.Empty;
            txtModel.Text = string.Empty;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var fileName = txtFilePath.Text;
            

            var reader = new StringReader(txtModel.Text);
            
            ModelToList(reader);

            GenerateModel(fileName+"\\"+txtModelName.Text+".cs");
            GenerateDto(fileName+"\\"+txtModelName.Text+"Dto.cs");
            GenerateIAppService(fileName+"\\I"+txtModelName.Text+"AppService.cs");
            GenerateAppService(fileName+"\\"+txtModelName.Text+"AppService.cs");
            GenerateController(fileName+"\\"+txtModelName.Text+ "Controller.cs");
        }

        private void GenerateModel(string fileName)
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

                    sw.WriteLine("public class " + txtModelName.Text + " : FullAuditedEntity<int>, IMustHaveTenant, IHasIsActive");
                    sw.WriteLine("{");                    

                    foreach (var item in _modelProperties)
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

        private void GenerateController(string fileName)
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
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Linq;");
                    sw.WriteLine("using HRIS.Controllers;");
                    sw.WriteLine("using HRIS.Setup.Country;");
                    sw.WriteLine("using HRIS.Setup.Country.Dto;");
                    sw.WriteLine("using Microsoft.AspNetCore.Http;");
                    sw.WriteLine("using Microsoft.AspNetCore.Mvc;");
                    sw.WriteLine("");
                    sw.WriteLine("namespace HRIS.Web.Host.Controllers");
                    sw.WriteLine("{");
                    sw.WriteLine("public class " + txtModelName.Text + "Controller : HRISControllerBase");
                    sw.WriteLine("{");
                    sw.WriteLine("private readonly I" + txtModelName.Text + "AppService _" +txtModelName.Text.ToLower()+"AppService;");

                    sw.WriteLine("public " + txtModelName.Text + "Controller(I" +txtModelName.Text+"AppService "+txtModelName.Text.ToLower()+"AppService)");
                    sw.WriteLine("{");
                    sw.WriteLine("_"+txtModelName.Text.ToLower()+"AppService = "+txtModelName.Text.ToLower()+"AppService;");
                    sw.WriteLine("}");

                    sw.WriteLine("[HttpGet]");
                    sw.WriteLine("public IActionResult Get(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("if (id<0)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: Id is null or empty\");");
                    sw.WriteLine("return BadRequest(\"Id is null or empty\");");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _"+txtModelName.Text.ToLower()+"AppService.GetById(id);");
                    sw.WriteLine("");
                    sw.WriteLine("return result == null ? StatusCode(StatusCodes.Status204NoContent, result) : Ok(result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + txtModelName.Text + "Controller] -[Get]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("[HttpGet]");
                    sw.WriteLine("public IActionResult GetAll()");
                    sw.WriteLine("{");
                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _"+txtModelName.Text.ToLower()+"AppService.GetAll();");
                    sw.WriteLine("return result.Any() ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + txtModelName.Text + "Controller] -[GetAll]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("[HttpPost]");
                    sw.WriteLine("public IActionResult Create([FromBody] " + txtModelName.Text + "Dto " + txtModelName.Text.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("if (" + txtModelName.Text.ToLower() + " == null)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: " + txtModelName.Text + " is empty or null\");");
                    sw.WriteLine("return BadRequest(\"" + txtModelName.Text + " is empty or null\");");
                    sw.WriteLine("}");

                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _"+txtModelName.Text.ToLower()+"AppService.Create(" + txtModelName.Text.ToLower() + ");");
                    sw.WriteLine("return result != null ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + txtModelName.Text + "Controller] -[Create]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("[HttpPost]");
                    sw.WriteLine("public IActionResult Update([FromBody] " + txtModelName.Text + "Dto " + txtModelName.Text.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("if (" + txtModelName.Text.ToLower() + " == null)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: " + txtModelName.Text + " is empty or null\");");
                    sw.WriteLine("return BadRequest(\"" + txtModelName.Text + " is empty or null\");");
                    sw.WriteLine("}");

                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _"+txtModelName.Text.ToLower()+"AppService.Update(" + txtModelName.Text.ToLower() + ");");
                    sw.WriteLine("return result != null ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + txtModelName.Text + "Controller] -[Update]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");

                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("[HttpDelete]");
                    sw.WriteLine("public IActionResult Delete([FromBody] " + txtModelName.Text + "Dto " + txtModelName.Text.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("if (" + txtModelName.Text.ToLower() + " == null)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: " + txtModelName.Text + " is empty or null\");");
                    sw.WriteLine("return BadRequest(\"" + txtModelName.Text + " is empty or null\");");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("_"+txtModelName.Text.ToLower()+"AppService.Delete(" + txtModelName.Text.ToLower() + ");");
                    sw.WriteLine("return Ok(true);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + txtModelName.Text + "Controller] -[Delete]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace:\" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    sw.WriteLine("}");
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

        private void GenerateDto(string fileName)
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
                    sw.WriteLine("using Abp.Application.Services.Dto;");
                    sw.WriteLine("using Abp.AutoMapper;");

                    sw.WriteLine("");
                    sw.WriteLine("");

                    sw.WriteLine("namespace HRIS.Setup."+txtModelName.Text+".Dto");
                    sw.WriteLine("{");
                    sw.WriteLine("[AutoMap(typeof(Entities.Setup."+txtModelName.Text+"))]");
                    sw.WriteLine("public class " + txtModelName.Text + "Dto : EntityDto");
                    sw.WriteLine("{");
                    foreach (var item in _modelProperties)
                    {
                        var propertyFormat = $"public {item.PropertyType} {item.PropertyName}";
                        sw.WriteLine(propertyFormat + "{ get; set; }");
                    }

                    sw.WriteLine("}");
                    sw.WriteLine("}");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        private void GenerateIAppService(string fileName)
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
                    sw.WriteLine("namespace HRIS.Setup."+txtModelName.Text+"");
                    sw.WriteLine("{");
                    sw.WriteLine("public interface I"+txtModelName.Text+"AppService : IApplicationService");
                    sw.WriteLine("{");
                    sw.WriteLine(""+txtModelName.Text+"Dto GetById(int? id);");
                    sw.WriteLine("List<"+txtModelName.Text+"Dto> GetAll();");
                    sw.WriteLine(""+txtModelName.Text+"Dto Create("+txtModelName.Text+"Dto "+txtModelName.Text.ToLower()+");");
                    sw.WriteLine(""+txtModelName.Text+"Dto Update("+txtModelName.Text+"Dto "+txtModelName.Text.ToLower()+");");
                    sw.WriteLine("void Delete("+txtModelName.Text+"Dto country);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void GenerateAppService(string fileName)
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
                    sw.WriteLine("namespace HRIS.Setup."+txtModelName.Text+"");
                    sw.WriteLine("{");
                    sw.WriteLine("public class "+ txtModelName.Text + "AppService : ApplicationService, I"+ txtModelName.Text + "AppService");
                    sw.WriteLine("{");
                    sw.WriteLine("private readonly IObjectMapper _objectMapper;");
                    sw.WriteLine("private  readonly IRepository<Entities.Setup."+ txtModelName.Text + "> _" + txtModelName.Text.ToLower() + "Repository;");
                    sw.WriteLine("");
                    sw.WriteLine("public " + txtModelName.Text + "AppService(IObjectMapper objectMapper,IRepository<Entities.Setup." + txtModelName.Text + "> " + txtModelName.Text.ToLower() + "Repository)");
                    sw.WriteLine("{");
                    sw.WriteLine("_objectMapper = objectMapper;");
                    sw.WriteLine("_" + txtModelName.Text.ToLower() + "Repository = " + txtModelName.Text.ToLower() + "Repository;");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("public " + txtModelName.Text + "Dto Create(" + txtModelName.Text + "Dto " + txtModelName.Text.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var " + txtModelName.Text.ToLower() + "Obj = _objectMapper.Map<Entities.Setup." + txtModelName.Text + ">(" + txtModelName.Text.ToLower() + ");");
                    sw.WriteLine("" + txtModelName.Text.ToLower() + "Obj.TenantId = 4;");
                    sw.WriteLine("return _objectMapper.Map<" + txtModelName.Text + "Dto>(_" + txtModelName.Text.ToLower() + "Repository.Insert(" + txtModelName.Text.ToLower() + "Obj));");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("public " + txtModelName.Text + "Dto GetById(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _" + txtModelName.Text.ToLower() + "Repository");
                    sw.WriteLine(".GetAllList()");
                    sw.WriteLine(".FirstOrDefault(w => w.Id == id && w.IsActive == true && w.IsDeleted == false);");
                    sw.WriteLine("");
                    sw.WriteLine("return _objectMapper.Map<" + txtModelName.Text + "Dto>(result);");
                    sw.WriteLine("}");

                    sw.WriteLine("public List<" + txtModelName.Text + "Dto> GetAll()");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _" + txtModelName.Text.ToLower() + "Repository");
                    sw.WriteLine(".GetAll()");
                    sw.WriteLine(".Where(w => w.IsActive == true && w.IsDeleted == false).ToList();");
                    sw.WriteLine("");
                    sw.WriteLine("return _objectMapper.Map<List<" + txtModelName.Text + "Dto>>(result);");
                    sw.WriteLine("}");

                    sw.WriteLine("public " + txtModelName.Text + "Dto Update(" + txtModelName.Text + "Dto " + txtModelName.Text.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = _objectMapper.Map<Entities.Setup." + txtModelName.Text + ">(" + txtModelName.Text.ToLower() + ");");
                    sw.WriteLine("return _objectMapper.Map<" + txtModelName.Text + "Dto>(_" + txtModelName.Text.ToLower() + "Repository.Update(result));");
                    sw.WriteLine("}");

                    sw.WriteLine("public void Delete(" + txtModelName.Text + "Dto " + txtModelName.Text.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("_" + txtModelName.Text.ToLower() + "Repository.Delete(_objectMapper.Map<Entities.Setup." + txtModelName.Text + ">(" + txtModelName.Text.ToLower() + "));");
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

        private  void ModelToList(StringReader reader)
        {
            string line;
            var counter = 1;
            while (null != (line = reader.ReadLine()))
            {
                var result = line.Split(' ').ToList();

                var model = new ModelProperty
                {
                    OrderId = counter,                    
                    PropertyType = result[0],
                    PropertyName = result[1],
                    ModelName = txtModelName.Text
                };
                counter++;
                _modelProperties.Add(model);
            }
        }
    }
}
