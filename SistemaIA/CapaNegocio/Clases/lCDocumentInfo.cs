﻿using CapaDatos;
using CapaNegocio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Clases
{
    public class lCDocumentInfo: IDocumentInfo
    {
        private DBIASYSTEMContext dbContext;
        public lCDocumentInfo(DBIASYSTEMContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string createDocumntInfo(DocumentInfo document)
        {
            try
            {
                if (document != null)
                {
                    dbContext.DocumentInfo.Add(document);
                    dbContext.SaveChanges();
                    return "El documento se creo con exito";
                }
                else
                {
                    return "El documento se encuentra vacio, intentelo de nuevo";
                }
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
        }

        public DocumentInfo getDocumentInfo(int id) 
        {
            return dbContext.DocumentInfo.Find(id);
        }

        public DocumentInfo getLastDocumentInfo()
        {
            return dbContext.DocumentInfo.OrderByDescending(d => d.IdDocument).FirstOrDefault();
        }
    }
}
