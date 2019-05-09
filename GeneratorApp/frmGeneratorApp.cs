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

            new GenerateModel().CreateModel(fileName+"\\"+txtModelName.Text+".cs",txtModelName.Text,_modelProperties);
            new GenerateDto().CreateDto(fileName+"\\"+txtModelName.Text+"Dto.cs",txtModelName.Text,_modelProperties);
            new GenerateIAppService().CreateIAppService(fileName+"\\I"+txtModelName.Text+"AppService.cs",txtModelName.Text);
            new GenerateAppService().CreateAppService(fileName+"\\"+txtModelName.Text+"AppService.cs",txtModelName.Text);
            new GenerateController().CreateController(fileName+"\\"+txtModelName.Text+ "Controller.cs",txtModelName.Text);
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
