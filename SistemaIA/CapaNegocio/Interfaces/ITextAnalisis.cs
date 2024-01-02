using Azure.AI.TextAnalytics;
using CapaDatos;
using CapaNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Interfaces
{
    public interface ITextAnalisis
    {
        string getSentiment(TextAnalyticsClient cliente, string texto);

        Task<string> getResumen(TextAnalyticsClient cliente, string texto);

        List<EntidadesTxt> getEntidades(TextAnalyticsClient cliente, string texto);

        string getdescripccion(string texto);

        DocumentInfo getDocumentInfo(string texto);

        string ScanerPdfToText(byte[] archivo);

        int identificaryGestionarArchivo(ArchivoRequest archivoRequest);
    }
}
