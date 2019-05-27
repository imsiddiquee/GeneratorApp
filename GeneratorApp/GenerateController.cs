using System;
using System.IO;
using System.Windows.Forms;

namespace GeneratorApp
{
    public class GenerateController
    {
        public void CreateController(string fileName,string modelName)
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
                    sw.WriteLine("using Microsoft.AspNetCore.Http;");
                    sw.WriteLine("using Microsoft.AspNetCore.Mvc;");
                    sw.WriteLine("using System.Threading.Tasks;");
                    sw.WriteLine("using HRIS.WebQueryModel;");
                    sw.WriteLine("");

                    sw.WriteLine("namespace HRIS.Web.Host.Controllers");
                    sw.WriteLine("{");

                    sw.WriteLine("public class " + modelName + "Controller : HRISControllerBase");
                    sw.WriteLine("{");

                    sw.WriteLine("");
                    sw.WriteLine("private readonly I" + modelName + "AppService _" +modelName.ToLower()+"AppService;");
                    sw.WriteLine("");

                    sw.WriteLine("public " + modelName + "Controller(I" +modelName+"AppService "+modelName.ToLower()+"AppService)");
                    sw.WriteLine("{");
                    sw.WriteLine("_"+modelName.ToLower()+"AppService = "+modelName.ToLower()+"AppService;");
                    sw.WriteLine("}");//end constructor

                    sw.WriteLine("[HttpGet]");
                    sw.WriteLine("public async Task<IActionResult> Get(int id)");
                    sw.WriteLine("{");
                    sw.WriteLine("if (id<0)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: Id is null or empty\");");
                    sw.WriteLine("return BadRequest(\"Id is null or empty\");");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = await _"+modelName.ToLower()+"AppService.GetById(id);");
                    sw.WriteLine("");
                    sw.WriteLine("return result == null ? StatusCode(StatusCodes.Status204NoContent, result) : Ok(result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + modelName + "Controller] -[Get]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("[HttpGet]");
                    sw.WriteLine("public async Task<IActionResult> GetAll(ModelQuery queryObject)");
                    sw.WriteLine("{");
                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = await _"+modelName.ToLower()+ "AppService.GetAll(queryObject);");
                    sw.WriteLine("return result.TotalItems > 0 ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + modelName + "Controller] -[GetAll]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("[HttpPost]");
                    sw.WriteLine("public async Task<IActionResult> Create([FromBody] " + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("if (" + modelName.ToLower() + " == null)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: " + modelName + " is empty or null\");");
                    sw.WriteLine("return BadRequest(\"" + modelName + " is empty or null\");");
                    sw.WriteLine("}");

                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = await _"+modelName.ToLower()+"AppService.Create(" + modelName.ToLower() + ");");
                    sw.WriteLine("return result != null ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + modelName + "Controller] -[Create]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("[HttpPut]");
                    sw.WriteLine("public async Task<IActionResult> Update([FromBody] " + modelName + "Dto " + modelName.ToLower() + ")");
                    sw.WriteLine("{");
                    sw.WriteLine("if (" + modelName.ToLower() + " == null)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: " + modelName + " is empty or null\");");
                    sw.WriteLine("return BadRequest(\"" + modelName + " is empty or null\");");
                    sw.WriteLine("}");

                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("var result = await _"+modelName.ToLower()+"AppService.Update(" + modelName.ToLower() + ");");
                    sw.WriteLine("return result != null ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + modelName + "Controller] -[Update]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace: \" + ex.StackTrace);");

                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    //sw.WriteLine("[HttpDelete]");
                    //sw.WriteLine("public async Task<IActionResult> Delete([FromBody] " + modelName + "Dto " + modelName.ToLower() + ")");
                    //sw.WriteLine("{");
                    //sw.WriteLine("if (" + modelName.ToLower() + " == null)");
                    //sw.WriteLine("{");
                    //sw.WriteLine("Logger.Error(\"ERROR: BadRequest: " + modelName + " is empty or null\");");
                    //sw.WriteLine("return BadRequest(\"" + modelName + " is empty or null\");");
                    //sw.WriteLine("}");
                    //sw.WriteLine("");
                    //sw.WriteLine("try");
                    //sw.WriteLine("{");
                    //sw.WriteLine("await _"+modelName.ToLower()+"AppService.Delete(" + modelName.ToLower() + ");");
                    //sw.WriteLine("return Ok(true);");
                    //sw.WriteLine("}");
                    //sw.WriteLine("catch (Exception ex)");
                    //sw.WriteLine("{");
                    //sw.WriteLine("Logger.Error(\"ERROR: [" + modelName + "Controller] -[Delete]: ExceptionMessage: \" + ex.Message +");
                    //sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    //sw.WriteLine("\", StackTrace:\" + ex.StackTrace);");
                    //sw.WriteLine("");
                    //sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    //sw.WriteLine("}");
                    //sw.WriteLine("}");

                    sw.WriteLine("[HttpDelete]");
                    sw.WriteLine("public async Task<IActionResult> Delete(int? id)");
                    sw.WriteLine("{");
                    sw.WriteLine("if (!id.HasValue)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: BadRequest: Id is empty or null\");");
                    sw.WriteLine("return BadRequest(\"Id is empty or null\");");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("try");
                    sw.WriteLine("{");
                    sw.WriteLine("await _" + modelName.ToLower() + "AppService.DeleteById(id.Value);");
                    sw.WriteLine("return Ok(true);");
                    sw.WriteLine("}");
                    sw.WriteLine("catch (Exception ex)");
                    sw.WriteLine("{");
                    sw.WriteLine("Logger.Error(\"ERROR: [" + modelName + "Controller] -[Delete_ID]: ExceptionMessage: \" + ex.Message +");
                    sw.WriteLine("\", InnerException: \" + ex.InnerException +");
                    sw.WriteLine("\", StackTrace:\" + ex.StackTrace);");
                    sw.WriteLine("");
                    sw.WriteLine("return StatusCode(StatusCodes.Status500InternalServerError);");
                    sw.WriteLine("}");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("}");//end class

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