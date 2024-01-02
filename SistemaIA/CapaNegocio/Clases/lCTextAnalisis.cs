using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------Azure IA TEXT
using Azure.AI.TextAnalytics;
using Azure;
using System.IO;
using System.Reflection.Metadata;
using OpenTextSummarizer;
using CapaNegocio.Interfaces;
using System.Text.RegularExpressions;
using CapaDatos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//--------------------Traslate
using Newtonsoft.Json;
using Azure.AI.Translation.Text;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;
//-------------lerPDF
using iText.Kernel.Pdf;
using System.Reflection.PortableExecutable;
using iText.Kernel.Pdf.Canvas.Parser;
using static iText.IO.Util.IntHashtable;
using iText.Forms.Form.Element;
//---------------------IMG
using Tesseract;
using System.Security.Cryptography.X509Certificates;
namespace CapaNegocio.Clases
{
    public class lCTextAnalisis : ITextAnalisis
    {
        //Asignacion de las variables de entorno llamadas "LANGUAGE_KEY" y "LANGUAGE_ENDPOINT"
        private static readonly string languageKey = Environment.GetEnvironmentVariable("LANGUAGE_KEY");
        private static readonly string languageEndpoint = Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT");

        private static readonly AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
        private static readonly Uri endpoint = new Uri(languageEndpoint);

        private List<EntidadesTxt> listaEntidadestxt = new List<EntidadesTxt>();

