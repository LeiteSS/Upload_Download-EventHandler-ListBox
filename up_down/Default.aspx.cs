using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace up_down
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArquivosNaListBox();
            }

            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "click")
            {
                if (ListBox1.SelectedItem != null)
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "filename="
                        + ListBox1.SelectedItem);
                    Response.TransmitFile(ConfigurationManager.AppSettings["pasta"]
                        + ListBox1.SelectedItem);
                    Response.End();
                }
            }
            ListBox1.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(ListBox1, "click"));
        }

        protected void ArquivosNaListBox()
        {
            //Alterar na web.config, a pasta onde os arquivos serão salvos, para a listBox os carregar
            string[] filePaths = Directory.GetFiles(ConfigurationManager.AppSettings["pasta"]);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            ListBox1.DataSource = files;
            ListBox1.DataBind();
        }


        protected void SaveFile(HttpPostedFile file)
        {
            // Caminho especificado na web.config
            string savePath = ConfigurationManager.AppSettings["pasta"];

            // Get the name of the file to upload.
            string fileName = FileUpload1.FileName;

            // Cria um caminho com a pasta onde será salvo e o nome do arquivo para checar duplicatas
            string pathToCheck = savePath + fileName;

            //Cria um arquivo temporario para checar duplicatas
            string tempfileName = "";

            // Confere se existe algum arquivo com o mesmo nome 
            //na pasta onde o arquivo será salvo.
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // Se um arquivo de mesmo nome já existe,
                    // Um numero é adicionado proximo ao nome original do arquivo.
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;

                UploadStatusLabel.Text = "Um arquivo com esse nome já existe" +
                    "<br />O seu arquivo foi salvo como " + fileName;
            }
            else
            {
                UploadStatusLabel.Text = "Seu arquivo foi enviado com sucesso.";
            }

            // Anexa o nome do arquivo
            savePath += fileName;

            FileUpload1.SaveAs(savePath);
            ArquivosNaListBox();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            {
                SaveFile(FileUpload1.PostedFile);
            }
            else
            {
                UploadStatusLabel.Text = "Não foi salvo nenhum arquivo!";
            }
        }


    }
}