        DBIASYSTEMContext dbcontext;
        public lCTextAnalisis(DBIASYSTEMContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public string getSentiment(TextAnalyticsClient cliente, string texto)
        {
            var documentos = new List<string> { texto };
            string sentimiento = "El sentimiento no se encontro, vuelvalo a intentar!";
            AnalyzeSentimentResultCollection resultados = cliente.AnalyzeSentimentBatch(documentos, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            foreach (AnalyzeSentimentResult resultado in resultados)
            {
                //Console.WriteLine($"Sentimiento del documento: {resultado.DocumentSentiment.Sentiment}\n");
                sentimiento = resultado.DocumentSentiment.Sentiment.ToString();
            }

            return sentimiento;

        }

        public List<EntidadesTxt> getEntidades(TextAnalyticsClient cliente, string texto)
        {
            var documentos = new List<string> { texto };
            List<EntidadesTxt> listaEntidadesTxt = new List<EntidadesTxt>();

            // Identificación de entidades
            RecognizeEntitiesResultCollection resultadosEntidades = cliente.RecognizeEntitiesBatch(documentos);

            // Imprimir resultados de identificación de entidades
            foreach (RecognizeEntitiesResult resultadoEntidades in resultadosEntidades)
            {
                Console.WriteLine("Entidades encontradas:");
                foreach (CategorizedEntity entidad in resultadoEntidades.Entities)
                {
                    EntidadesTxt entidades = new EntidadesTxt();
                    entidades.tipo = entidad.Category.ToString();
                    entidades.texto = entidad.Text;
                    entidades.puntuacion = entidad.ConfidenceScore;
                    listaEntidadesTxt.Add(entidades);
                    //Console.WriteLine(entidades);
                    Console.WriteLine($"\tTipo: {entidad.Category}, Texto: {entidad.Text}, Puntuación: {entidad.ConfidenceScore}");
                }

            }
            listaEntidadestxt = listaEntidadesTxt;
            return (listaEntidadesTxt);
        }
        public bool EsFactura(List<EntidadesTxt> entidades)
        {
            // Buscar patrones o combinaciones específicas que sugieran una factura
            // Puedes ajustar esta lógica según las características de tus facturas

            //bool contienePalabraPrecioOAmount = entidades.Any(e => e.texto.ToLower().Contains("precio") || e.texto.ToLower().Contains("price") || e.texto.ToLower().Contains("amount") || e.texto.ToLower().Contains("cantidad") || e.texto.ToLower().Contains("quantity"));
            int cantidadQuantity = entidades.Count(e => e.tipo == "Quantity");
            int cantidadProducto = entidades.Count(e => e.tipo == "Product");
            // Establece un umbral para la cantidad de entidades de interés
            bool cumpleUmbral = cantidadQuantity > 4 && cantidadProducto > 3;

            // Combinación de condiciones que podrían indicar una factura
            //bool esFactura = cumpleUmbral;

            return cumpleUmbral;
        }
        public async Task<string> getResumen(TextAnalyticsClient client, string document)
        {
            // Prepare the input of the text analysis operation. You can add multiple documents to this list and
            // perform the same operation on all of them simultaneously.
            List<string> batchedDocuments = new()
            {
                document
            };
            string resumentxt = "";
            // Perform the text analysis operation.
            AbstractiveSummarizeOperation operation = client.AbstractiveSummarize(WaitUntil.Completed, batchedDocuments);

            // View the operation results.
            await foreach (AbstractiveSummarizeResultCollection documentsInPage in operation.Value)
            {
                Console.WriteLine($"Abstractive Summarize, model version: \"{documentsInPage.ModelVersion}\"");
                Console.WriteLine();

                foreach (AbstractiveSummarizeResult documentResult in documentsInPage)
                {
                    if (documentResult.HasError)
                    {
                        Console.WriteLine($"  Error!");
                        Console.WriteLine($"  Document error code: {documentResult.Error.ErrorCode}");
                        Console.WriteLine($"  Message: {documentResult.Error.Message}");
                        continue;
                    }

                    Console.WriteLine($"  Produced the following abstractive summaries:");
                    Console.WriteLine();

                    foreach (AbstractiveSummary summary in documentResult.Summaries)
                    {
                        resumentxt = summary.Text.Replace("\n", " ");
                        Console.WriteLine($"  Text: {summary.Text.Replace("\n", " ")}");
                        Console.WriteLine($"  Contexts:");

                        foreach (AbstractiveSummaryContext context in summary.Contexts)
                        {
                            Console.WriteLine($"    Offset: {context.Offset}");
                            Console.WriteLine($"    Length: {context.Length}");
                        }

                        Console.WriteLine();
                    }
                }
            }
            resumentxt = traducirTexto(resumentxt).Result;
            return (resumentxt);
        }

        public string getdescripccion(string texto)
        {

            // Expresión regular para encontrar palabras
            Regex regex = new Regex(@"\b\w+\b");

            // Obtener todas las coincidencias
            MatchCollection matches = regex.Matches(texto);

            // Tomar las primeras 10 palabras
            int cantidadPalabras = Math.Min(matches.Count, 10);
            string[] palabras = new string[cantidadPalabras];

            for (int i = 0; i < cantidadPalabras; i++)
            {
                palabras[i] = matches[i].Value;
            }

            // Unir las palabras en una cadena
            string resultado = string.Join(" ", palabras);

            return resultado;
        }

        public DocumentInfo getDocumentInfo(string texto)
        {
            var cliente = new TextAnalyticsClient(endpoint, credentials);
            DocumentInfo documentInfo = new DocumentInfo();
            documentInfo.Resumen = getResumen(cliente, texto).Result;
            documentInfo.Descripccion = getdescripccion(documentInfo.Resumen);
            documentInfo.Sentimiento = getSentiment(cliente, texto);

            dbcontext.Add(documentInfo);
            dbcontext.SaveChanges();

            return documentInfo;
        }

        private static readonly string key = "9b1fd0dbf77141b1b0896ef6a4fc5e15";
        private static readonly string endpointTraslate = "https://api.cognitive.microsofttranslator.com/";

        // location, also known as region.
        // required if you're using a multi-service or regional (not global) resource. It can be found in the Azure portal on the Keys and Endpoint page.
        private static readonly string location = "eastus";
        public async Task<string> traducirTexto(string textToTranslate)
        {
            // Input and output languages are defined as parameters.
            string route = "/translate?api-version=3.0&from=en&to=es";
            object[] body = new object[] { new { Text = textToTranslate.Replace("\n", "") } };
            var requestBody = JsonConvert.SerializeObject(body);
            string result = "";
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpointTraslate + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                Console.WriteLine(response);
                // Read response as a string.
                result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            // Deserializar el JSON
            JArray jsonArray = JArray.Parse(result);

            // Obtener el texto de la traducción
            string translatedText = jsonArray[0]["translations"][0]["text"].ToString();
            return translatedText;
        }

        public string ScanerPdfToText(byte[] archivo)
        {
            string txt = "";
            if (archivo != null && archivo.Length > 0)
            {
                var extractedText = new StringBuilder();
                // Leer el contenido del archivo PDF/Datos enviados
                using (var stream = new MemoryStream())
                {
                    // Utilizar iTextSharp para extraer texto del PDF
                    var pdfReader = new PdfReader(new MemoryStream(archivo));
                    var pdfDocument = new PdfDocument(pdfReader);
                    var numberOfPages = pdfDocument.GetNumberOfPages();


                    for (int i = 1; i <= numberOfPages; i++)
                    {
                        extractedText.AppendLine(PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i)));
                    }
                }
                txt = extractedText.ToString();
            }
            return (txt);
        }

        public string scannerIMGToText(byte[] archivo)
        {
            //string pathImg = @"C:\Users\ISAAC\Desktop\MyProjets\EvaluacionOneCore\TestBlazor\ConsoleApp1\ConsoleApp1\invoiceexample.png";

            string text = "";

            try
            {
                using (var engine = new TesseractEngine(@"C:\Users\ISAAC\Documents\GitHub\DesafioOneCore\SistemaIA\CapaNegocio\tessdata\", "eng", EngineMode.Default))
                {

                    using (var img = Pix.LoadFromMemory(archivo))
                    {
                        using (var page = engine.Process(img))
                        {
                            text = page.GetText();
                        }
                    }

                }
               
               // Console.WriteLine("Texto de la imagen:  "+text);
              
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return text;
        }

        public int identificaryGestionarArchivo(ArchivoRequest archivoRequest)
        {
            Registro registro = new Registro();

            //Si idArchivo es 1=pdf / si es 0 es una img
            int idArchivo = 2;
            var cliente = new TextAnalyticsClient(endpoint, credentials);
            string text = "";
            try
            {
                if (archivoRequest.tipo == "application/pdf")
                {
                    text = ScanerPdfToText(archivoRequest.archivo);
                    if (!EsFactura(getEntidades(cliente, text)))
                    {
                        getDocumentInfo(text);
                        idArchivo = 1;
                        registro.Tipo = "application/pdf, resumen y análisis de pdf. Azure Cognitive Services";

                    }
                    else
                    {
                        registro.Tipo = "application/pdf, Análisis y scaneo de Factura. Azure Cognitive Services";
                        getFactura();
                        idArchivo = 0;
                    }
                    registro.Descripccion = listaEntidadestxt[0].texto;
                    registro.FechaRegistro = DateTime.Now;
                    dbcontext.Registros.Add(registro);
                    dbcontext.SaveChanges();
                }
                else
                {        
                    text = scannerIMGToText(archivoRequest.archivo);
                    if (!EsFactura(getEntidades(cliente, text)))
                    {
                        getDocumentInfo(text);
                        idArchivo = 1;
                        registro.Tipo = "IMG, resumen y análisis de pdf. Azure Cognitive Services";

                    }
                    else
                    {
                        registro.Tipo = "IMG, Análisis y scaneo de Factura. Azure Cognitive Services";
                        getFactura();
                        idArchivo = 0;
                       

                    }
                    registro.Descripccion = listaEntidadestxt[0].texto;
                    registro.FechaRegistro = DateTime.Now;
                    dbcontext.Registros.Add(registro);
                    dbcontext.SaveChanges();
                    //aqui va para procesar la img
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return idArchivo;
        }

        public void getFactura()
        {
            Factura factura = new Factura();
            //listaEntidadestxt
            int contador = 0;
            foreach (var item in listaEntidadestxt)
            {
                var campo = item.tipo;
                if(campo  == "Organization" && contador == 0)
                {
                    factura.NombreProveedor = item.texto;
                    contador++;
                }
                if (campo == "Location" && contador == 1)
                {
                    factura.DireccionProveedor = item.texto;
                    contador++;
                }
                if (campo == "Organization" && contador == 2)
                {
                    factura.NombreCliente = item.texto;
                    contador++;
                }
                if (campo == "DateTime" && (contador>=0 && contador <= 4))
                {
                    factura.Fecha = Convert.ToDateTime(item.texto);
                    contador++;
                }
                if (campo == "Quantity" && contador >= listaEntidadestxt.Count-2 && item.texto.Contains("$"))
                {
                    factura.Fecha = Convert.ToDateTime(item.texto);
                    contador++;
                }
            }
            dbcontext.Facturas.Add(factura);
            dbcontext.SaveChanges();
            Console.WriteLine("Factura funtion: " + factura);
            //return factura;
        }

        //public List<Factura> ProcesarEntidades(List<EntidadesTxt> entidades)
        //{
        //    // Filtrar entidades por tipo
        //    var productos = entidades.Where(e => e.tipo == "Product").ToList();
        //    var quantities = entidades.Where(e => e.tipo == "Quantity").ToList();
        //    var fechas = entidades.Where(e => e.tipo == "DateTime").ToList();

        //    // Crear instancia de Factura
        //    var factura = new Factura();

        //    // Extraer información de las entidades
        //    foreach (var entidad in entidades)
        //    {
        //        switch (entidad.tipo)
        //        {
        //            case "Product":
        //                AgregarProducto(factura, entidad);
        //                break;
        //            case "Quantity":
        //                AgregarCantidad(factura, entidad);
        //                break;
        //            case "DateTime":
        //                AgregarFecha(factura, entidad);
        //                break;
        //                // Agregar más casos según sea necesario
        //        }
        //    }

        //    // Devolver la lista de facturas (puede haber más de una si hay varios productos)
        //    return factura;
        //}

        //private void AgregarProducto(Factura factura, EntidadesTxt entidad)
        //{
        //    // Crear instancia de ProductosFactura y agregar a la lista de productos de la factura
        //    var producto = new ProductosFactura
        //    {
        //        Nombre = entidad.texto,
        //        // Puedes ajustar las propiedades según sea necesario
        //    };

        //    factura.Productos.Add(producto);
        //}

        //private void AgregarCantidad(Factura factura, EntidadesTxt entidad)
        //{
        //    // Actualizar la cantidad en el último producto agregado
        //    var ultimoProducto = factura.Productos.LastOrDefault();
        //    if (ultimoProducto != null)
        //    {
        //        ultimoProducto.Cantidad = int.Parse(entidad.texto);
        //    }
        //}

        //private void AgregarFecha(Factura factura, EntidadesTxt entidad)
        //{
        //    // Convertir el texto de fecha a DateTime y asignar a la propiedad Fecha de la factura
        //    factura.Fecha = DateTime.Parse(entidad.texto);
        //}
    }
}
